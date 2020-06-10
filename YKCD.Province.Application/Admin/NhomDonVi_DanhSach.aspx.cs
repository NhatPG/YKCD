using Framework.Helper;
using Framework.Web;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Admin
{
    public partial class NhomDonVi_DanhSach : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
        }

        protected override void GetDataList()
        {
            BaseCollection = AgencyGroupServices.GetList();
        }

        protected override void DeleteAction(string id)
        {
            AgencyGroupServices.Delete(id.ToInteger());
        }
    }
}