using Framework.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Components;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.Services
{
    /// <summary>
    /// Summary description for SubAgencyServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SubAgencyServices : System.Web.Services.WebService
    {

        /// <summary>
        /// Nhận ý kiến chỉ đạo từ đơn vị cấp trên
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ReceiveRequest(string xmlDoc)
        {
            try
            {
                var xmlObj = xmlDoc.ToRequestList();

                foreach (var requestXml in xmlObj.Requests?.Where(r => r != null))
                {
                    requestXml.SaveRequest(AppSettings.UploadFolder);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return false;
            }
        }

        /// <summary>
        /// Nhận báo cáo thực hiện ý kiến chỉ đạo từ đơn vị cấp trên
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ReceiveReport(string xmlDoc)
        {
            try
            {
                foreach (var reportXml in xmlDoc?.ToReportList().Reports)
                {
                    reportXml.SaveReport(AppSettings.UploadFolder);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return false;
            }
        }

        /// <summary>
        /// Xóa thông tin ý kiến chỉ đạo
        /// </summary>
        /// <param name="requestID"></param>
        [WebMethod]
        public void DeleteRequest(long requestID)
        {
            var request = RequestServices.GetByAgencyID(requestID);

            if (request != null)
            {
                request.IsDeleted = true;

                foreach (var perform in request.Performs)
                {
                    PerformServices.Delete(perform);
                }

                RequestServices.Update(request);
            }
        }
    }
}
