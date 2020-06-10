using System;
using Framework.Helper;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Entities;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Public
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
                Department department = DepartmentServices.GetById(Sessions.DepartmentID);

                if (department.Password.Equals(MatKhauCu.Text.Encrypt(AppSettings.Pasword)))
                {
                    department.Password = MatKhauMoi.Text.Encrypt(AppSettings.Pasword);
                    DepartmentServices.Update(department);
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