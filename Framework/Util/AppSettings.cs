using System.Configuration;

namespace Framework.Util
{
    public class CommonAppSettings
    {
        public static string DatabaseConnection => ConfigurationManager.AppSettings["DatabaseConnection"];
    }
}
