using Framework.Helper;
using Framework.Web;
using System;
using System.Linq;
using YKCD.SubAgency.Business.Components;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.Public
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
            foreach (var user in UserServices.GetList(new[] { UserRole.LanhDaoVP, UserRole.LanhDaoDonVi }))
            {
                NguoiKy.AddSelectItem(user.FullName, user.UserID.ToString(), user.DepartmentName?.ToUpper());
            }

            ChuyenVien.BindData(UserServices.GetList(new[] { UserRole.ChuyenVienVP, UserRole.ChuyenVien, UserRole.TruongPhongBan, UserRole.LanhDaoVP, UserRole.Administrator }).ToList<object>(), "UserID", "FullName", "DepartmentName");
            DonViThucHien.BindData(DepartmentServices.GetList().ToList<object>(), "DepartmentID", "DepartmentName", "DepartmentGroupName");
        }

        protected override void GetDataList()
        {
            BaseCollection = RequestServices.Search(SoHieuVanBan.Text.Trim(), TrichYeu.Text.Trim(), NguoiKy.GetSelectedValues(),
                ChuyenVien.GetSelectedValues(), NoiDungChiDao.Text.Trim(), DonViThucHien.GetSelectedValues());
        }

        protected void Search_OnClick(object sender, EventArgs e)
        {
            BindDataToListView();
        }
    }
}