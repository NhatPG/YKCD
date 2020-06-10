using Framework.Helper;
using Framework.Web;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Admin
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

        protected override void GetDataList()
        {
            BaseCollection = UserServices.GetList(searchText: txtSearchText.Text);
        }

        protected override void DeleteAction(string id)
        {
            UserServices.Delete(id.ToInteger());
        }
    }
}