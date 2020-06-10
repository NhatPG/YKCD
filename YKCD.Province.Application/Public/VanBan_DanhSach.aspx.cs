using System.Linq;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Public
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
            BaseCollection = DocumentServices.GetList(categoryID: Parameters.Id, searchText: txtSearchText.Text)
                .OrderByDescending(document => document.ReleaseDate)
                .ToList();
        }
    }
}