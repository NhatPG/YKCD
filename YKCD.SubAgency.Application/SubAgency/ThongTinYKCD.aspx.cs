using Framework.Web;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.SubAgency
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