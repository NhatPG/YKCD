using System.Web;

namespace Framework.Helper
{
    public static class Redirector
    {
        public static string ToUrl(this string serverUrl)
        {
            if (HttpContext.Current.Request.ApplicationPath == "/")
            {
                return HttpContext.Current.Request.ApplicationPath + serverUrl.Substring(2);
            }

            return HttpContext.Current.Request.ApplicationPath + serverUrl.Substring(1);
        }

        public static void RedirectToDefault()
        {
            HttpContext.Current.Response.Redirect("~/Default.aspx".ToUrl(), true);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static string GetLink(string viewName)
        {
            return $"~/{viewName}".ToUrl();
        }

        public static void Redirect(this string viewName)
        {
            HttpContext.Current.Response.Redirect(GetLink(viewName), true);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void Transfer(this string viewName)
        {
            HttpContext.Current.Server.Transfer(GetLink(viewName), true);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void RedirectPermanent(this string viewName)
        {
            HttpContext.Current.Response.RedirectPermanent(GetLink(viewName), true);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static string GetLink(string viewName, string paramName, object paramValue)
        {
            return $"~/{viewName}?{paramName}={paramValue}".ToUrl();
        }

        public static string GetLink(string viewName, string paramName, object paramValue, string paramName2, object paramValue2)
        {
            return $"~/{viewName}?{paramName}={paramValue}&{paramName2}={paramValue2}".ToUrl();
        }

        public static string GetLink(string viewName, string paramName, object paramValue, string paramName2, object paramValue2, string paramName3, object paramValue3)
        {
            return $"~/{viewName}?{paramName}={paramValue}&{paramName2}={paramValue2}&{paramName3}={paramValue3}".ToUrl();
        }

        public static void Redirect(string viewName, string paramName, object paramValue)
        {
            HttpContext.Current.Response.Redirect(GetLink(viewName, paramName, paramValue), true);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}