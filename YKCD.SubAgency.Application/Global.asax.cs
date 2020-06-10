using System;
using System.Threading;
using System.Web.Optimization;
using Framework.Helper;
using log4net.Config;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Components;

namespace YKCD.SubAgency.Application
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterBundles(BundleTable.Bundles);
            XmlConfigurator.Configure();
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-1.9.1.min.js",
                "~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/sb-admin-2.min.js",
                "~/Scripts/bootstrap-datepicker.min.js",
                "~/Scripts/bootstrap-file-upload.js",
                "~/Scripts/metisMenu.min.js",
                "~/Scripts/select2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/highchartjs").Include(
                "~/Scripts/Highcharts/js/highcharts.js"));

            bundles.Add(new ScriptBundle("~/bundles/commonjs").Include(
                "~/Scripts/common.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrapcss").Include(
                "~/Styles/bootstrap.min.css",
                "~/Styles/sb-admin-2.min.css",
                "~/Styles/font-awesome.min.css",
                "~/Styles/bootstrap-datepicker.min.css",
                "~/Styles/panel-with-tabs.css",
                "~/Styles/select2.min.css",
                "~/Styles/bootstrap-file-upload.css",
                "~/Styles/awesome-bootstrap-checkbox.css",
                "~/Styles/common.css"));

            BundleTable.EnableOptimizations = true;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            if (AppSettings.IS_USE_AGENCY_MODULE)
            {
                Application.Lock();
                AgencyServiceHelper.GetDataFromAgency(AppSettings.Agency_Service, AppSettings.SUBAGENCY_ID, AppSettings.UploadFolder);
                Application.UnLock();
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (ex is ThreadAbortException)
                return;

            LogHelper.WriteLog(ex);
        }
    }
}