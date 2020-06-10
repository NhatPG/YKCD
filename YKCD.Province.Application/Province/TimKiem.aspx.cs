using System;
using System.Linq;
using Framework.Helper;
using Framework.Web;
using YKCD.Province.Business.Services;
using YKCD.Province.Application.Helper;
using Exceptionless;
using Exceptionless.Models;

namespace YKCD.Province.Application.Province
{
    public partial class TimKiem : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
        }

        protected override void BindOtherValue()
        {
            NguoiKy.BindData(UserServices.GetList(new[] { 3, 4 }).ToList<object>(), "UserID", "FullName", "DepartmentName");
            ChuyenVienTheoDoi.BindData(UserServices.GetList(new[] { 5 }).ToList<object>(), "UserID", "FullName", "DepartmentName");
            DonViThucHien.BindData(AgencyServices.GetList().ToList<object>(), "AgencyID", "AgencyName", "AgencyGroupName");
        }

        protected override void GetDataList()
        {
            BaseCollection = RequestServices.Search(SoHieuVanBan.Text.Trim(), TrichYeu.Text.Trim(), NguoiKy.GetSelectedValues(),
                ChuyenVienTheoDoi.GetSelectedValues(), NoiDungChiDao.Text.Trim(), DonViThucHien.GetSelectedValues());
        }

        protected void Search_OnClick(object sender, EventArgs e)
        {
            BindDataToListView();

            ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Tìm kiếm KYCD ({Sessions.DisplayName})", Type = "Tìm kiếm KYCD", Source = "YKCD_UBND" });
        }
    }
}