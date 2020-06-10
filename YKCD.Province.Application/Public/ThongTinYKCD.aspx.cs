using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Public
{
    public partial class ThongTinYKCD : ListViewPageBase
    {
        protected override void SetDefaultValueForListView()
        {
            ThongTinYKienChiDaoControl.RequestID = Parameters.Id;
            BaseListView = lvData;
            GroupColumn = "CreatedByName";
            TotalColumns = 5;
        }

        protected override void GetDataList()
        {
            BaseCollection = RequestServices.GetById(Parameters.Id).Reports;
        }
    }
}