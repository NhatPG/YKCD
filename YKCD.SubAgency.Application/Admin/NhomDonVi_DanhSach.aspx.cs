﻿using Framework.Helper;
using Framework.Web;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.Admin
{
    public partial class NhomDonVi_DanhSach : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
        }

        protected override void GetDataList()
        {
            BaseCollection = DepartmentGroupServices.GetList();
        }

        protected override void DeleteAction(string id)
        {
            foreach (var department in DepartmentServices.GetList(id.ToInteger()))
            {
                DepartmentServices.Delete(department);
            }

            DepartmentGroupServices.Delete(id.ToInteger());
        }
    }
}