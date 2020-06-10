using System;
using Framework.Helper;
using YKCD.Agency.Application.Helper;

namespace YKCD.Agency.Application.MasterPages
{
    public partial class Agency : System.Web.UI.MasterPage
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