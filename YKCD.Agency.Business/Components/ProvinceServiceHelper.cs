using System;
using System.Linq;
using System.ServiceModel;
using YKCD.Agency.Business.Entities;
using YKCD.Agency.Business.ProvinceService;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Business.Components
{
    public static class ProvinceServiceHelper
    {
        public static ProvinceServiceSoapClient ProvinceClient = new ProvinceServiceSoapClient();

        public static bool ReceiveReport(string serviceUrl, Report report)
        {
            try
            {
                if (!string.IsNullOrEmpty(serviceUrl))
                {
                    ProvinceClient.Endpoint.Address = new EndpointAddress(serviceUrl);
                    var xmlobject = XmlHelper.ConvertToXmlObject(report);
                    ProvinceClient.ReceiveReport(xmlobject.ToXml());
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
                    ProvinceClient.Endpoint.Address = new EndpointAddress(serviceUrl);
                    return ProvinceClient.GetRequestInfo(requestID, agencyID).ToRequestList().Requests[0].ToRequest();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void GetDataFromProvince(string serviceUrl, int agencyID, string uploadFolder)
        {
            if (!string.IsNullOrEmpty(serviceUrl))
            {
                ProvinceClient.Endpoint.Address = new EndpointAddress(serviceUrl);

                var requestList = ProvinceClient.GetAllRequest(agencyID)?.ToRequestList();

                if (requestList?.Requests != null)
                {
                    foreach (var requestElement in requestList.Requests)
                    {
                        var request = requestElement.SaveRequest(uploadFolder);

                        var reportElements = ProvinceClient.GetAllReport(requestElement.RequestID, agencyID)?.ToReportList()?.Reports;

                        if (reportElements != null && reportElements.Length > 0)
                        {
                            var reportList = ProvinceClient.GetAllReport(requestElement.RequestID, agencyID)?.ToReportList();

                            if (reportList != null)
                            {
                                foreach (var reportElement in reportList.Reports)
                                {
                                    var localReports = request.Reports;

                                    reportElement.SaveReport(uploadFolder);
                                }
                            }
                        }

                        ProvinceClient.SetRequestSynced(request.ProvincePerformID);
                    }
                }
            }
        }

        public static Document SaveDocument(this RequestElement requestElement, string uploadFolder)
        {
            var document = DocumentServices.GetByProvinceID(requestElement.DocumentID);

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

        public static Request SaveRequest(this RequestElement requestElement, string uploadFolder)
        {
            var request = RequestServices.GetByProvinceID(requestElement.RequestID);

            if (request == null || request.RequestID <= 0)
            {
                var document = requestElement.SaveDocument(uploadFolder);
                request = requestElement.ToRequest();
                request.DocumentID = document.DocumentID;

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

                    if (request.IsAssignPerform)
                    {
                        foreach (Perform perform in request.Performs)
                        {
                            if (request.Status <= 1 && perform.Status == 2)
                            {
                                perform.Status = request.Status;
                                PerformServices.Update(perform);
                            }
                            else if (request.Status == 2 && perform.Status != 2)
                            {
                                perform.Status = 2;
                                perform.FinishedOnDate = request.FinishedOnDate;
                                PerformServices.Update(perform);
                            }
                        }
                    }
                }
            }

            return request;
        }

        public static void SaveReport(this ReportElement reportElement, string uploadFolder)
        {
            var request = RequestServices.GetByProvinceID(reportElement.RequestID);

            if (request.Reports != null && !request.Reports.Select(r => r.ReportContent.Trim()).Contains(reportElement.ReportContent.Trim()))
            {
                var report = new Report
                {
                    ReportContent = reportElement.ReportContent,
                    PerformOnDate = reportElement.PerformOnDate,
                    RequestID = request.RequestID,
                    Status = reportElement.Status,
                    ReporterName = reportElement.ReporterName
                };

                ReportServices.Create(report: report, performIds: null, fileContent: reportElement.DanhSachFile?.FirstOrDefault()?.Content, fileName: reportElement.DanhSachFile?.FirstOrDefault()?.FileName, uploadFolder: uploadFolder, isSendFromProvince: true);
            }
        }

        public static void SetProvinceRequestStatus(string serviceUrl, long performID, int status, DateTime date)
        {
            ProvinceClient.Endpoint.Address = new EndpointAddress(serviceUrl);
            ProvinceClient.SetRequestStatus(performID, status, date);
        }

        
    }
}
