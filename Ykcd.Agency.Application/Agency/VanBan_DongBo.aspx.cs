using System;
using System.Linq;
using System.ServiceModel;
using System.Web.UI.WebControls;
using Framework.Helper;
using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Application.HscvService;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Agency
{
    public partial class VanBan_DongBo : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
        }

        protected override void BindOtherValue()
        {
            TuNgay.Text = DateTime.Now.AddDays(-30).ToDateString();
            DenNgay.Text = DateTime.Now.ToDateString();
            ChiLayVbCaNhan.Checked = true;
        }

        protected override void GetDataList()
        {
            ExchangeDocServiceSoapClient client = new ExchangeDocServiceSoapClient();
            client.Endpoint.Address = new EndpointAddress(AppSettings.HSCV_Service);

            if(ChiLayVbCaNhan.Checked)
            {
                BaseCollection = client.GetVBDiHasIdeaLeader(TuNgay.Text.ToDateTime(), DenNgay.Text.ToDateTime())
                .Where(item => item.NguoiSoanThao != null && item.NguoiSoanThao.ToUnsign().ToUpper().Equals(Sessions.DisplayName.ToUnsign().ToUpper()))
                .Where(item => DocumentServices.GetByVbdhCode(item.MaVBDi) == null)
                .ToList();
            }
            else
            {
                BaseCollection = client.GetVBDiHasIdeaLeader(TuNgay.Text.ToDateTime(), DenNgay.Text.ToDateTime())                
                .Where(item => DocumentServices.GetByVbdhCode(item.MaVBDi) == null)
                .ToList();
            }
        }

        protected override void AdditionActionOnItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        protected void btnThongKe_Click(object sender, EventArgs e)
        {
            BindDataToListView();
        }
    }
}