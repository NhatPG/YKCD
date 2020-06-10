using System.Linq;
using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Agency
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