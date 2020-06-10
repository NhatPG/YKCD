using System.Linq;
using Framework.Helper;
using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Application.HscvService;
using YKCD.Agency.Business.Components;
using YKCD.Agency.Business.Entities;
using YKCD.Agency.Business.Services;
using Exceptionless;
using Exceptionless.Models;

namespace YKCD.Agency.Application.Agency
{
    public partial class VanBan_QuanLy : ManagePageBase
    {
        private Document item;
        public string VBDiCode;

        protected override void SetBaseControl()
        {
            LongId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "NHẬP THÔNG TIN VĂN BẢN MỚI";
            UpdateTitle = "CẬP NHẬT THÔNG TIN VĂN BẢN";
        }

        protected override void BindValueToPageControls()
        {
            foreach (var user in UserServices.GetList(new[] { UserRole.LanhDaoDonVi, UserRole.LanhDaoVP, UserRole.TruongPhongBan }))
            {
                NguoiKy.AddSelectItem(user.FullName, user.UserID.ToString(), user.DepartmentName.ToUpper());
            }

            NguoiSoanThao.BindData(UserServices.GetList(), "UserID", "FullName", "DepartmentName");
            LoaiVanBan.BindData(DocumentCategoryServices.GetList(), "DocumentCategoryID", "DocumentCategoryName");
        }

        protected override void SetDefaultValueOnCreate()
        {
            NguoiSoanThao.SelectByValue(Sessions.UserID);

            if (!string.IsNullOrEmpty(Parameters.VbdhCode))
            {
                ShowVBDHDocumentInfo();
            }
        }

        protected override void ShowObjectInformation()
        {
            item = DocumentServices.GetById(LongId);
            ViewState["CurrentObject"] = item;
            SoHieuVanBan.Text = item.DocumentCode;            
            NgayBanHanh.Text = item.ReleaseDate.ToDateString();
            TrichYeu.Text = item.MainContent;
            LoaiVanBan.SelectByValue(item.DocumentCategoryID);

            if(item.SignerID > 0)
            {
                NguoiKy.SelectByValue(item.SignerID);
                NguoiSoanThao.SelectByValue(item.WriterID);
                VanBanNgoai.Checked = false;

                TenNguoiKy.Visible = false;
                NguoiKy.Visible = true;
                SoanThaoPH.Visible = true;
            }
            else
            {
                VanBanNgoai.Checked = true;

                TenNguoiKy.Visible = true;
                NguoiKy.Visible = false;
                SoanThaoPH.Visible = false;
            }
            
            VanBanNgoai.Enabled = false;
        }

        protected void ShowVBDHDocumentInfo()
        {
            //Khởi tạo services
            ExchangeDocServiceSoapClient client = new ExchangeDocServiceSoapClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(AppSettings.HSCV_Service);

            if (!string.IsNullOrEmpty(Parameters.VbdhCode))
            {
                var document = client.GetVBDiByIDs(new ArrayOfString { Parameters.VbdhCode }).SingleOrDefault();
                ViewState["SyncDoc"] = document;

                if (document != null)
                {
                    SoHieuVanBan.Text = document.SoKyHieu;
                    TrichYeu.Text = document.TrichYeu;
                    NgayBanHanh.Text = document.NgayPhatHanh;
                    NguoiKy.SelectByValue(UserServices.GetUser(document.NguoiKy).UserID);
                    NguoiSoanThao.SelectByValue(Sessions.UserID);
                    LoaiVanBan.SelectByValue(4);

                    VanBanNgoai.Checked = true;
                    VanBanNgoai.Enabled = false;
                }
            }
        }

        protected override void CreateNewObject()
        {
            item = new Document();
            item.DocumentCategoryID = LoaiVanBan.SelectedValue.ToInteger();
            item.DocumentCode = SoHieuVanBan.Text.Trim().ToUpper();

            if(VanBanNgoai.Checked)
            {
                item.SignerName = TenNguoiKy.Text;
            }
            else
            {
                item.SignerID = NguoiKy.SelectedValue.ToInteger();
                item.SignerName = NguoiKy.SelectedItem.Text;
                item.WriterID = NguoiSoanThao.SelectedValue.ToInteger();
            }
            
            item.ReleaseDate = NgayBanHanh.Text.ToDateTime();
            item.MainContent = TrichYeu.Text.Trim();
            item.VBDHCode = Parameters.VbdhCode;
            DocumentServices.Create(item);

            if (!string.IsNullOrEmpty(Parameters.VbdhCode))
            {
                var VBDi = ViewState["SyncDoc"] as VBDiDTO;

                foreach (FileDinhKemDTO file in VBDi.FileDinhKems)
                {
                    DocumentFileServices.CreateDocumentFile(item, file.DuLieu, file.TenFileDinhKem, AppSettings.UploadFolder);
                }
            }
            else
            {
                DocumentFileServices.CreateDocumentFile(item, fileContent: FileDinhKem.PostedFile, fileName: FileDinhKem.PostedFile?.FileName, uploadFolder: AppSettings.UploadFolder);
            }

            ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Nhập thông tin văn bản số {item.DocumentCode} ({Sessions.DisplayName})", Type = "Nhập văn bản", Source = AppSettings.AGENCY_NAME });

            Redirector.Redirect(ViewNames.Agency.YKCD_QuanLy, ParameterNames.Pid, item.DocumentID);
        }

        protected override void UpdateObject()
        {
            item = (Document)ViewState["CurrentObject"];
            item.DocumentCategoryID = LoaiVanBan.SelectedValue.ToInteger();
            item.DocumentCode = SoHieuVanBan.Text.Trim().ToUpper();

            if (VanBanNgoai.Checked)
            {
                item.SignerName = TenNguoiKy.Text;
            }
            else
            {
                item.SignerID = NguoiKy.SelectedValue.ToInteger();
                item.SignerName = NguoiKy.SelectedItem.Text;
                item.WriterID = NguoiSoanThao.SelectedValue.ToInteger();
            }

            item.ReleaseDate = NgayBanHanh.Text.ToDateTime();
            item.MainContent = TrichYeu.Text.Trim();
            DocumentServices.Update(item);

            DocumentFileServices.CreateDocumentFile(item, FileDinhKem.PostedFile, FileDinhKem.PostedFile?.FileName, AppSettings.UploadFolder);

            ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Cập nhật thông tin văn bản số {item.DocumentCode} ({Sessions.DisplayName})", Type = "Cập nhật văn bản", Source = AppSettings.AGENCY_NAME });

            Redirector.Redirect(ViewNames.Agency.VanBan_DanhSach);
        }

        protected void VanBanNgoai_CheckedChanged(object sender, System.EventArgs e)
        {
            TenNguoiKy.Visible = VanBanNgoai.Checked;
            NguoiKy.Visible = !VanBanNgoai.Checked;
            SoanThaoPH.Visible = !VanBanNgoai.Checked;
        }
    }
}