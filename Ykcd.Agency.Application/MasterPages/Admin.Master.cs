using System;
using Framework.Helper;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Components;

namespace YKCD.Agency.Application.MasterPages
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Authenticator.ValidateSSO();

            if (!Authenticator.CheckRole(UserRole.Administrator))
                Redirector.RedirectToDefault();
        }
    }
}