using System.Linq;
using System.Web;
using EsbUsers.Sso;
using Framework.Helper;
using YKCD.Agency.Business.Components;
using YKCD.Agency.Business.Entities;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Helper
{
    public class Authenticator
    {
        public static bool CheckRole(params int[] roles)
        {
            if (Sessions.User != null)
            {
                return roles.Any(item => item == Sessions.User.Role);
            }

            return false;
        }

        public static bool CheckLoggedIn()
        {
            return Sessions.UserID > 0 || Sessions.DepartmentID > 0;
        }

        public static bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;
            username = username.Trim();
            password = Encryptor.Encrypt(password, AppSettings.Pasword);

            User user = UserServices.GetUser(username);

            if (user != null && user.Password.Equals(password))
            {
                Sessions.UserID = user.UserID;
                Sessions.DisplayName = user.FullName;
                Sessions.User = user;
                Sessions.Role = user.Role;

                if (CheckRole(UserRole.TruongPhongBan, UserRole.ChuyenVien))
                {
                    Sessions.DepartmentID = user.DepartmentID;
                }

                return true;
            }

            return false;
        }

        public static bool ValidateDepartment(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;
            username = username.Trim();
            password = Encryptor.Encrypt(password, AppSettings.Pasword);

            Department department = DepartmentServices.GetDepartment(username);

            if (department != null && department.Password.Equals(password))
            {
                Sessions.DepartmentID = department.DepartmentID;
                Sessions.DisplayName = department.DepartmentName;
                return true;
            }

            return false;
        }

        public static bool ValidateSSO(string username, string password)
        {
            if (AppSettings.IS_USE_SSO)
            {
                EsbUsers.Model.ServiceResultMessage<bool> results = ClientSso.Ins?.DangNhap(username, password);

                if (results != null && !results.IsError && results.ResultValue)
                {
                    var user = UserServices.GetUser(ClientSso.Ins.CurrentSessionLoginInfo.TenDangNhap);

                    if (user != null)
                    {
                        Sessions.UserID = user.UserID;
                        Sessions.DisplayName = user.FullName;
                        Sessions.User = user;
                        Sessions.IsCurrentUseSSO = true;
                        Sessions.Role = user.Role;

                        if (CheckRole(UserRole.TruongPhongBan, UserRole.ChuyenVien))
                        {
                            Sessions.DepartmentID = user.DepartmentID;
                        }

                        return true;
                    }

                    return false;
                }
            }
            
            return false;
        }

        public static bool ValidateSSO()
        {
            if (!Authenticator.CheckLoggedIn() && AppSettings.IS_USE_SSO && !Sessions.IsCurrentUseSSO)
            {
                ClientSso.Ins?.XacThucNguoiDung();

                if (ClientSso.Ins?.CurrentSessionLoginInfo == null && Sessions.IsCurrentUseSSO)
                {
                    HttpContext.Current.Session.Abandon();
                    HttpContext.Current.Session.Clear();
                    ClientSso.Ins.DangXuat();
                    return false;
                }
                else if (ClientSso.Ins?.CurrentSessionLoginInfo != null && !Sessions.IsCurrentUseSSO)
                {
                    var user = UserServices.GetUser(ClientSso.Ins.CurrentSessionLoginInfo.TenDangNhap);

                    if (user != null)
                    {
                        Sessions.UserID = user.UserID;
                        Sessions.DisplayName = user.FullName;
                        Sessions.User = user;
                        Sessions.Role = user.Role;
                        Sessions.IsCurrentUseSSO = true;

                        if (CheckRole(UserRole.TruongPhongBan, UserRole.ChuyenVien))
                        {
                            Sessions.DepartmentID = user.DepartmentID;
                        }

                        return true;
                    }
                }
                else if (!Authenticator.CheckLoggedIn() && Sessions.IsCurrentUseSSO)
                {
                    HttpContext.Current.Session.Abandon();
                    HttpContext.Current.Session.Clear();
                    return false;
                }
            }

            return false;
        }
    }
}