using System;
using Framework.Helper;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Components;
using Exceptionless;
using Exceptionless.Models;

namespace YKCD.Province.Application.Public
{
    public partial class DangNhap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authenticator.ValidateSSO())
            {
                if (Authenticator.CheckRole(UserRole.Administrator))
                    Redirector.Redirect(ViewNames.Admin.NguoiSuDung_DanhSach);
                else
                    Redirector.Redirect(ViewNames.Province.BangThongKe);

                ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Đăng nhập SSO thành công ({Sessions.DisplayName})", Type = "Login", Source = "YKCD_UBND" });
            }
        }

        protected void DangNhap_OnClick(object sender, EventArgs e)
        {
            if (IsSSO.Checked)
            {
                if (Authenticator.ValidateSSO(TenDangNhap.Text, MatKhau.Text))
                {
                    if (Authenticator.CheckRole(UserRole.Administrator))
                        Redirector.Redirect(ViewNames.Admin.NguoiSuDung_DanhSach);
                    else
                        Redirector.Redirect(ViewNames.Province.BangThongKe);

                    ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Đăng nhập SSO thành công ({TenDangNhap.Text})", Type = "Login", Source = "YKCD_UBND" });
                }
                else
                {
                    lblMessage.Text = "Tên đăng nhập hay mật khẩu không chính xác";

                    ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Đăng nhập SSO thất bại ({TenDangNhap.Text})", Type = "Login", Source = "YKCD_UBND" });
                }
            }
            else
            {
                if (Authenticator.ValidateUser(TenDangNhap.Text, MatKhau.Text) ||
                    Authenticator.ValidateAgency(TenDangNhap.Text, MatKhau.Text))
                {
                    if (Authenticator.CheckRole(UserRole.Administrator))
                        Redirector.Redirect(ViewNames.Admin.NguoiSuDung_DanhSach);
                    else
                        Redirector.Redirect(ViewNames.Province.BangThongKe);

                    ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Đăng nhập thành công ({TenDangNhap.Text})", Type = "Login", Source = "YKCD_UBND" });
                }
                else
                {
                    lblMessage.Text = "Tên đăng nhập hay mật khẩu không chính xác";

                    ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Đăng nhập thất bại ({TenDangNhap.Text})", Type = "Login", Source = "YKCD_UBND" });
                }
            }
        }
    }
}