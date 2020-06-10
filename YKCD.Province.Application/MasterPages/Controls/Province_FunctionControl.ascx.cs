using System;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.MasterPages.Controls
{
    public partial class Province_FunctionControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SoLieuThongKe result = new SoLieuThongKe();

            if (Authenticator.CheckRole(UserRole.ChuyenVienVPUBNDTinh))
            {
                result = RequestServices.LaySoLieuThongKe(MaNguoiTheoDoi: Sessions.UserID);
            }
            else if (Authenticator.CheckRole(UserRole.LanhDaoUBNDTinh, UserRole.LanhDaoVPUBNDTinh))
            {
                result = RequestServices.LaySoLieuThongKe(MaLanhDao: Sessions.UserID);
            }
            else if (Sessions.AgencyID > 0)
            {
                result = RequestServices.LaySoLieuThongKe(MaDonVi: Sessions.AgencyID);
            }

            SoChuaThucHien.Text = result.NotPerform.ToString();
            SoDangThucHien.Text = result.Performing.ToString();
            SoDaHoanThanh.Text = result.Done.ToString();
        }
    }
}