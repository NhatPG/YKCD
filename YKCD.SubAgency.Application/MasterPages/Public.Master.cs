using System;
using Framework.Helper;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Components;

namespace YKCD.SubAgency.Application.MasterPages
{
    public partial class Public : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authenticator.ValidateSSO())
            {
                if (Authenticator.CheckRole(UserRole.Administrator))
                    Redirector.Redirect(ViewNames.Admin.NguoiSuDung_DanhSach);
                else
                    Redirector.Redirect(ViewNames.SubAgency.BangThongKe);
            }
        }
    }
}