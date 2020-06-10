using System;
using Framework.Helper;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Controls
{
    public partial class ThongTinVanBanControl : System.Web.UI.UserControl
    {
        public long? DocumentID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DocumentID > 0)
            {
                DocumentDetail.BindData(DocumentServices.GetById(DocumentID).WrapInList());
            }
        }
    }
}