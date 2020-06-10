using System;
using System.Web;
using System.Web.UI;
using Framework.Helper;

namespace Framework.Web
{
    public class PageBase : Page
    {
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            Exception ex = Server.GetLastError();

            if (ex != null)
            {
                LogHelper.WriteLog(ex);
            }
        }

        public static string GetStyle(params string[] viewNames)
        {
            string cssClass = string.Empty;

            foreach (string viewName in viewNames)
            {
                if (HttpContext.Current.Request.Url.ToString().Contains(viewName))
                    cssClass = "active";
            }

            return cssClass;
        }

        public static string GetStyle(string viewName, string parameterName, string parameterValue)
        {
            string cssClass = string.Empty;

            if (HttpContext.Current.Request.Url.ToString().Contains(viewName) && parameterName.ToUrlString().Equals(parameterValue))
            {
                cssClass = "active";
            }

            return cssClass;
        }

        public static string GetStyleByMasterPages(params string[] masterPages)
        {
            Page page = HttpContext.Current.Handler as Page;
            string cssClass = string.Empty;

            if (page != null)
            {
                foreach (var item in masterPages)
                {
                    if (page.MasterPageFile.Contains(item))
                        cssClass = "active";
                }
            }

            return cssClass;
        }
    }
}