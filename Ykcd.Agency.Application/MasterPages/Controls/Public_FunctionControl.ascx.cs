using System;
using Framework.Helper;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.MasterPages.Controls
{
    public partial class Public_FunctionControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DanhSachNhomDonVi.BindData(DepartmentGroupServices.GetList(isShowStatistic: true));
            DanhSachLoaiVanBan.BindData(DocumentCategoryServices.GetList());
        }
    }
}