using System.Linq;
using Framework.Helper;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Application.HscvService;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;
using Exceptionless;
using Exceptionless.Models;

namespace YKCD.Province.Application.Province
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

            btnSave.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        }

        protected override void BindValueToPageControls()
        {
            NguoiKy.BindData(UserServices.GetList(new[] { UserRole.LanhDaoUBNDTinh, UserRole.LanhDaoVPUBNDTinh }), "UserID", "FullName", "DepartmentName");
            NguoiSoanThao.BindData(UserServices.GetList(new[] { UserRole.ChuyenVienVPUBNDTinh }), "UserID", "FullName", "DepartmentName");
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
            NguoiKy.SelectByValue(item.SignerID);
            NguoiSoanThao.SelectByValue(item.WriterID);
            NgayBanHanh.Text = item.ReleaseDate.ToDateString();
            TrichYeu.Text = item.MainContent;
            LoaiVanBan.SelectByValue(item.DocumentCategoryID);
        }

        protected void ShowVBDHDocumentInfo()
        {
            //Khởi tạo services
            ExchangeDocServiceSoapClient client = new ExchangeDocServiceSoapClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(AppSettings.HSCV_Service);

            if (!string.IsNullOrEmpty(Parameters.VbdhCode))
            {
                var document = client.GetVBDiByIDs(new ArrayOfString { Parameters.VbdhCode })?.SingleOrDefault();

                ViewState["SyncDoc"] = document;
                if (document != null)
                {
                    SoHieuVanBan.Text = document.SoKyHieu;
                    TrichYeu.Text = document.TrichYeu;
                    NgayBanHanh.Text = document.NgayPhatHanh;
                    NguoiKy.SelectByValue(UserServices.GetUser(document.NguoiKy)?.UserID);
                    NguoiSoanThao.SelectByValue(Sessions.UserID);
                    LoaiVanBan.SelectByValue(4);
                }
            }
        }

        protected override void CreateNewObject()
        {
            item = new Document();
            item.DocumentCategoryID = LoaiVanBan.SelectedValue.ToInteger();
            item.DocumentCode = SoHieuVanBan.Text.Trim().ToUpper();
            item.SignerID = NguoiKy.SelectedValue.ToInteger();
            item.SignerName = NguoiKy.SelectedItem.Text;
            item.WriterID = NguoiSoanThao.SelectedValue.ToInteger();
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
                DocumentFileServices.CreateDocumentFile(item, fileContent: FileDinhKem.PostedFile, fileName: FileDinhKem.PostedFile.FileName, uploadFolder: AppSettings.UploadFolder);
            }

            ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Nhập thông tin văn bản số {item.DocumentCode} ({Sessions.DisplayName})", Type = "Nhập văn bản", Source = "YKCD_UBND" });

            Redirector.Redirect(ViewNames.Province.YKCD_QuanLy, ParameterNames.Pid, item.DocumentID);
        }

        protected override void UpdateObject()
        {
            item = (Document)ViewState["CurrentObject"];
            item.DocumentCategoryID = LoaiVanBan.SelectedValue.ToInteger();
            item.DocumentCode = SoHieuVanBan.Text.Trim().ToUpper();
            item.SignerID = NguoiKy.SelectedValue.ToInteger();
            item.SignerName = NguoiKy.SelectedItem.Text;
            item.WriterID = NguoiSoanThao.SelectedValue.ToInteger();
            item.ReleaseDate = NgayBanHanh.Text.ToDateTime();
            item.MainContent = TrichYeu.Text.Trim();
            DocumentServices.Update(item);

            DocumentFileServices.CreateDocumentFile(item, fileContent: FileDinhKem.PostedFile, fileName: FileDinhKem.PostedFile?.FileName, uploadFolder: AppSettings.UploadFolder);

            ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Cập nhật thông tin văn bản số {item.DocumentCode} ({Sessions.DisplayName})", Type = "Cập nhật văn bản", Source = "YKCD_UBND" });

            Redirector.Redirect(ViewNames.Province.VanBan_DanhSach);
        }
    }
}