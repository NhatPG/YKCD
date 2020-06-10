using System;
using log4net.Config;
using Framework.Helper;
using YKCD.Province.Business.Services;
using YKCD.Province.Application.HscvService;
using System.ServiceModel;
using YKCD.Province.Application.Helper;
using System.Linq;

namespace YKCD.Province.Application
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            XmlConfigurator.Configure();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //MettingServices.SyncLichHop(DateTime.Now.AddDays(-14));
            //GetVbdhCode();
        }

        public static void GetVbdhCode()
        {
            ExchangeDocServiceSoapClient client = new ExchangeDocServiceSoapClient();
            client.Endpoint.Address = new EndpointAddress(AppSettings.HSCV_Service);

            var documents = client.GetVBDiByDate(new DateTime(2018, 8, 20), new DateTime(2018, 12, 31))?.ToList();

            foreach (var document in DocumentServices.GetList(fromDate: new DateTime(2018, 8, 20), toDate: new DateTime(2018, 12, 31)))
            {
                if (document.VBDHCode.IsNullOrEmpty())
                {
                    string documentCode = document.DocumentCode.Trim().ToUpper().Replace(" ", "");
                    document.VBDHCode = documents.Where(doc => doc != null && doc.SoKyHieu != null && doc.SoKyHieu.Trim().ToUpper().Equals(documentCode))?.FirstOrDefault()?.MaVBDi;
                    DocumentServices.Update(document);
                }
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //var items = RequestServices.Search();

            //foreach (var item in items)
            //{
            //    RequestServices.CapNhatTrangThaiYKCD(item.RequestID);
            //}
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            LogHelper.WriteLog(exc);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}