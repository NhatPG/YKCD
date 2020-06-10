using Framework.Helper;
using Framework.Web;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Admin
{
    public partial class LoaiVanBan_DanhSach : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
        }

        protected override void GetDataList()
        {
            BaseCollection = DocumentCategoryServices.GetList(txtSearchText.Text);
        }

        protected override void DeleteAction(string id)
        {
            DocumentCategoryServices.Delete(id.ToInteger());
        }
    }
}