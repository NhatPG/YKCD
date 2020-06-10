using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Framework.Helper;
using YKCD.SubAgency.Business.Entities;
using System.Configuration;
using System.IO;

namespace YKCD.SubAgency.Business.Components
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
        public static ReportList ConvertToXmlObject(Report report)
        {
            if (report == null)
                return null;

            var listFiles = new List<FileDinhKem>();

            if (report.Files != null)
            {
                foreach (var file in report.Files)
                {
                    listFiles.Add(new FileDinhKem
                    {
                        FileName = file.FileName,
                        Content = System.IO.File.ReadAllBytes($"{ConfigurationManager.AppSettings["Path_Upload"]}\\{file.FileName}")
                    });
                }
            }

            return new ReportList
            {
                Reports = new[]
                {
                    new ReportElement
                    {
                        RequestID = report.Request.AgencyRequestID.ToLong(),
                        PerformID = report.Request.AgencyPerformID.ToLong(),
                        ReportID = report.ReportID,
                        Status = report.Status,
                        ReportContent = report.ReportContent,
                        ReporterName = ConfigurationManager.AppSettings["SUBAGENCY_NAME"],
                        PerformOnDate = report.PerformOnDate,
                        IsDeleted = report.IsDeleted,
                        DanhSachFile = listFiles?.ToArray()
                    }
                }
            };
        }

        public static string ToXml(this RequestList requestList)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(RequestList));
            System.IO.StringWriter sww = new System.IO.StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);

            xsSubmit.Serialize(writer, requestList);
            return sww.ToString();
        }

        public static string ToXml(this ReportList reportList)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(ReportList));
            System.IO.StringWriter sww = new System.IO.StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);

            xsSubmit.Serialize(writer, reportList);
            return sww.ToString();
        }

        public static RequestList ToRequestList(this string xmlDoc)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(RequestList));

                using (var reader = new StringReader(xmlDoc))
                {
                    return (RequestList)serializer.Deserialize(reader);
                }
            }
            catch
            {
                return null;
            }
        }

        public static ReportList ToReportList(this string xmlDoc)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ReportList));

                using (var reader = new StringReader(xmlDoc))
                {
                    return (ReportList)serializer.Deserialize(reader);
                }
            }
            catch
            {
                return null;
            }
        }

        public static Request ToRequest(this RequestElement requestElement)
        {
            var request = new Request();
            request.AgencyRequestID = requestElement.RequestID;
            request.AgencyPerformID = requestElement.PerformID;
            request.Status = requestElement.Status;
            request.RequiredDate = requestElement.RequiredDate;
            request.FinishedOnDate = requestElement.FinishedOnDate;
            request.RequestContent = requestElement.RequestContent;
            request.IsAgencyRequest = true;
            request.RequesterName = requestElement.SignerName;

            return request;
        }

        public static Request ToRequest(this string xmlDoc)
        {
            return xmlDoc.ToRequestList()?.Requests[0]?.ToRequest();
        }

        public static Document ToDocument(this string xmlDoc)
        {
            return xmlDoc.ToRequestList()?.Requests[0]?.ToDocument();
        }

        public static Document ToDocument(this RequestElement requestElement)
        {
            return new Document
            {
                DocumentCategoryID = requestElement.DocumentCategoryID,
                SignerName = requestElement.SignerName,
                DocumentCode = requestElement.DocumentCode,
                MainContent = requestElement.MainContent,
                ReleaseDate = requestElement.ReleasedDate,
                AgencyDocumentID = requestElement.DocumentID
            };
        }
    }
}