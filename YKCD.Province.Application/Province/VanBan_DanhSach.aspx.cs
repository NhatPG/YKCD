using System.Linq;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Province
{
    public partial class VanBan_DanhSach : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
        }

        protected override void GetDataList()
        {
            BaseCollection = DocumentServices.GetList(userId: Sessions.UserID, searchText: txtSearchText.Text)
                .OrderByDescending(document => document.ReleaseDate)
                .ToList();
        }
    }
}