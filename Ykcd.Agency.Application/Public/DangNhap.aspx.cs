using System;
using Framework.Helper;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Components;
using Exceptionless;
using Exceptionless.Models;

namespace YKCD.Agency.Application.Public
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
                    Redirector.Redirect(ViewNames.Agency.BangThongKe);

                ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Đăng nhập SSO thành công ({Sessions.DisplayName})", Type = "Login", Source = AppSettings.AGENCY_NAME });
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
                        Redirector.Redirect(ViewNames.Agency.BangThongKe);

                    ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Đăng nhập SSO thành công ({Sessions.DisplayName})", Type = "Login", Source = AppSettings.AGENCY_NAME });
                }
                else
                {
                    lblMessage.Text = "Tên đăng nhập hay mật khẩu không chính xác";

                    ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Đăng nhập không thành công", Type = "Login", Source = AppSettings.AGENCY_NAME });
                }
            }
            else
            {
                if (Authenticator.ValidateUser(TenDangNhap.Text, MatKhau.Text) ||
                    Authenticator.ValidateDepartment(TenDangNhap.Text, MatKhau.Text))
                {
                    if (Authenticator.CheckRole(UserRole.Administrator))
                        Redirector.Redirect(ViewNames.Admin.NguoiSuDung_DanhSach);
                    else
                        Redirector.Redirect(ViewNames.Agency.BangThongKe);

                    ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Đăng nhập thành công ({Sessions.DisplayName})", Type = "Login", Source = AppSettings.AGENCY_NAME });
                }
                else
                {
                    lblMessage.Text = "Tên đăng nhập hay mật khẩu không chính xác";

                    ExceptionlessClient.Default.SubmitEvent(new Event { Message = $"Đăng nhập không thành công", Type = "Login", Source = AppSettings.AGENCY_NAME });
                }
            }
        }
    }
}