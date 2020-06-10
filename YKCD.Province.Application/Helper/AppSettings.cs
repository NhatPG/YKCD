using System.Configuration;
using Framework.Helper;

namespace YKCD.Province.Application.Helper
{
    public class AppSettings
    {
        public static string Pasword => ConfigurationManager.AppSettings["Encrypt_Password"];
        public static string UploadFolder => ConfigurationManager.AppSettings["Path_Upload"];
        public static string HSCV_Service => ConfigurationManager.AppSettings["HSCV_Service"];
        public static string ProvinceName => ConfigurationManager.AppSettings["ProvinceName"].Decrypt(Pasword);
    }
}