using Exceptionless;
using Exceptionless.Models;
using Framework.Helper;
using Framework.Web;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Components;
using YKCD.SubAgency.Business.Entities;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.SubAgency
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
            else if (request.IsAgencyRequest)
            {
                ThucHien.Items.Add(new ListItem(text: AppSettings.AGENCY_NAME, value: ""));
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

            ReportServices.Create(report: report, performIds: ThucHien.GetSelectedValues(), fileContent: FileDinhKem.PostedFile, fileName: FileDinhKem.PostedFile?.FileName, uploadFolder: AppSettings.UploadFolder, isStaffReport: report.Request.CoQuyenXacNhan, isSendToAgency: true);

            ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Nhập báo cáo thực hiện YKCD ({Sessions.DisplayName})", Type = "Nhập báo cáo", Source = AppSettings.AGENCY_NAME });

            Redirector.Redirect(ViewNames.SubAgency.ThongTinYKCD, "id", Parameters.Pid);
        }

        protected void VanBanDaBaoCao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(VanBanDaBaoCao.SelectedValue.Trim()))
            {
                
            }
        }
    }
}