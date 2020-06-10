using System;
using Framework.Helper;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Controls
{
    public partial class ThongTinYKienChiDaoControl : System.Web.UI.UserControl
    {
        public long RequestID;

        protected void Page_Load(object sender, EventArgs e)
        {
            var item = RequestServices.GetById(RequestID);
            RequestDetail.BindData(item.WrapInList());

        }
    }
}