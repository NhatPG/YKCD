using System;
using Framework.Helper;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.MasterPages.Controls
{
    public partial class Public_FunctionControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DanhSachNhomDonVi.BindData(AgencyGroupServices.GetList(isShowStatistic: true));
            DanhSachLoaiVanBan.BindData(DocumentCategoryServices.GetList());
        }
    }
}