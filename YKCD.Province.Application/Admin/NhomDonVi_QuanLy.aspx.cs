using Framework.Helper;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Admin
{
    public partial class NhomDonVi_QuanLy : ManagePageBase
    {
        private AgencyGroup item;

        protected override void SetBaseControl()
        {
            IntId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "NHẬP THÔNG TIN NHÓM ĐƠN VỊ MỚI";
            UpdateTitle = "CẬP NHẬT THÔNG TIN NHÓM ĐƠN VỊ";
        }

        protected override void ShowObjectInformation()
        {
            item = AgencyGroupServices.GetById(IntId);
            ViewState["CurrentObject"] = item;
            TenNhomDonVi.Text = item.AgencyGroupName;
            ThuTuHienThi.Text = item.DisplayOrder.ToString();
            HienThiThongKe.Checked = item.IsShowStatistic ?? false;
        }

        protected override void CreateNewObject()
        {
            item = new AgencyGroup();
            item.AgencyGroupName = TenNhomDonVi.Text.Trim();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            item.IsShowStatistic = HienThiThongKe.Checked;
            AgencyGroupServices.Create(item);
        }

        protected override void UpdateObject()
        {
            item = (AgencyGroup)ViewState["CurrentObject"];
            item.AgencyGroupName = TenNhomDonVi.Text.Trim();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            item.IsShowStatistic = HienThiThongKe.Checked;
            AgencyGroupServices.Update(item);
        }

        protected override void ActionAfterFinish()
        {
            Redirector.Redirect(ViewNames.Admin.NhomDonVi_DanhSach);
        }
    }
}