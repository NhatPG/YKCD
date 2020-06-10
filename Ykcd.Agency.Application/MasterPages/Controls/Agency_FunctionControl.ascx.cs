using System;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Components;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.MasterPages.Controls
{
    public partial class Agency_FunctionControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SoLieuThongKe result = new SoLieuThongKe();

            if (Authenticator.CheckRole(UserRole.ChuyenVienVP))
            {
                result = RequestServices.LaySoLieuThongKe(MaNguoiTheoDoi: Sessions.UserID);
            }
            else if (Authenticator.CheckRole(UserRole.LanhDaoDonVi, UserRole.LanhDaoVP))
            {
                result = RequestServices.LaySoLieuThongKe(MaLanhDao: Sessions.UserID);
            }
            else if (Sessions.DepartmentID > 0)
            {
                result = RequestServices.LaySoLieuThongKe(MaDonVi: Sessions.DepartmentID);
            }

            SoChuaThucHien.Text = result.NotPerform.ToString();
            SoDangThucHien.Text = result.Performing.ToString();
            SoDaHoanThanh.Text = result.Done.ToString();
        }
    }
}