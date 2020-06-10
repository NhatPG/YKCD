using Framework.Helper;
using System;
using YKCD.SubAgency.Application.Helper;

namespace YKCD.SubAgency.Application.MasterPages
{
    public partial class SubAgency : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authenticator.CheckLoggedIn())
                Authenticator.ValidateSSO();

            if (!Authenticator.CheckLoggedIn())
                Redirector.RedirectToDefault();
        }
    }
}