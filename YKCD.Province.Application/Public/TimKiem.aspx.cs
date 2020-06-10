using System;
using System.Linq;
using Framework.Helper;
using Framework.Web;
using YKCD.Province.Business.Services;
using YKCD.Province.Business.Components;

namespace YKCD.Province.Application.Public
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
            foreach (var user in UserServices.GetList(new[] { UserRole.LanhDaoUBNDTinh, UserRole.LanhDaoVPUBNDTinh }))
            {
                NguoiKy.AddSelectItem(user.FullName, user.UserID.ToString(), user.DepartmentName?.ToUpper());
            }

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
        }
    }
}