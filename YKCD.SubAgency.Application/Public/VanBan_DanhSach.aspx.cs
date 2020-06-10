using Framework.Web;
using System.Linq;
using System.Web.UI.WebControls;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.Public
{
    public partial class VanBan_DanhSach : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;

            if (Parameters.Id > 0)
                SmallHeader.Text = $"Loại văn bản: {DocumentCategoryServices.GetById(Parameters.Id).DocumentCategoryName}";
        }

        protected override void GetDataList()
        {
            BaseCollection = DocumentServices.GetList(categoryId: Parameters.Id, searchText: txtSearchText.Text)
                .OrderByDescending(document => document.ReleaseDate)
                .ToList();
        }
    }
}