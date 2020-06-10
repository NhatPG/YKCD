using System;
using System.Collections.Generic;
using Framework.Helper;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Entities;
using YKCD.Agency.Business.Services;
using Exceptionless;
using Exceptionless.Models;

namespace YKCD.Agency.Application.Agency
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
            var report = new Report();

            if (perform != null)
            {
                if (!string.IsNullOrEmpty(BaoCaoThucHien.Text.Trim()))
                {
                    report = new Report
                    {
                        RequestID = perform.RequestID,
                        ReporterName = Sessions.DisplayName,
                        ReportContent = BaoCaoThucHien.Text.Trim(),
                        Status = TrangThai.SelectedValue.ToInteger(),
                        PerformOnDate = NgayHoanThanh.Text.ToDateTime(),
                        DepartmentID = perform.DepartmentID
                    };
                }
                else if (TrangThai.SelectedValue.ToInteger() != 2 && string.IsNullOrEmpty(BaoCaoThucHien.Text.Trim()))
                {
                    report = new Report
                    {
                        RequestID = perform.RequestID,
                        ReporterName = Sessions.DisplayName,
                        ReportContent = $"Chuyển trạng thái thực hiện của {perform.DepartmentName} thành {TrangThai.SelectedItem.Text.ToUpper()}",
                        Status = TrangThai.SelectedValue.ToInteger(),
                        PerformOnDate = NgayHoanThanh.Text.ToDateTime(),
                        DepartmentID = perform.DepartmentID
                    };
                }
                else if (perform.Status == 3 && TrangThai.SelectedValue.ToInteger() == 2 &&
                         string.IsNullOrEmpty(BaoCaoThucHien.Text.Trim()))
                {
                    report = new Report
                    {
                        RequestID = perform.RequestID,
                        ReporterName = Sessions.DisplayName,
                        ReportContent = $"Xác nhận trạng thái thực hiện của {perform.DepartmentName} thành {TrangThai.SelectedItem.Text.ToUpper()}",
                        Status = TrangThai.SelectedValue.ToInteger(),
                        PerformOnDate = NgayHoanThanh.Text.ToDateTime(),
                        DepartmentID = perform.DepartmentID
                    };
                }
                else
                {
                    report = new Report
                    {
                        RequestID = perform.RequestID,
                        ReporterName = Sessions.DisplayName,
                        ReportContent = BaoCaoThucHien.Text.Trim(),
                        Status = TrangThai.SelectedValue.ToInteger(),
                        PerformOnDate = NgayHoanThanh.Text.ToDateTime(),
                        DepartmentID = perform.DepartmentID
                    };
                }

                ReportServices.Create(report: report, performIds: new List<int> { Parameters.Id }, fileContent: null, fileName: "", uploadFolder: AppSettings.UploadFolder, isStaffReport: true, isSendToProvince: true);

                ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Chuyển trạng thái thực hiện của {perform.DepartmentName} thành {TrangThai.SelectedItem.Text.ToUpper()} ({Sessions.DisplayName})", Type = "Chuyển trạng thái YKCD", Source = AppSettings.AGENCY_NAME });

                Redirector.Redirect(ViewNames.Agency.ThongTinYKCD, "id", perform.RequestID);
            }
        }
    }
}