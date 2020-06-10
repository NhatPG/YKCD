using System;
using System.Linq;
using Framework.Helper;
using Framework.Web;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Agency
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
            foreach (var user in UserServices.GetList(new[] { 6, 9 }))
            {
                NguoiKy.AddSelectItem(user.FullName, user.UserID.ToString(), user.DepartmentName.ToUpper());
            }

            ChuyenVienTheoDoi.BindData(UserServices.GetList(new[] { 10 }).ToList<object>(), "UserID", "FullName", "DepartmentName");
            DonViThucHien.BindData(DepartmentServices.GetList().ToList<object>(), "DepartmentID", "DepartmentName", "DepartmentGroupName");
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