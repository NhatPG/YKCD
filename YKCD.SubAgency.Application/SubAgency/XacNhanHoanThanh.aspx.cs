using Exceptionless;
using Exceptionless.Models;
using Framework.Helper;
using System;
using System.Collections.Generic;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Entities;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.SubAgency
{
    public partial class XacNhanHoanThanh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NgayHoanThanh.Text = DateTime.Now.ToDateString();

                if (PerformServices.GetById(Parameters.Id).Status == 3)
                {
                    TrangThai.SelectByValue(2);
                }
                else
                {
                    TrangThai.SelectByValue(PerformServices.GetById(Parameters.Id).Status);
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var perform = PerformServices.GetById(Parameters.Id);

            if (perform != null)
            {
                var report = new Report
                {
                    RequestID = perform.RequestID,
                    ReporterName = Sessions.DisplayName,
                    ReportContent = BaoCaoThucHien.Text.Trim(),
                    Status = TrangThai.SelectedValue.ToInteger(),
                    PerformOnDate = NgayHoanThanh.Text.ToDateTime(),
                    DepartmentID = perform.DepartmentID
                };

                ReportServices.Create(report: report, performIds: new List<int> { Parameters.Id }, fileContent: null, fileName: "", uploadFolder: AppSettings.UploadFolder, isStaffReport: true, isSendToAgency: true);

                ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Chuyển trạng thái thực hiện của {perform.DepartmentName} thành {TrangThai.SelectedItem.Text.ToUpper()} ({Sessions.DisplayName})", Type = "Chuyển trạng thái YKCD", Source = AppSettings.AGENCY_NAME });

                Redirector.Redirect(ViewNames.SubAgency.ThongTinYKCD, "id", perform.RequestID);
            }
        }
    }
}