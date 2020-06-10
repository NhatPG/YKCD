using Framework.Helper;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Admin
{
    public partial class PhongBan_QuanLy : ManagePageBase
    {
        private Department item;

        protected override void SetBaseControl()
        {
            IntId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "NHẬP THÔNG TIN PHÒNG BAN MỚI";
            UpdateTitle = "CẬP NHẬT THÔNG TIN PHÒNG BAN";
        }

        protected override void ShowObjectInformation()
        {
            item = DepartmentServices.GetById(IntId);
            ViewState["CurrentObject"] = item;
            TenPhongBan.Text = item.DepartmentName;
            ThuTuHienThi.Text = item.DisplayOrder.ToString();
            HienThiThongKe.Checked = item.IsShowStatistic ?? false;
        }

        protected override void CreateNewObject()
        {
            item = new Department();
            item.DepartmentName = TenPhongBan.Text.Trim();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            item.IsShowStatistic = HienThiThongKe.Checked;
            DepartmentServices.Create(item);
        }

        protected override void UpdateObject()
        {
            item = (Department)ViewState["CurrentObject"];
            item.DepartmentName = TenPhongBan.Text.Trim();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            item.IsShowStatistic = HienThiThongKe.Checked;
            DepartmentServices.Update(item);
        }

        protected override void ActionAfterFinish()
        {
            Redirector.Redirect(ViewNames.Admin.PhongBan_DanhSach);
        }
    }
}