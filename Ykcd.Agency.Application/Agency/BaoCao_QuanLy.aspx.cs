using System;
using System.Linq;
using System.ServiceModel;
using System.Web.UI.WebControls;
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
    public partial class BaoCao_QuanLy : ManagePageBase
    {
        private Report report;

        protected override void SetBaseControl()
        {
            LongId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "NHẬP THÔNG TIN BÁO CÁO MỚI";
            UpdateTitle = "CẬP NHẬT THÔNG TIN BÁO CÁO";

            ThongTinYKienChiDaoControl.RequestID = Parameters.Pid;
        }

        protected override void BindValueToPageControls()
        {
            var request = RequestServices.GetById(Parameters.Pid);

            ThucHien.Items.Clear();

            if (request.Performs?.Count > 0)
            {
                foreach (var perform in request.Performs)
                {
                    ThucHien.AddSelectItem(perform.DepartmentID > 0 ? perform.Department.DepartmentName : perform.User.FullName, perform.PerformID.ToString());
                }
            }
            else if (request.IsProvinceRequest)
            {
                ThucHien.Items.Add(new ListItem(text: AppSettings.AGENCY_NAME, value: ""));
            }

            //Hiển thị ô chọn văn bản có ý kiến chỉ đạo nếu đây là YKCD của UBND tỉnh và có dữ liệu ở HSCV Service
            if (!string.IsNullOrEmpty(AppSettings.HSCV_Service) && request.IsProvinceRequest && AppSettings.IS_USE_SYNC_DOCUMENT_REPORT)
            {
                ExchangeDocServiceSoapClient client = new ExchangeDocServiceSoapClient();
                client.Endpoint.Address = new EndpointAddress(AppSettings.HSCV_Service);

                var syncDocs = client.GetVBDiHasIdeaLeader(DateTime.Now.AddDays(-7), DateTime.Now);

                if (syncDocs != null && syncDocs.Length > 0)
                {
                    VanBanDaBaoCaoGroup.Visible = true;

                    VanBanDaBaoCao.Items.Clear();

                    foreach (var item in syncDocs)
                    {
                        VanBanDaBaoCao.AddSelectItem($"{item.SoKyHieu} ({item.TrichYeu})", item.MaVBDi);
                    }

                    VanBanDaBaoCao.AddSelectItem("Chọn văn bản báo cáo", "");
                }
            }
        }

        protected override void SetDefaultValueOnCreate()
        {
            var requestPerforms = RequestServices.GetById(Parameters.Pid)?.Performs;

            if (requestPerforms != null)
            {
                if (Sessions.UserID > 0 && requestPerforms.Count(item => item.UserID == Sessions.UserID) > 0)
                {
                    ThucHien.SelectByValue(requestPerforms.First(p => p.UserID == Sessions.UserID)?.PerformID, true);
                }
                else if (Sessions.DepartmentID > 0 && requestPerforms.Count(item => item.DepartmentID == Sessions.DepartmentID) > 0)
                {
                    ThucHien.SelectByValue(requestPerforms.First(p => p.DepartmentID == Sessions.DepartmentID)?.PerformID, true);
                }
                else if (Authenticator.CheckRole(UserRole.ChuyenVienVP, UserRole.LanhDaoDonVi, UserRole.LanhDaoVP, UserRole.Administrator))
                {
                    ThucHien.SelectAll();
                }
            }

            ThoiGianThucHien.Text = DateTime.Now.ToDateString();
        }

        protected override void CreateNewObject()
        {
            report = new Report
            {
                ReportContent = NoiDungThucHien.Text,
                PerformOnDate = ThoiGianThucHien.Text.ToDateTime(),
                RequestID = Parameters.Pid,
                Status = TrangThai.SelectedValue.ToInteger(),
                ReporterName = Sessions.DisplayName,
                DepartmentID = Sessions.DepartmentID
            };

            if (!string.IsNullOrEmpty(VanBanDaBaoCao.SelectedValue.Trim()))
            {
                ExchangeDocServiceSoapClient client = new ExchangeDocServiceSoapClient();
                client.Endpoint.Address = new System.ServiceModel.EndpointAddress(AppSettings.HSCV_Service);
                var document = client.GetVBDiByIDs(new ArrayOfString { VanBanDaBaoCao.SelectedValue.Trim() }).SingleOrDefault();

                ReportServices.Create(report: report, performIds: ThucHien.GetSelectedValues(), fileContent: document?.FileDinhKems?.FirstOrDefault()?.DuLieu, fileName: document?.FileDinhKems?.FirstOrDefault()?.TenFileDinhKem, uploadFolder: AppSettings.UploadFolder, isStaffReport: report.Request.CoQuyenXacNhan, isSendToProvince: true);
            }
            else
            {
                ReportServices.Create(report: report, performIds: ThucHien.GetSelectedValues(), fileContent: FileDinhKem.PostedFile, fileName: FileDinhKem.PostedFile?.FileName, uploadFolder: AppSettings.UploadFolder, isStaffReport: report.Request.CoQuyenXacNhan, isSendToProvince: true);
            }

            ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Nhập báo cáo thực hiện YKCD ({Sessions.DisplayName})", Type = "Nhập báo cáo", Source = AppSettings.AGENCY_NAME });

            Redirector.Redirect(ViewNames.Agency.ThongTinYKCD, "id", Parameters.Pid);
        }

        protected void VanBanDaBaoCao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(VanBanDaBaoCao.SelectedValue.Trim()))
            {
                ExchangeDocServiceSoapClient client = new ExchangeDocServiceSoapClient();
                client.Endpoint.Address = new System.ServiceModel.EndpointAddress(AppSettings.HSCV_Service);
                var document = client.GetVBDiByIDs(new ArrayOfString { VanBanDaBaoCao.SelectedValue.Trim() }).SingleOrDefault();
                NoiDungThucHien.Text = $"Đã có văn bản số {VanBanDaBaoCao.SelectedItem.Text} ban hành ngày {document.NgayPhatHanh} báo cáo UBND tỉnh";
                ThoiGianThucHien.Text = document.NgayPhatHanh;
                TrangThai.SelectByValue(2);
            }
        }
    }
}