using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Components
{
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class RequestList
    {
        [XmlElement("Request")]
        public RequestElement[] Requests { get; set; }
    }

    public class RequestElement
    {
        public long RequestID { get; set; }
        public long PerformID { get; set; }
        public long DocumentID { get; set; }
        public int DocumentCategoryID { get; set; }
        public string DocumentCode { get; set; }
        public string MainContent { get; set; }
        public DateTime ReleasedDate { get; set; }
        public string SignerName { get; set; }
        public string RequestContent { get; set; }
        public DateTime RequiredDate { get; set; }
        public int RequesterID { get; set; }
        public string RequesterName { get; set; }
        public int Status { get; set; }
        public DateTime FinishedOnDate { get; set; } = new DateTime(1990, 1, 1);
        public bool IsDeleted { get; set; }

        [XmlArray("DanhSachFile")]
        [XmlArrayItem("FileDinhKem")]
        public FileDinhKem[] DanhSachFile { get; set; }
    }

    public class FileDinhKem
    {
        [XmlAttribute]
        public string FileName { get; set; }

        [XmlText]
        public byte[] Content { get; set; }
    }

    [XmlRoot(Namespace = "", IsNullable = false)]
    public class ReportList
    {
        [XmlElement("Report")]
        public ReportElement[] Reports { get; set; }
    }

    public class ReportElement
    {
        public long RequestID { get; set; }
        public long ReportID { get; set; }
        public long PerformID { get; set; }
        public int Status { get; set; }
        public string ReportContent { get; set; }
        public string ReporterName { get; set; }
        public DateTime PerformOnDate { get; set; } = new DateTime(1990, 1, 1);
        public bool IsDeleted { get; set; }

        [XmlArray("DanhSachFile")]
        [XmlArrayItem("FileDinhKem")]
        public FileDinhKem[] DanhSachFile { get; set; }
    }

    public static class XmlHelper
    {
        public static RequestList ConvertToXmlObject(Request request, Perform perform, bool isDeleted = false)
        {
            var listFiles = new List<FileDinhKem>();

            foreach (var file in request.Document.Files)
            {
                listFiles.Add(new FileDinhKem()
                {
                    FileName = file.FileName,
                    Content = System.IO.File.ReadAllBytes($"{ConfigurationManager.AppSettings["Path_Upload"]}\\{file.FileName}")
                });
            }

            return new RequestList
            {
                Requests = new[]
                {
                    new RequestElement
                    {
                        DocumentID = request.DocumentID,
                        DocumentCode = request.Document.DocumentCode,
                        ReleasedDate = request.Document.ReleaseDate,
                        MainContent = request.Document.MainContent,
                        SignerName = request.Document.SignerName,
                        RequestID = request.RequestID,
                        PerformID = perform.PerformID,
                        RequestContent =  request.RequestContent,
                        RequiredDate = perform.RequiredDate,
                        RequesterID = request.RequesterID,
                        RequesterName = request.RequesterName,
                        Status = perform.Status,
                        FinishedOnDate = perform.FinishedOnDate,
                        IsDeleted = isDeleted,
                        DanhSachFile = listFiles.ToArray()
                    }
                }
            };
        }

        public static RequestList ConvertToXmlObject(List<Request> requests)
        {
            int i = 0;

            var result = new RequestList()
            {
                Requests = new RequestElement[requests.Count]
            };

            foreach (var request in requests)
            {
                var listFiles = new List<FileDinhKem>();

                if (request.Status == 0) //Chỉ lấy file của những ykcd được thêm mới
                {
                    foreach (var file in request.Document.Files)
                    {
                        string path = $"{ConfigurationManager.AppSettings["Path_Upload"]}\\{file.FileName}";

                        if (System.IO.File.Exists(path))
                        {
                            listFiles.Add(new FileDinhKem()
                            {
                                FileName = file.FileName,
                                Content = System.IO.File.ReadAllBytes(path)
                            });
                        }
                    }
                }

                result.Requests[i] = new RequestElement
                {
                    DocumentID = request.DocumentID,
                    DocumentCode = request.Document.DocumentCode,
                    ReleasedDate = request.Document.ReleaseDate,
                    MainContent = request.Document.MainContent,
                    SignerName = request.Document.SignerName,
                    RequestID = request.RequestID,
                    PerformID = request.PerformID,
                    RequestContent = request.RequestContent,
                    RequiredDate = request.RequiredDate,
                    RequesterID = request.RequesterID,
                    RequesterName = request.RequesterName,
                    Status = request.Status,
                    FinishedOnDate = request.FinishedOnDate,
                    IsDeleted = request.IsDeleted,
                    DanhSachFile = listFiles?.ToArray()
                };

                i++;
            }

            return result;
        }

        public static ReportList ConvertToXmlObject(Report report, Perform perform)
        {
            var listFiles = new List<FileDinhKem>();

            foreach (var file in report.Files)
            {
                listFiles.Add(new FileDinhKem
                {
                    FileName = file.FileName,
                    Content = System.IO.File.ReadAllBytes($"{ConfigurationManager.AppSettings["Path_Upload"]}\\{file.FileName}")
                });
            }

            return new ReportList
            {
                Reports = new[]
                {
                    new ReportElement
                    {
                        RequestID = report.Request.RequestID,
                        ReportID = report.ReportID,
                        Status = perform.Status,
                        ReportContent = report.ReportContent,
                        ReporterName = report.CreatedByName,
                        PerformOnDate = report.PerformOnDate,
                        IsDeleted = report.IsDeleted,
                        DanhSachFile = listFiles.ToArray()
                    }
                }
            };
        }

        public static ReportList ConvertToXmlObject(List<Report> reports)
        {
            int i = 0;

            var result = new ReportList()
            {
                Reports = new ReportElement[reports.Count]
            };

            foreach (var report in reports)
            {
                var listFiles = new List<FileDinhKem>();

                foreach (var file in report.Files)
                {
                    string path = $"{ConfigurationManager.AppSettings["Path_Upload"]}\\{file.FileName}";

                    if (System.IO.File.Exists(path))
                    {
                        listFiles.Add(new FileDinhKem
                        {
                            FileName = file.FileName,
                            Content = System.IO.File.ReadAllBytes(path)
                        });
                    }

                }

                result.Reports[i] = new ReportElement
                {
                    RequestID = report.Request.RequestID,
                    ReportID = report.ReportID,
                    Status = report.Status,
                    ReportContent = report.ReportContent,
                    ReporterName = report.CreatedByName,
                    PerformOnDate = report.PerformOnDate,
                    IsDeleted = report.IsDeleted,
                    DanhSachFile = listFiles.ToArray()
                };
            }

            return result;
        }

        public static string ToXml(this RequestList requestList)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(RequestList));
            System.IO.StringWriter sww = new System.IO.StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);

            xsSubmit.Serialize(writer, requestList);
            return sww.ToString();
        }

        public static string ToXml(this ReportList requestList)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(ReportList));
            System.IO.StringWriter sww = new System.IO.StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);

            xsSubmit.Serialize(writer, requestList);
            return sww.ToString();
        }
    }
}