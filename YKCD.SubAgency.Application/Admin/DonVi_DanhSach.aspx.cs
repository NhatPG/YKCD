using Framework.Helper;
using Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.Admin
{
    public partial class DonVi_DanhSach : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
            SmallHeader.Text = $"Nhóm đơn vị: {DepartmentGroupServices.GetById(Parameters.Id).DepartmentGroupName}";
        }

        protected override void GetDataList()
        {
            BaseCollection = DepartmentServices.GetList(DepartmentGroupID: Parameters.Id, searchText: txtSearchText.Text);
        }

        protected override void DeleteAction(string id)
        {
            DepartmentServices.Delete(id.ToInteger());
        }
    }
}