using System;
using Framework.Helper;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.Controls
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