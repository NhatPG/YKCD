using System;
using Framework.Helper;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Components;

namespace YKCD.Province.Application
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
                    Redirector.Redirect(ViewNames.Province.BangThongKe);
            }
            else
            {
                Redirector.Redirect(ViewNames.Public.ThongKe_NhomDonVi);
            }
        }
    }
}