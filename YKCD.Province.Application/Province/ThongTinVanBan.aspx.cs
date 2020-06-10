using System;
using Framework.Helper;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Province
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