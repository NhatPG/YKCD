using Framework.Helper;
using Framework.Web;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Admin
{
    public partial class NgayNghi_DanhSach : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
        }

        protected override void GetDataList()
        {
            BaseCollection = HolidayServices.GetList(txtSearchText.Text);
        }

        protected override void DeleteAction(string id)
        {
            HolidayServices.Delete(id.ToInteger());
        }
    }
}