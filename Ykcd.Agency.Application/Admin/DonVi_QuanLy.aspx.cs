using Framework.Helper;
using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Entities;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Admin
{
    public partial class DonVi_QuanLy : ManagePageBase
    {
        private Department item;

        protected override void SetBaseControl()
        {
            IntId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "NHẬP THÔNG TIN ĐƠN VỊ MỚI";
            UpdateTitle = "CẬP NHẬT THÔNG TIN ĐƠN VỊ";
        }

        protected override void BindValueToPageControls()
        {
            NhomDonVi.BindData(DepartmentGroupServices.GetList(), "DepartmentGroupID", "DepartmentGroupName");
        }

        protected override void SetDefaultValueOnCreate()
        {
            NhomDonVi.SelectByValue(Parameters.Pid);
            HienThiThongKe.Checked = true;
        }

        protected override void ShowObjectInformation()
        {
            item = DepartmentServices.GetById(IntId);
            ViewState["CurrentObject"] = item;
            TenDonVi.Text = item.DepartmentName;
            TenDangNhap.Text = item.UserName;
            ThuTuHienThi.Text = item.DisplayOrder.ToString();
            HienThiThongKe.Checked = item.IsShowStatistic;
            NhomDonVi.SelectByValue(item.DepartmentGroupID);
            SoDienThoai.Text = item.PhoneNumber;
        }

        protected override void CreateNewObject()
        {
            item = new Department();
            item.DepartmentName = TenDonVi.Text.Trim();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            item.IsShowStatistic = HienThiThongKe.Checked;
            item.DepartmentGroupID = NhomDonVi.SelectedValue.ToInteger();
            item.UserName = TenDangNhap.Text.Trim();
            item.Password = MatKhau.Text.Encrypt(AppSettings.Pasword);
            item.PhoneNumber = SoDienThoai.Text.Trim();
            DepartmentServices.Create(item);
        }

        protected override void UpdateObject()
        {
            item = (Department)ViewState["CurrentObject"];
            item.DepartmentName = TenDonVi.Text.Trim();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            item.IsShowStatistic = HienThiThongKe.Checked;
            item.DepartmentGroupID = NhomDonVi.SelectedValue.ToInteger();
            item.UserName = TenDangNhap.Text.Trim();
            item.PhoneNumber = SoDienThoai.Text.Trim();
            if (!string.IsNullOrEmpty(MatKhau.Text))
                item.Password = MatKhau.Text.Encrypt(AppSettings.Pasword);
            DepartmentServices.Update(item);
        }

        protected override void ActionAfterFinish()
        {
            Redirector.Redirect(ViewNames.Admin.DonVi_DanhSach, "id", item.DepartmentGroupID);
        }
    }
}