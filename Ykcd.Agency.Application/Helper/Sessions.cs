using System.Web;
using Framework.Util;
using YKCD.Agency.Business.Entities;

namespace YKCD.Agency.Application.Helper
{
    public class Sessions : CommonSessions
    {
        public static User User
        {
            get
            {
                if (HttpContext.Current.Session["User"] != null)
                    return HttpContext.Current.Session["User"] as User;
                return new User();
            }
            set { HttpContext.Current.Session["User"] = value; }
        }
    }
}