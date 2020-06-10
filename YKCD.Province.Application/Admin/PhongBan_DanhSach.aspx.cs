using Framework.Helper;
using Framework.Web;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Admin
{
    public partial class PhongBan_DanhSach : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
        }

        protected override void GetDataList()
        {
            BaseCollection = DepartmentServices.GetList(searchText: txtSearchText.Text);
        }

        protected override void DeleteAction(string id)
        {
            DepartmentServices.Delete(id.ToInteger());
        }
    }
}