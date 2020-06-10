using Framework.Helper;
using System;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Services;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Components;
using YKCD.Agency.Business.Services;
using System.IO;
using System.Collections.Generic;
using YKCD.Agency.Business.Entities;

namespace YKCD.Agency.Application.Services
{
    /// <summary>
    /// Summary description for AgencyServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AgencyServices : WebService
    {
        /// <summary>
        /// Nhận ý kiến chỉ đạo từ UBND tỉnh
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
        /// Nhận báo cáo thực hiện ý kiến chỉ đạo từ UBND tỉnh
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
            var request = RequestServices.GetByProvinceID(requestID);

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

        /// <summary>
        /// Lấy thông tin ý kiến chỉ đạo
        /// </summary>
        /// <param name="requestID"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetRequestInfo(long requestID, int departmentID)
        {
            var requests = RequestServices.GetList(MaDonViThucHien: departmentID).Where(r => r.RequestID == requestID).ToList();
            return XmlHelper.ConvertToXmlObject(requests).ToXml();
        }

        /// <summary>
        /// Lấy danh sách tất cả YKCD theo đơn vị
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetAllRequest(int departmentID)
        {
            var requests = RequestServices.GetList(MaDonViThucHien: departmentID).Where(r => r.IsSynced != true).ToList();
            return XmlHelper.ConvertToXmlObject(requests).ToXml();
        }

        /// <summary>
        /// Lấy tất cả báo cáo theo
        /// </summary>
        /// <param name="requestID"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetAllReport(long requestID, int agencyID)
        {
            var reports = ReportServices.GetList(requestID, agencyID);
            return XmlHelper.ConvertToXmlObject(reports).ToXml();
        }

        /// <summary>
        /// Chuyển trạng thái ykcd thành đã đồng bộ
        /// </summary>
        /// <param name="performID"></param>
        [WebMethod]
        public void SetRequestSynced(long performID)
        {
            var perform = PerformServices.GetById(performID);

            if (perform != null)
            {
                perform.IsSynced = true;
                PerformServices.Update(perform);
            }
        }

        /// <summary>
        /// Cập nhật trạng thái thực hiện ý kiến chỉ đạo
        /// </summary>
        /// <param name="performID"></param>
        /// <param name="status"></param>
        /// <param name="date"></param>
        [WebMethod]
        public void SetRequestStatus(long performID, int status, DateTime date)
        {
            var perform = PerformServices.GetById(performID);

            if (perform != null)
            {
                perform.Status = status;
                if (status == 2 || status == 3)
                {
                    perform.Status = 3;
                    perform.FinishedOnDate = date;
                }
                perform.IsSynced = false;
                PerformServices.Update(perform);

                RequestServices.CapNhatTrangThaiYKCD(perform.RequestID);
            }
        }

        /// <summary>
        /// Nhận báo cáo từ đơn vị chuyển lên
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ReceiveReportFromSubAgency(string xmlDoc)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ReportList));

                using (var reader = new StringReader(xmlDoc))
                {
                    var xmlObj = (ReportList)serializer.Deserialize(reader);

                    foreach (var reportXml in xmlObj.Reports)
                    {
                        var perform = PerformServices.GetById(reportXml.PerformID);

                        if (reportXml != null)
                        {
                            var report = new Report
                            {
                                ReportContent = reportXml.ReportContent,
                                PerformOnDate = reportXml.PerformOnDate,
                                RequestID = reportXml.RequestID,
                                Status = reportXml.Status,
                                ReporterName = reportXml.ReporterName,
                                DepartmentID = perform.DepartmentID
                            };

                            if(reportXml.DanhSachFile != null && reportXml.DanhSachFile.Count() > 0)
                            {
                                ReportServices.Create(report, new List<int> { reportXml.PerformID.ToInteger() }, reportXml.DanhSachFile[0]?.Content, reportXml.DanhSachFile[0]?.FileName, AppSettings.UploadFolder);
                            }
                            else
                            {
                                ReportServices.Create(report, new List<int> { reportXml.PerformID.ToInteger() }, null, null, AppSettings.UploadFolder);
                            }                            
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return false;
            }
        }
    }
}
