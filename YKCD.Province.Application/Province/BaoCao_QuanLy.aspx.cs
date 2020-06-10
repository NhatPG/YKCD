using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Helper;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;
using Exceptionless;
using Exceptionless.Models;

namespace YKCD.Province.Application.Province
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

            btnSave.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        }

        protected override void BindValueToPageControls()
        {
            DonViThucHien.BindData(RequestServices.GetById(Parameters.Pid).Performs, "PerformID", "AgencyName");
        }

        protected override void SetDefaultValueOnCreate()
        {
            if (Authenticator.IsUser)
                DonViThucHien.SelectAll();

            else if (Authenticator.IsAgency)
            {
                DonViThucHien.SelectByValue(
                    RequestServices.GetById(Parameters.Pid).Performs.First(p => p.AgencyID == Sessions.AgencyID).PerformID
                    );
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
                CreatedByName = Sessions.DisplayName,
                AgencyID = Sessions.AgencyID
            };

            List<long> performIds = null;

            if (Authenticator.IsUser)
            {
                performIds = DonViThucHien.GetSelectedValues().ConvertAll(i => (long)i);
            }
            else if (Authenticator.IsAgency)
            {
                performIds = new List<long> { RequestServices.GetById(Parameters.Pid).Performs.First(p => p.AgencyID == Sessions.AgencyID).PerformID };
            }

            ReportServices.Create(report: report, performIds: performIds, fileContent: FileDinhKem.PostedFile, fileName: FileDinhKem.PostedFile.FileName, uploadFolder: AppSettings.UploadFolder, isStaffReport: Authenticator.IsUser);
            ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Nhập báo cáo thực hiện YKCD ({Sessions.DisplayName})", Type = "Nhập báo cáo", Source = "YKCD_UBND" });
            Redirector.Redirect(ViewNames.Province.ThongTinYKCD, "id", Parameters.Pid);
        }
    }
}