using Framework.Helper;
using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Entities;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Admin
{
    public partial class NhomDonVi_QuanLy : ManagePageBase
    {
        private DepartmentGroup item;

        protected override void SetBaseControl()
        {
            IntId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "NHẬP THÔNG TIN NHÓM ĐƠN VỊ MỚI";
            UpdateTitle = "CẬP NHẬT THÔNG TIN NHÓM ĐƠN VỊ";
        }

        protected override void ShowObjectInformation()
        {
            item = DepartmentGroupServices.GetById(IntId);
            ViewState["CurrentObject"] = item;
            TenNhomDonVi.Text = item.DepartmentGroupName;
            ThuTuHienThi.Text = item.DisplayOrder.ToString();
            HienThiThongKe.Checked = item.IsShowStatistic ?? false;
        }

        protected override void CreateNewObject()
        {
            item = new DepartmentGroup();
            item.DepartmentGroupName = TenNhomDonVi.Text.Trim();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            item.IsShowStatistic = HienThiThongKe.Checked;
            DepartmentGroupServices.Create(item);
        }

        protected override void UpdateObject()
        {
            item = (DepartmentGroup)ViewState["CurrentObject"];
            item.DepartmentGroupName = TenNhomDonVi.Text.Trim();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            item.IsShowStatistic = HienThiThongKe.Checked;
            DepartmentGroupServices.Update(item);
        }

        protected override void ActionAfterFinish()
        {
            Redirector.Redirect(ViewNames.Admin.NhomDonVi_DanhSach);
        }
    }
}