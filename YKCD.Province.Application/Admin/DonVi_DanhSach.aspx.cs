using System.Linq;
using System.Web.UI.WebControls;
using Framework.Helper;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Admin
{
    public partial class DonVi_DanhSach : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
            SmallHeader.Text = $"Nhóm đơn vị: {AgencyGroupServices.GetById(Parameters.Id)?.AgencyGroupName}";
        }

        protected override void GetDataList()
        {
            BaseCollection = AgencyServices.GetList(AgencyGroupID: Parameters.Id, SearchText: txtSearchText.Text);
        }

        protected override void DeleteAction(string id)
        {
            AgencyServices.Delete(id.ToInteger());
        }

        protected void lvData_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "SyncData" && e.CommandArgument.ToInteger() > 0)
            {
                int agencyID = e.CommandArgument.ToInteger();

                var agency = AgencyServices.GetById(agencyID);

                if (!string.IsNullOrEmpty(agency.ServiceUrl.Trim()))
                {
                    var requests = RequestServices.GetList(MaDonVi: agencyID).Where(r => r.IsSynced != true);

                    foreach (var request in requests)
                    {
                        var perform = request.Performs?.Where(p => p.AgencyID == agencyID)?.FirstOrDefault();

                        if(perform != null)
                        {
                            AgencyServiceHelper.ReceiveRequest(agency.ServiceUrl, request, perform);
                        }                        
                    }
                }
            }
        }
    }
}