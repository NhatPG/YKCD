using Exceptionless;
using Exceptionless.Models;
using Framework.Helper;
using Framework.Web;
using System.Linq;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Components;
using YKCD.SubAgency.Business.Entities;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.SubAgency
{
    public partial class YKCD_QuanLy : ManagePageBase
    {
        Request request = new Request();

        protected override void SetBaseControl()
        {
            LongId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "Nhập thông tin ý kiến chỉ đạo mới".ToUpper();
            UpdateTitle = "Cập nhật thông tin ý kiến chỉ đạo".ToUpper();

            if (Parameters.Id > 0)
            {
                if (RequestServices.GetById(Parameters.Id).DocumentID > 0)
                {
                    ThongTinVanBanControl.DocumentID = RequestServices.GetById(Parameters.Id).DocumentID;
                }
                else
                {
                    ThongTinVanBanControl.Visible = false;
                }
            }
            else
            {
                if (Parameters.Pid > 0)
                {
                    ThongTinVanBanControl.DocumentID = Parameters.Pid;
                }
                else
                {
                    ThongTinVanBanControl.Visible = false;
                }
            }
        }

        protected override void BindValueToPageControls()
        {
            foreach (var user in UserServices.GetList(new[] { UserRole.LanhDaoDonVi, UserRole.LanhDaoVP }))
            {
                LanhDaoYeuCau.AddSelectItem(user.FullName, user.UserID.ToString(), user.DepartmentName?.ToUpper());
            }

            foreach (var user in UserServices.GetList())
            {
                NguoiThucHien.AddSelectItem(user.FullName, user.UserID.ToString(), user.DepartmentName + " .");
            }

            ChuyenVienTheoDoi.BindData(UserServices.GetList(new[] { UserRole.ChuyenVienVP, UserRole.ChuyenVien, UserRole.TruongPhongBan, UserRole.Administrator, UserRole.LanhDaoVP }).ToList<object>(), "UserID", "FullName", "DepartmentName");
            DonViThucHien.BindData(DepartmentServices.GetList().ToList<object>(), "DepartmentID", "DepartmentName", "DepartmentGroupName");
        }

        protected override void ShowObjectInformation()
        {
            request = RequestServices.GetById(IntId);
            NoiDungChiDao.Text = request.RequestContent;
            ThoiHan.Text = request.RequiredDate.ToDateString();

            LanhDaoYeuCau.SelectByValue(request.RequesterID);

            foreach (var tracker in request.Trackers)
            {
                ChuyenVienTheoDoi.SelectByValue(tracker.UserID, multipleChoice: true);
            }

            foreach (var department in request.Departments)
            {
                DonViThucHien.SelectByValue(department.DepartmentID, multipleChoice: true);
            }

            if (request.IsAgencyRequest)
            {
                NoiDungChiDao.Enabled = false;
            }


            if (request.Performs.Count > 0)
                XacNhanHoanThanh.Checked = request.Performs.First().IsFinishedConfirm;
        }

        protected override void SetDefaultValueOnCreate()
        {
            ChuyenVienTheoDoi.SelectByValue(Sessions.UserID);

            if (Parameters.Pid > 0)
            {
                LanhDaoYeuCau.SelectByValue(DocumentServices.GetById(Parameters.Pid)?.SignerID);
            }

            XacNhanHoanThanh.Checked = true;
        }

        protected override void CreateNewObject()
        {
            request = new Request
            {
                DocumentID = Parameters.Pid,
                RequestContent = NoiDungChiDao.Text.Trim(),
                RequiredDate = ThoiHan.Text.ToDateTime(),
                RequesterID = LanhDaoYeuCau.SelectedValue.ToInteger(),
                RequesterName = LanhDaoYeuCau.SelectedItem.Text
            };

            RequestServices.Create(request, ChuyenVienTheoDoi.GetSelectedValues(), DonViThucHien.GetSelectedValues(), NguoiThucHien.GetSelectedValues(), XacNhanHoanThanh.Checked);

            ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Nhập thông tin ykcd mới ({Sessions.DisplayName})", Type = "Nhập YKCD", Source = AppSettings.AGENCY_NAME });

            if (request.DocumentID > 0)
                Redirector.Redirect(ViewNames.SubAgency.ThongTinVanBan, "id", request.DocumentID);
            else
                Redirector.Redirect(ViewNames.SubAgency.ThongTinYKCD, "id", request.RequestID);
        }

        protected override void UpdateObject()
        {
            request = RequestServices.GetById(Parameters.Id);

            if (request != null)
            {
                if (!request.IsAgencyRequest)
                {
                    request.RequestContent = NoiDungChiDao.Text.Trim();
                    request.RequiredDate = ThoiHan.Text.ToDateTime();
                    request.RequesterName = LanhDaoYeuCau.SelectedItem.Text;
                }

                request.RequesterID = LanhDaoYeuCau.SelectedValue.ToInteger();

                RequestServices.Update(request, ChuyenVienTheoDoi.GetSelectedValues(), DonViThucHien.GetSelectedValues(), NguoiThucHien.GetSelectedValues(), XacNhanHoanThanh.Checked);

                ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Cập nhật thông tin ykcd ({Sessions.DisplayName})", Type = "Cập nhật YKCD", Source = AppSettings.SUBAGENCY_NAME });

                if (request.DocumentID > 0)
                    Redirector.Redirect(ViewNames.SubAgency.ThongTinVanBan, "id", request.DocumentID);
                else
                    Redirector.Redirect(ViewNames.SubAgency.ThongTinYKCD, "id", request.RequestID);
            }
        }
    }
}