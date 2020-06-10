using System;
using System.Linq;
using System.ServiceModel;
using Ykcd.SubAgency.Business.AgencyServices;
using YKCD.SubAgency.Business.Entities;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Business.Components
{
    public static class AgencyServiceHelper
    {
        public static AgencyServicesSoapClient AgencyClient = new AgencyServicesSoapClient();

        public static bool ReceiveReport(string serviceUrl, Report report)
        {
            try
            {
                if (!string.IsNullOrEmpty(serviceUrl))
                {
                    AgencyClient.Endpoint.Address = new EndpointAddress(serviceUrl);
                    var xmlobject = XmlHelper.ConvertToXmlObject(report);
                    AgencyClient.ReceiveReportFromSubAgency(xmlobject.ToXml());
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Request GetRequestInfo(string serviceUrl, long requestID, int agencyID)
        {
            try
            {
                if (!string.IsNullOrEmpty(serviceUrl))
                {
                    AgencyClient.Endpoint.Address = new EndpointAddress(serviceUrl);
                    return AgencyClient.GetRequestInfo(requestID, agencyID).ToRequestList().Requests[0].ToRequest();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void GetDataFromAgency(string serviceUrl, int subAgencyID, string uploadFolder)
        {
            if (!string.IsNullOrEmpty(serviceUrl))
            {
                AgencyClient.Endpoint.Address = new EndpointAddress(serviceUrl);

                var requestList = AgencyClient.GetAllRequest(subAgencyID)?.ToRequestList();

                if (requestList?.Requests != null)
                {
                    foreach (var requestElement in requestList.Requests)
                    {
                        var request = requestElement.SaveRequest(uploadFolder);

                        var reportElements = AgencyClient.GetAllReport(requestElement.RequestID, subAgencyID)?.ToReportList()?.Reports;

                        if (reportElements != null && reportElements.Length > 0)
                        {
                            var reportList = AgencyClient.GetAllReport(requestElement.RequestID, subAgencyID)?.ToReportList();

                            if (reportList != null)
                            {
                                foreach (var reportElement in reportList.Reports)
                                {
                                    var localReports = request.Reports;

                                    if (localReports == null || !localReports.Select(item => item.ReportContent.Trim().ToUpper().Equals(reportElement.ReportContent.Trim().ToUpper())).Any())
                                    {
                                        reportElement.SaveReport(uploadFolder);
                                    }
                                }
                            }
                        }

                        AgencyClient.SetRequestSynced(request.AgencyPerformID);
                    }
                }
            }
        }

        public static Document SaveDocument(this RequestElement requestElement, string uploadFolder)
        {
            if(requestElement.DocumentID > 0)
            {
                var document = DocumentServices.GetByAgencyID(requestElement.DocumentID);

                if (document == null)
                {
                    document = requestElement.ToDocument();
                    DocumentServices.Create(document);

                    if (requestElement.DanhSachFile != null)
                    {
                        foreach (var docFile in requestElement.DanhSachFile.Where(r => r?.Content != null && !string.IsNullOrEmpty(r.FileName)))
                        {
                            DocumentFileServices.CreateDocumentFile(document, docFile.Content, docFile.FileName, uploadFolder);
                        }
                    }
                }
                else
                {
                    document.SignerName = requestElement.SignerName;
                    document.DocumentCode = requestElement.DocumentCode;
                    document.MainContent = requestElement.MainContent;
                    document.ReleaseDate = requestElement.ReleasedDate;
                    DocumentServices.Update(document);
                }

                return document;
            }

            return null;
        }

        public static Request SaveRequest(this RequestElement requestElement, string uploadFolder)
        {
            var request = RequestServices.GetByAgencyID(requestElement.RequestID);

            if (request == null || request.RequestID <= 0)
            {
                var document = requestElement.SaveDocument(uploadFolder);
                request = requestElement.ToRequest();
                request.DocumentID = (document != null) ? document.DocumentID : 0;

                RequestServices.Create(request);
            }
            else
            {
                if (requestElement.IsDeleted)
                {
                    RequestServices.Delete(request.RequestID, shiftDelete: true);
                }
                else
                {
                    request.Status = requestElement.Status;
                    request.RequiredDate = requestElement.RequiredDate;
                    request.FinishedOnDate = requestElement.FinishedOnDate;
                    request.RequestContent = requestElement.RequestContent;
                    request.IsDeleted = requestElement.IsDeleted;
                    RequestServices.Update(request);
                }
            }

            return request;
        }

        public static void SaveReport(this ReportElement reportElement, string uploadFolder)
        {
            var request = RequestServices.GetByAgencyID(reportElement.RequestID);

            if (request.Reports.Select(r => r.ReportContent.Trim()).Contains(reportElement.ReportContent.Trim()))
            {
                var report = new Report
                {
                    ReportContent = reportElement.ReportContent,
                    PerformOnDate = reportElement.PerformOnDate,
                    RequestID = request.RequestID,
                    Status = reportElement.Status,
                    ReporterName = reportElement.ReporterName
                };

                ReportServices.Create(report: report, performIds: null, fileContent: reportElement.DanhSachFile?.FirstOrDefault()?.Content, fileName: reportElement.DanhSachFile?.FirstOrDefault()?.FileName, uploadFolder: uploadFolder, isSendFromAgency: true);
            }
        }

        public static void SetAgencyRequestStatus(string serviceUrl, long performID, int status, DateTime date)
        {
            AgencyClient.Endpoint.Address = new EndpointAddress(serviceUrl);
            AgencyClient.SetRequestStatus(performID, status, date);
        }
    }
}
