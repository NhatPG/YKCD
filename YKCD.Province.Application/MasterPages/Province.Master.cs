using System;
using Framework.Helper;
using YKCD.Province.Application.Helper;

namespace YKCD.Province.Application.MasterPages
{
    public partial class Province : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Authenticator.ValidateSSO();

            if (!Authenticator.CheckLoggedIn())
            {
                Redirector.Redirect(ViewNames.Public.ThongKe_NhomDonVi);
            }
        }
    }
}