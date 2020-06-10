using System.Linq;
using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Public
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