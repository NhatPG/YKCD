using Framework.Web;
using System.Linq;
using System.Web.UI.WebControls;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.SubAgency
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