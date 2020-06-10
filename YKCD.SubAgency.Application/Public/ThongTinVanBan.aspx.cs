using Framework.Helper;
using System;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.Public
{
    public partial class ThongTinVanBan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Parameters.Id > 0)
            {
                ThongTinVanBanControl.DocumentID = Parameters.Id;
                lvData.BindData(DocumentServices.GetById(Parameters.Id).Requests);
            }
        }
    }
}