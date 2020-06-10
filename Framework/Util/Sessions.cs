using System.Web;
using Framework.Helper;

namespace Framework.Util
{
    public class CommonSessions
    {
        public static int UserID
        {
            get
            {
                return HttpContext.Current.Session["UserID"].ToInteger();
            }
            set { HttpContext.Current.Session["UserID"] = value; }
        }

        public static int AgencyID
        {
            get
            {
                return HttpContext.Current.Session["AgencyID"].ToInteger();
            }
            set { HttpContext.Current.Session["AgencyID"] = value; }
        }

        public static int DepartmentID
        {
            get
            {
                return HttpContext.Current.Session["DepartmentID"].ToInteger();
            }
            set { HttpContext.Current.Session["DepartmentID"] = value; }
        }

        public static string DisplayName
        {
            get
            {
                if (HttpContext.Current.Session["DisplayName"] != null)
                    return HttpContext.Current.Session["DisplayName"].ToString();
                return null;
            }
            set { HttpContext.Current.Session["DisplayName"] = value; }
        }

        public static bool IsCurrentUseSSO
        {
            get
            {
                return HttpContext.Current.Session["IsCurrentUseSSO"].ToBoolean();
            }
            set { HttpContext.Current.Session["IsCurrentUseSSO"] = value; }
        }

        public static int Role
        {
            get
            {
                return HttpContext.Current.Session["Role"].ToInteger();
            }
            set { HttpContext.Current.Session["Role"] = value; }
        }
    }
}
