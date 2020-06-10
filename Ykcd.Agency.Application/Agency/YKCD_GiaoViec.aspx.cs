using System.Linq;
using Framework.Helper;
using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Components;
using YKCD.Agency.Business.Entities;
using YKCD.Agency.Business.Services;
using Exceptionless;
using Exceptionless.Models;

namespace YKCD.Agency.Application.Agency
{
    public partial class YKCD_GiaoViec : ManagePageBase
    {
        Request request = new Request();

        protected override void SetBaseControl()
        {
            IntId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "Nhập thông tin giao việc".ToUpper();
            UpdateTitle = "Cập nhật thông tin giao việc".ToUpper();

            ThongTinYKienChiDaoControl.RequestID = Parameters.Pid;
        }

        protected override void BindValueToPageControls()
        {
            //Chọn người yêu cầu là lãnh đạo đơn vị
            foreach (var user in UserServices.GetList(new[] { UserRole.LanhDaoDonVi }))
            {
                LanhDaoYeuCau.AddSelectItem(user.FullName, user.UserID.ToString(), user.DepartmentName?.ToUpper());
            }

            //Chuyên viên theo dõi: tất cả cá nhân trong đơn vị
            foreach (var user in UserServices.GetList())
            {
                ChuyenVienTheoDoi.AddSelectItem(user.FullName, user.UserID.ToString(), user.DepartmentName?.ToUpper() + ".");
            }

            //Đơn vị thực hiện ý kiến chỉ đạo
            DonViThucHien.BindData(DepartmentServices.GetList().ToList<object>(), "DepartmentID", "DepartmentName", "DepartmentGroupName");
        }

        protected override void ShowObjectInformation()
        {

        }

        protected override void SetDefaultValueOnCreate()
        {
            request = RequestServices.GetById(Parameters.Pid);
            ThoiHan.Text = request.RequiredDate.ToDateString();
            ChuyenVienTheoDoi.SelectByValue(Sessions.UserID);
            XacNhanHoanThanh.Checked = true;
        }

        protected override void CreateNewObject()
        {
            request = new Request
            {
                RequestID = Parameters.Pid,
                RequiredDate = ThoiHan.Text.ToDateTime(),
                IsProvinceRequest = RequestServices.GetById(Parameters.Pid).IsProvinceRequest
            };

            RequestServices.Assign(request, LanhDaoYeuCau.SelectedValue.ToInteger(), ChuyenVienTheoDoi.GetSelectedValues(), DonViThucHien.GetSelectedValues(), XacNhanHoanThanh.Checked);

            ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Thực hiện giao việc ({Sessions.DisplayName})", Type = "Giao việc", Source = AppSettings.AGENCY_NAME });

            Redirector.Redirect(ViewNames.Agency.ThongTinYKCD, "id", Parameters.Pid);
        }
    }
}