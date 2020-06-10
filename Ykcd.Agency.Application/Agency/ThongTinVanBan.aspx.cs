using System;
using Framework.Helper;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Agency
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