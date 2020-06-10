using EsbUsers.Sso;
using Framework.Helper;
using System.Linq;
using System.Web;
using Framework.Util;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Helper
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
            return Sessions.UserID > 0 || Sessions.AgencyID > 0;
        }

        public static bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                return false;
            username = username.Trim();
            password = Encryptor.Encrypt(password, AppSettings.Pasword);

            User user = UserServices.GetUser(username);

            if (user != null && user.Password.Equals(password))
            {
                Sessions.UserID = user.UserID;
                Sessions.Role = user.Role;
                Sessions.DisplayName = user.FullName;
                HttpContext.Current.Session[Sessions.DisplayName] = user.FullName;
                Sessions.User = user;
                return true;
            }

            return false;
        }

        public static bool ValidateAgency(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;
            username = username.Trim();
            password = Encryptor.Encrypt(password, AppSettings.Pasword);

            Agency agency = AgencyServices.GetAgency(userName: username);

            if (agency != null && agency.Password.Equals(password))
            {
                Sessions.AgencyID = agency.AgencyID;
                Sessions.DisplayName = agency.AgencyName;
                return true;
            }

            return false;
        }

        public static bool ValidateSSO(string username, string password)
        {
            EsbUsers.Model.ServiceResultMessage<bool> results = ClientSso.Ins.DangNhap(username, password);

            if (!results.IsError && results.ResultValue)
            {
                var user = UserServices.GetUser(ClientSso.Ins.CurrentSessionLoginInfo.TenDangNhap);

                if (user != null)
                {
                    Sessions.UserID = user.UserID;
                    Sessions.DisplayName = user.FullName;
                    Sessions.User = user;
                    Sessions.Role = user.Role;
                    Sessions.IsCurrentUseSSO = true;
                    return true;
                }

                return false;
            }
            return false;
        }

        public static bool ValidateSSO()
        {
            ClientSso.Ins.XacThucNguoiDung();

            if (ClientSso.Ins.CurrentSessionLoginInfo == null && Sessions.IsCurrentUseSSO)
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.Clear();
                ClientSso.Ins.DangXuat();
                return false;
            }
            else if (ClientSso.Ins.CurrentSessionLoginInfo != null && !Sessions.IsCurrentUseSSO)
            {
                var user = UserServices.GetUser(ClientSso.Ins.CurrentSessionLoginInfo.TenDangNhap);

                if (user != null)
                {
                    Sessions.UserID = user.UserID;
                    Sessions.DisplayName = user.FullName;
                    Sessions.User = user;
                    Sessions.Role = user.Role;
                    Sessions.IsCurrentUseSSO = true;
                    return true;
                }
            }

            if (!Authenticator.CheckLoggedIn() && Sessions.IsCurrentUseSSO)
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.Clear();
                return false;
            }

            return false;
        }

        public static bool IsUser => CommonSessions.UserID > 0;

        public static bool IsAgency => CommonSessions.AgencyID > 0;
    }
}