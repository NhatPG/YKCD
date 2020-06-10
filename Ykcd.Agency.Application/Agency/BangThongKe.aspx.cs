using System;
using Framework.Helper;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Components;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Agency
{
    public partial class BangThongKe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Hiển thị thống kê YKCD của UBND tỉnh đối với lãnh đạo đơn vị, lãnh đạo VP, chuyên viên VP và admin
            if (Authenticator.CheckRole(UserRole.LanhDaoDonVi, UserRole.LanhDaoVP, UserRole.ChuyenVienVP, UserRole.Administrator))
            {                
                UbTinhDetail.BindData(RequestServices.LaySoLieuThongKe(YkcdCuaUbndTinh: true).WrapInList());                
            }

            //Hiển thị thống kê YKCD giao việc đối với lãnh đạo đơn vị, lãnh đạo VP hoặc trưởng phòng ban
            if(Authenticator.CheckRole(UserRole.LanhDaoDonVi, UserRole.LanhDaoVP, UserRole.TruongPhongBan))
            {
                GiaoViecDetail.BindData(RequestServices.LaySoLieuThongKe(MaLanhDao: Sessions.UserID).WrapInList());
            }

            //Hiển thị thống kê YKCD theo dõi đối với lãnh đạo VP, trưởng và chuyên viên
            if(Authenticator.CheckRole(UserRole.LanhDaoVP, UserRole.ChuyenVienVP, UserRole.TruongPhongBan, UserRole.ChuyenVien, UserRole.Administrator))
            {
                TheoDoiDetail.BindData(RequestServices.LaySoLieuThongKe(MaNguoiTheoDoi: Sessions.UserID).WrapInList());
            }

            //Hiển thị thống kê YKCD thực hiện giao cho phòng ban cho tất cả các tài khoản thuộc phòng ban
            if(Sessions.DepartmentID > 0)
            {
                DonViThucHienDetail.BindData(RequestServices.LaySoLieuThongKe(MaDonVi: Sessions.DepartmentID).WrapInList());
            }

            //Hiển thị thống kê YKCD thực hiện giao cho cá nhân cho tất cả các tài khoản cá nhân
            if (Authenticator.CheckRole(UserRole.LanhDaoVP, UserRole.ChuyenVienVP, UserRole.TruongPhongBan, UserRole.ChuyenVien, UserRole.Administrator))
            {
                NguoiThucHienDetail.BindData(RequestServices.LaySoLieuThongKe(MaNguoiThucHien: Sessions.UserID).WrapInList());
            }            
        }
    }
}