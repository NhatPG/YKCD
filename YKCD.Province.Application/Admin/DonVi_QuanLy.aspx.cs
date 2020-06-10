using Framework.Helper;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Admin
{
    public partial class DonVi_QuanLy : ManagePageBase
    {
        private Agency item;

        protected override void SetBaseControl()
        {
            IntId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "NHẬP THÔNG TIN ĐƠN VỊ MỚI";
            UpdateTitle = "CẬP NHẬT THÔNG TIN ĐƠN VỊ";
        }

        protected override void BindValueToPageControls()
        {
            NhomDonVi.BindData(AgencyGroupServices.GetList(), "AgencyGroupID", "AgencyGroupName");
        }

        protected override void SetDefaultValueOnCreate()
        {
            NhomDonVi.SelectByValue(Parameters.Pid);
        }

        protected override void ShowObjectInformation()
        {
            item = AgencyServices.GetById(IntId);
            ViewState["CurrentObject"] = item;
            TenDonVi.Text = item.AgencyName;
            TenDangNhap.Text = item.UserName;
            ThuTuHienThi.Text = item.DisplayOrder.ToString();
            HienThiThongKe.Checked = item.IsShowStatistic ?? false;
            NhomDonVi.SelectByValue(item.AgencyGroupID);
            ServiceUrl.Text = item.ServiceUrl;
        }

        protected override void CreateNewObject()
        {
            item = new Agency();
            item.AgencyName = TenDonVi.Text.Trim();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            item.IsShowStatistic = HienThiThongKe.Checked;
            item.AgencyGroupID = NhomDonVi.SelectedValue.ToInteger();
            item.UserName = TenDangNhap.Text.Trim();
            item.Password = MatKhau.Text.Encrypt(AppSettings.Pasword);
            item.ServiceUrl = ServiceUrl.Text;
            AgencyServices.Create(item);
        }

        protected override void UpdateObject()
        {
            item = (Agency)ViewState["CurrentObject"];
            item.AgencyName = TenDonVi.Text.Trim();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            item.IsShowStatistic = HienThiThongKe.Checked;
            item.AgencyGroupID = NhomDonVi.SelectedValue.ToInteger();
            item.UserName = TenDangNhap.Text.Trim();

            if (!string.IsNullOrEmpty(MatKhau.Text))
                item.Password = MatKhau.Text.Encrypt(AppSettings.Pasword);
            item.ServiceUrl = ServiceUrl.Text;
            AgencyServices.Update(item);
        }

        protected override void ActionAfterFinish()
        {
            Redirector.Redirect(ViewNames.Admin.DonVi_DanhSach, "id", item.AgencyGroupID);
        }
    }
}