using Framework.Helper;
using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Admin
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