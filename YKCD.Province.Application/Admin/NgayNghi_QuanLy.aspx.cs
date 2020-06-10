using Framework.Helper;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Admin
{
    public partial class NgayNghi_QuanLy : ManagePageBase
    {
        private Holiday item;

        protected override void SetBaseControl()
        {
            IntId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "NHẬP THÔNG TIN NGÀY NGHỈ MỚI";
            UpdateTitle = "CẬP NHẬT THÔNG TIN NGÀY NGHỈ";
        }

        protected override void ShowObjectInformation()
        {
            item = HolidayServices.GetById(IntId);
            ViewState["CurrentObject"] = item;
            TenNgayNghi.Text = item.HolidayName;
            TuNgay.Text = item.FromDate.ToDateString();
            DenNgay.Text = item.ToDate.ToDateString();
        }

        protected override void CreateNewObject()
        {
            item = new Holiday();
            item.HolidayName = TenNgayNghi.Text.Trim();
            item.FromDate = TuNgay.Text.ToDateTime();
            item.ToDate = DenNgay.Text.ToDateTime();
            HolidayServices.Create(item);
        }

        protected override void UpdateObject()
        {
            item = (Holiday)ViewState["CurrentObject"];
            item.HolidayName = TenNgayNghi.Text.Trim();
            item.FromDate = TuNgay.Text.ToDateTime();
            item.ToDate = DenNgay.Text.ToDateTime();
            HolidayServices.Update(item);
        }

        protected override void ActionAfterFinish()
        {
            Redirector.Redirect(ViewNames.Admin.NgayNghi_DanhSach);
        }
    }
}