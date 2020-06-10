using Exceptionless;
using log4net;
using System;
using System.Web;

namespace Framework.Helper
{
    public class LogHelper
    {
        public static void WriteLog(Exception ex)
        {
            ExceptionlessClient.Default.Startup("UJxab0Hs3FoA4H91dGAA179VtLQud3olPLKbTap6");
            ex.ToExceptionless().Submit();
        }
    }
}
