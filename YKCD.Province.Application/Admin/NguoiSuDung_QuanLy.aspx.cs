using Framework.Helper;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Admin
{
    public partial class NguoiSuDung_QuanLy : ManagePageBase
    {
        private User item;

        protected override void SetBaseControl()
        {
            IntId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "NHẬP THÔNG TIN NGƯỜI SỬ DỤNG MỚI";
            UpdateTitle = "CẬP NHẬT THÔNG TIN NGƯỜI SỬ DỤNG";
        }

        protected override void BindValueToPageControls()
        {
            DonVi.BindData(DepartmentServices.GetList(), "DepartmentID", "DepartmentName");
        }

        protected override void SetDefaultValueOnCreate()
        {
            LoaiTaiKhoan.SelectByValue(UserRole.ChuyenVienVPUBNDTinh);
            ConSuDung.Checked = true;
        }

        protected override void ShowObjectInformation()
        {
            item = UserServices.GetById(LongId);
            ViewState["CurrentObject"] = item;
            HoTen.Text = item.FullName;
            DonVi.SelectByValue(item.DepartmentID);
            TenTaiKhoan.Text = item.UserName;
            ChucVu.Text = item.Position;
            LoaiTaiKhoan.SelectByValue(item.Role);
            ThuTuHienThi.Text = item.DisplayOrder.ToString();
            ConSuDung.Checked = item.IsActive ?? false;
        }

        protected override void CreateNewObject()
        {
            item = new User();
            item.FullName = HoTen.Text.Trim();
            item.DepartmentID = DonVi.SelectedValue.ToInteger();
            item.UserName = TenTaiKhoan.Text.Trim();
            item.Password = MatKhau.Text.Encrypt(AppSettings.Pasword);
            item.Position = ChucVu.Text.Trim();
            item.Role = LoaiTaiKhoan.SelectedValue.ToInteger();
            item.IsActive = ConSuDung.Checked;
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            UserServices.Create(item);
        }

        protected override void UpdateObject()
        {
            item = (User)ViewState["CurrentObject"];
            item.FullName = HoTen.Text.Trim();
            item.DepartmentID = DonVi.SelectedValue.ToInteger();
            item.UserName = TenTaiKhoan.Text.Trim();
            item.Position = ChucVu.Text.Trim();
            item.Role = LoaiTaiKhoan.SelectedValue.ToInteger();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            item.IsActive = ConSuDung.Checked;

            if (!string.IsNullOrEmpty(MatKhau.Text.Trim()))
                item.Password = MatKhau.Text.Encrypt(AppSettings.Pasword);
            
            UserServices.Update(item);
        }

        protected override void ActionAfterFinish()
        {
            Redirector.Redirect(ViewNames.Admin.NguoiSuDung_DanhSach);
        }
    }
}