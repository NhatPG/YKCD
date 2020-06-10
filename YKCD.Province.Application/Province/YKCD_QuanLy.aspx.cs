using System;
using System.Linq;
using Framework.Helper;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;
using Exceptionless;
using Exceptionless.Models;

namespace YKCD.Province.Application.Province
{
    public partial class YKCD_QuanLy : ManagePageBase
    {
        Request request = new Request();

        protected override void SetBaseControl()
        {
            IntId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "Nhập thông tin ý kiến chỉ đạo mới".ToUpper();
            UpdateTitle = "Cập nhật thông tin ý kiến chỉ đạo".ToUpper();

            if (Parameters.Id > 0)
            {
                ThongTinVanBanControl.DocumentID = RequestServices.GetById(Parameters.Id).DocumentID;
            }
            else
            {
                ThongTinVanBanControl.DocumentID = Parameters.Pid;
            }

            btnSave.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        }

        protected override void BindValueToPageControls()
        {
            ChuyenVienTheoDoi.BindData(UserServices.GetList(new[] { UserRole.ChuyenVienVPUBNDTinh }).ToList<object>(), "UserID", "FullName", "DepartmentName");
            DonViThucHien.BindData(AgencyServices.GetList().ToList<object>(), "AgencyID", "AgencyName", "AgencyGroupName");
        }

        protected override void ShowObjectInformation()
        {
            request = RequestServices.GetById(IntId);
            NoiDungChiDao.Text = request.RequestContent;
            ThoiHan.Text = request.RequiredDate.ToDateString();

            foreach (var tracker in request.Trackers)
            {
                ChuyenVienTheoDoi.SelectByValue(tracker.UserID, multipleChoice: true);
            }

            foreach (var agency in request.Agencies)
            {
                DonViThucHien.SelectByValue(agency.AgencyID, multipleChoice: true);
            }
        }

        protected override void SetDefaultValueOnCreate()
        {
            ChuyenVienTheoDoi.SelectByValue(Sessions.UserID);
        }

        protected override void CreateNewObject()
        {
            request = new Request
            {
                DocumentID = Parameters.Pid,
                RequestContent = NoiDungChiDao.Text.Trim(),
                RequiredDate = ThoiHan.Text.ToDateTime(),
                RequesterID = DocumentServices.GetById(Parameters.Pid).SignerID,
                RequesterName = DocumentServices.GetById(Parameters.Pid).SignerName
            };

            RequestServices.Create(request, ChuyenVienTheoDoi.GetSelectedValues(), DonViThucHien.GetSelectedValues());

            ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Nhập thông tin ykcd mới ({Sessions.DisplayName})", Type = "Nhập YKCD", Source = "YKCD_UBND" });
        }

        protected override void UpdateObject()
        {
            request = RequestServices.GetById(Parameters.Id);

            if (request != null)
            {
                if(request.RequiredDate != ThoiHan.Text.ToDateTime())
                {
                    Report report = new Report
                    {
                        RequestID = request.RequestID,
                        CreatedByName = Sessions.DisplayName,
                        ReportContent = $"Cập nhật thời hạn thực hiện từ ngày {request.RequiredDate.ToDateString()} thành ngày {ThoiHan.Text}",
                        Status = request.Status,
                        PerformOnDate = DateTime.Now
                    };

                    ReportServices.Create(report: report, performIds: null, fileContent: null, fileName: "", uploadFolder: AppSettings.UploadFolder, isStaffReport: true);
                }

                request.RequestContent = NoiDungChiDao.Text.Trim();
                request.RequiredDate = ThoiHan.Text.ToDateTime();

                RequestServices.Update(request, ChuyenVienTheoDoi.GetSelectedValues(), DonViThucHien.GetSelectedValues());

                ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Cập nhật thông tin ykcd ({Sessions.DisplayName})", Type = "Cập nhật YKCD", Source = "YKCD_UBND" });

                Redirector.Redirect(ViewNames.Province.ThongTinVanBan, "id", request.DocumentID);
            }
        }

        protected override void ActionAfterFinish()
        {
            Redirector.Redirect(ViewNames.Province.ThongTinVanBan, "id", request.DocumentID);
        }

        protected void btnSaveAndCreate_Click(object sender, System.EventArgs e)
        {
            if (IntId > 0 || LongId > 0)
            {
                UpdateObject();
            }
            else
            {
                CreateNewObject();
            }

            Redirector.Redirect(ViewNames.Province.YKCD_QuanLy, "pid", request.DocumentID);
        }
    }
}