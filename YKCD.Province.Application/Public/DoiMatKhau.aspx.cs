using System;
using Framework.Helper;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Public
{
    public partial class DoiMatKhau : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authenticator.CheckLoggedIn())
                Redirector.Redirect(ViewNames.Public.DangNhap);
        }

        protected void btnDoiMatKhau_OnClick(object sender, EventArgs e)
        {
            if (Sessions.UserID > 0)
            {
                User user = UserServices.GetUser(Sessions.User.UserName);

                if (user.Password.Equals(MatKhauCu.Text.Encrypt(AppSettings.Pasword)))
                {
                    user.Password = MatKhauMoi.Text.Encrypt(AppSettings.Pasword);
                    UserServices.Update(user);
                    lblMessage.Text = "Đổi mật khẩu thành công";
                }
                else
                {
                    lblMessage.Text = "Mật khẩu cũ không đúng";
                }
            }
            else if (Sessions.AgencyID > 0)
            {
                Agency agency = AgencyServices.GetById(Sessions.AgencyID);

                if (agency.Password.Equals(MatKhauCu.Text.Encrypt(AppSettings.Pasword)))
                {
                    agency.Password = MatKhauMoi.Text.Encrypt(AppSettings.Pasword);
                    AgencyServices.Update(agency);
                    lblMessage.Text = "Đổi mật khẩu thành công";
                }
                else
                {
                    lblMessage.Text = "Mật khẩu cũ không đúng";
                }
            }
        }
    }
}