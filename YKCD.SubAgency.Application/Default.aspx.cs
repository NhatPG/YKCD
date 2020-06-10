using System;
using Framework.Helper;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Components;
using EsbUsers.Sso;
using System.Web;
using YKCD.SubAgency.Business.Services;
using System.Linq;

namespace YKCD.SubAgency.Application
{
    public partial class Default : System.Web.UI.Page
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
            else
            {
                Redirector.Redirect(ViewNames.Public.ThongKe_NhomDonVi);
            }
        }
    }    
}