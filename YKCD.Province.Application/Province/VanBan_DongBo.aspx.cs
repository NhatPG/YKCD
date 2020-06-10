using System;
using System.Linq;
using System.ServiceModel;
using System.Web.UI.WebControls;
using Framework.Helper;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Application.HscvService;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Province
{
    public partial class VanBan_DongBo : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
        }

        protected override void GetDataList()
        {
            ExchangeDocServiceSoapClient client = new ExchangeDocServiceSoapClient();
            client.Endpoint.Address = new EndpointAddress(AppSettings.HSCV_Service);

            BaseCollection = client.GetVBDiHasIdeaLeader(DateTime.Now.AddDays(-30), DateTime.Now)
                ?.Where(item => !string.IsNullOrEmpty(item.NguoiSoanThao) && item.NguoiSoanThao.ToUnsign().ToUpper().Equals(Sessions.DisplayName.ToUnsign().ToUpper()))
                ?.Where(item => DocumentServices.GetByVbdhCode(item.MaVBDi) == null)
                ?.ToList();
        }

        protected override void AdditionActionOnItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }
    }
}