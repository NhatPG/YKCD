using System;
using EsbUsers.Sso;
using Framework.Helper;
using YKCD.Agency.Application.Helper;

namespace YKCD.Agency.Application.Public
{
    public partial class DangXuat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            ClientSso.Ins.DangXuat();
            Redirector.Redirect(ViewNames.Public.ThongKe_NhomDonVi);
        }
    }
}