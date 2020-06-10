using Framework.Helper;
using Framework.Web;
using System;
using System.Linq;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.Admin
{
    public partial class NguoiSuDung_DanhSach : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
            GroupColumn = "DepartmentName";
            TotalColumns = 5;
        }

        protected override void BindOtherValue()
        {
            DonVi.BindData(DepartmentServices.GetList().ToList<object>(), "DepartmentID", "DepartmentName", "DepartmentGroupName");
        }

        protected override void GetDataList()
        {
            BaseCollection = UserServices.GetList(departmentId: DonVi.SelectedValue.ToInteger(), searchText: txtSearchText.Text);
        }

        protected override void DeleteAction(string id)
        {
            UserServices.Delete(id.ToInteger());
        }

        protected void DonVi_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataToListView();
        }
    }
}