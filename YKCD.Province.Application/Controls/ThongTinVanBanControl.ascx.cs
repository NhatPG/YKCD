using System;
using Framework.Helper;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Controls
{
    public partial class ThongTinVanBanControl : System.Web.UI.UserControl
    {
        public long DocumentID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DocumentID > 0)
            {
                DocumentDetail.BindData(DocumentServices.GetById(DocumentID).WrapInList());
            }
        }
    }
}