using Framework.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.SubAgency
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