using System;
using Framework.Helper;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Components;

namespace YKCD.Province.Application.MasterPages
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Authenticator.ValidateSSO();

            if (!Authenticator.CheckRole(UserRole.Administrator))
            {
                Redirector.Redirect(ViewNames.Public.ThongKe_NhomDonVi);
            }
        }
    }
}