using System;
using System.Collections.Generic;
using Framework.Helper;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;
using Exceptionless;
using Exceptionless.Models;

namespace YKCD.Province.Application.Province
{
    public partial class XacNhanHoanThanh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var perform = PerformServices.GetById(Parameters.Id);

                if (perform.Status == 3)
                {
                    TrangThai.SelectByValue(2);
                    NgayHoanThanh.Text = perform.FinishedOnDate.ToDateString();
                }
                else
                {
                    TrangThai.SelectByValue(PerformServices.GetById(Parameters.Id).Status);
                    NgayHoanThanh.Text = DateTime.Now.ToDateString();
                }
            }

            btnSave.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var perform = PerformServices.GetById(Parameters.Id);
            var report = new Report();

            if (!string.IsNullOrEmpty(BaoCaoThucHien.Text.Trim()))
            {
                report = new Report
                {
                    RequestID = perform.RequestID,
                    CreatedByName = Sessions.DisplayName,
                    ReportContent = BaoCaoThucHien.Text.Trim(),
                    Status = TrangThai.SelectedValue.ToInteger(),
                    PerformOnDate = NgayHoanThanh.Text.ToDateTime(),
                    AgencyID = perform.AgencyID
                };
            }
            else if (TrangThai.SelectedValue.ToInteger() != 2 && string.IsNullOrEmpty(BaoCaoThucHien.Text.Trim()))
            {
                report = new Report
                {
                    RequestID = perform.RequestID,
                    CreatedByName = Sessions.DisplayName,
                    ReportContent = $"Chuyển trạng thái thực hiện của {perform.AgencyName} thành {TrangThai.SelectedItem.Text.ToUpper()}",
                    Status = TrangThai.SelectedValue.ToInteger(),
                    PerformOnDate = NgayHoanThanh.Text.ToDateTime(),
                    AgencyID = perform.AgencyID
                };
            }
            else if (perform.Status == 3 && TrangThai.SelectedValue.ToInteger() == 2 &&
                     string.IsNullOrEmpty(BaoCaoThucHien.Text.Trim()))
            {
                report = new Report
                {
                    RequestID = perform.RequestID,
                    CreatedByName = Sessions.DisplayName,
                    ReportContent = $"Xác nhận trạng thái thực hiện của {perform.AgencyName} thành {TrangThai.SelectedItem.Text.ToUpper()}",
                    Status = TrangThai.SelectedValue.ToInteger(),
                    PerformOnDate = NgayHoanThanh.Text.ToDateTime(),
                    AgencyID = perform.AgencyID
                };
            }
            else
            {
                report = new Report
                {
                    RequestID = perform.RequestID,
                    CreatedByName = Sessions.DisplayName,
                    ReportContent = BaoCaoThucHien.Text.Trim(),
                    Status = TrangThai.SelectedValue.ToInteger(),
                    PerformOnDate = NgayHoanThanh.Text.ToDateTime(),
                    AgencyID = perform.AgencyID
                };
            }

            ReportServices.Create(report: report, performIds: new List<long> { Parameters.Id }, fileContent: null, fileName: "", uploadFolder: AppSettings.UploadFolder, isStaffReport: true);

            ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Chuyển trạng thái thực hiện của {perform.AgencyName} thành {TrangThai.SelectedItem.Text.ToUpper()} ({Sessions.DisplayName})", Type = "Chuyển trạng thái YKCD", Source = "YKCD_UBND" });

            Redirector.Redirect(ViewNames.Province.ThongTinYKCD, "id", perform.RequestID);
        }
    }
}