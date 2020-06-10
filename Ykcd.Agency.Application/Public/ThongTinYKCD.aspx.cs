using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Public
{
    public partial class ThongTinYKCD : ListViewPageBase
    {
        protected override void SetDefaultValueForListView()
        {
            ThongTinYKienChiDaoControl.RequestID = Parameters.Id;
            BaseListView = lvData;
            GroupColumn = "ReporterName";
            TotalColumns = 5;
        }

        protected override void GetDataList()
        {
            BaseCollection = RequestServices.GetById(Parameters.Id).Reports;
        }
    }
}