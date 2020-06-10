using System.Configuration;
using Framework.Helper;

namespace YKCD.Agency.Application.Helper
{
    public class AppSettings
    {
        public static string Pasword => ConfigurationManager.AppSettings["Encrypt_Password"];
        public static string UploadFolder => ConfigurationManager.AppSettings["Path_Upload"];
        public static string HSCV_Service => ConfigurationManager.AppSettings["HSCV_Service"];
        public static string AGENCY_NAME => ConfigurationManager.AppSettings["AGENCY_NAME"];
        public static int AGENCY_ID => ConfigurationManager.AppSettings["AGENCY_ID"].ToInteger();
        public static string DateRegion => ConfigurationManager.AppSettings["DateRegion"].Decrypt(Pasword);
        public static bool IS_FIX_DATA => ConfigurationManager.AppSettings["IS_FIX_DATA"].ToBoolean();
        public static bool IS_USE_SSO => ConfigurationManager.AppSettings["IS_USE_SSO"].ToBoolean();
        public static string Province_Service => ConfigurationManager.AppSettings["Province_Service"];
        public static bool IS_USE_PROVINCE_MODULE => ConfigurationManager.AppSettings["IS_USE_PROVINCE_MODULE"].ToBoolean();
        public static bool IS_USE_SYNC_DOCUMENT_REPORT => ConfigurationManager.AppSettings["IS_USE_SYNC_DOCUMENT_REPORT"].ToBoolean();
    }
}