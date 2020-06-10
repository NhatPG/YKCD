using System;
using Framework.Helper;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.Controls
{
    public partial class ThongTinYKienChiDaoControl : System.Web.UI.UserControl
    {
        public long RequestID;

        protected void Page_Load(object sender, EventArgs e)
        {
            RequestDetail.BindData(RequestServices.GetById(RequestID).WrapInList());
        }
    }
}