using Framework.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Services;
using System.Xml.Serialization;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Services
{
    /// <summary>
    /// Summary description for ProvinceService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProvinceService : WebService
    {
        /// <summary>
        /// Nhận báo cáo từ đơn vị chuyển lên
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ReceiveReport(string xmlDoc)
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

                        if (!string.IsNullOrEmpty(reportXml.ReportContent))
                        {
                            var report = new Report
                            {
                                ReportContent = reportXml.ReportContent,
                                PerformOnDate = reportXml.PerformOnDate,
                                RequestID = reportXml.RequestID,
                                Status = reportXml.Status,
                                CreatedByName = reportXml.ReporterName,
                                AgencyID = perform.AgencyID
                            };

                            ReportServices.Create(report);

                            foreach (var reportFile in reportXml.DanhSachFile)
                            {
                                try
                                {
                                    ReportFileServices.CreateReportFile(report, fileContent: reportFile.Content, fileName: reportFile.FileName, uploadFolder: AppSettings.UploadFolder);
                                }
                                catch (Exception ex)
                                {
                                    LogHelper.WriteLog(ex);
                                    continue;
                                }
                            }
                        }

                        if (perform.Status != 2)
                        {
                            perform.Status = reportXml.Status;

                            if (perform.Status == 2 || perform.Status == 3)
                            {
                                perform.Status = 3;
                                perform.FinishedOnDate = reportXml.PerformOnDate;
                            }

                            perform.IsSynced = false;
                            PerformServices.Update(perform);

                            RequestServices.CapNhatTrangThaiYKCD(reportXml.RequestID);
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

        /// <summary>
        /// Lấy thông tin ý kiến chỉ đạo
        /// </summary>
        /// <param name="requestID"></param>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetRequestInfo(long requestID, int agencyID)
        {
            var requests = RequestServices.GetList(MaDonVi: agencyID).Where(r => r.RequestID == requestID).ToList();
            return XmlHelper.ConvertToXmlObject(requests).ToXml();
        }

        /// <summary>
        /// Lấy danh sách tất cả YKCD theo đơn vị
        /// </summary>
        /// <param name="agencyID"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetAllRequest(int agencyID)
        {
            var requests = RequestServices.GetList(MaDonVi: agencyID).Where(r => r.IsSynced != true).ToList();
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
        public int SetRequestStatus(long performID, int status, DateTime date)
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

                return perform.Status;
            }

            return status;
        }

        /// <summary>
        /// Nhập thông tin ý kiến chỉ đạo từ hệ thống khác
        /// </summary>
        /// <param name="vanBan"></param>
        /// <param name="dsYKienChiDao"></param>
        /// <returns></returns>
        [WebMethod]
        public bool CreateRequest(VanBanChiDao vanBan, YKienChiDao[] dsYKienChiDao)
        {
            try
            {
                Document document = new Document
                {
                    DocumentCategoryID = 1,
                    DocumentCode = vanBan.KyHieu.ToUpper(),
                    SignerID = UserServices.GetUser(userName: vanBan.NguoiKy).UserID,
                    SignerName = UserServices.GetUser(userName: vanBan.NguoiKy).FullName,
                    WriterID = UserServices.GetUser(userName: vanBan.NguoiSoanThao).UserID,
                    ReleaseDate = vanBan.NgayBanHanh,
                    MainContent = vanBan.TrichYeu,
                    VBDHCode = vanBan.MaVanBan
                };

                DocumentServices.Create(document);

                foreach (var file in vanBan.DsFileDinhKem)
                {
                    DocumentFileServices.CreateDocumentFile(document, file.Content, file.FileName, AppSettings.UploadFolder);
                }

                foreach (var ykcd in dsYKienChiDao)
                {
                    Request request = new Request
                    {
                        DocumentID = document.DocumentID,
                        RequestContent = ykcd.NoiDungThucHien,
                        RequiredDate = ykcd.ThoiHanThucHien,
                        RequesterID = document.SignerID,
                        RequesterName = document.SignerName
                    };

                    List<int> agencyIds = new List<int>();

                    foreach (var agencyCode in ykcd.DsDonViThucHien)
                    {
                        var agency = AgencyServices.GetAgency(agencyIdentifierCode: agencyCode);

                        if (agency != null)
                        {
                            agencyIds.Add(agency.AgencyID);
                        }
                    }
                    List<int> staffIds = new List<int>();

                    foreach (var staffUserName in ykcd.DsChuyenVienTheoDoi)
                    {
                        var staff = UserServices.GetUser(userName: staffUserName);

                        if (staff != null)
                        {
                            staffIds.Add(staff.UserID);
                        }
                    }

                    RequestServices.Create(request, staffIds, agencyIds);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                return false;
            }
        }

        [WebMethod]
        public List<SoLieuThongKe> ThongKeTheoTatCaChuyenVienTheoDoi(DateTime? tuNgay = null)
        {
            return RequestServices.ThongKeTheoTatCaChuyenVienTheoDoi(tuNgay);
        }

        [WebMethod]
        public List<SoLieuThongKe> ThongKeTheoTatCaLanhDaoGiaoViec(DateTime? tuNgay = null)
        {
            return RequestServices.ThongKeTheoTatCaLanhDaoGiaoViec(tuNgay);
        }

        [WebMethod]
        public List<SoLieuThongKe> ThongKeDonViTheoNhom(int AgencyGroupID = 0, DateTime? tuNgay = null)
        {
            return RequestServices.ThongKeDonViTheoNhom(AgencyGroupID, tuNgay);
        }

        [WebMethod]
        public List<SoLieuThongKe> ThongKeTheoTatCaNhomDonVi()
        {
            return RequestServices.ThongKeTheoTatCaNhomDonVi();
        }
    }

    public class VanBanChiDao
    {
        public string MaVanBan { get; set; }
        public string KyHieu { get; set; }
        public DateTime NgayBanHanh { get; set; }
        public string NguoiKy { get; set; }
        public string NguoiSoanThao { get; set; }
        public string TrichYeu { get; set; }
        public FileDinhKem[] DsFileDinhKem { get; set; }
    }

    public class YKienChiDao
    {
        public string NoiDungThucHien { get; set; }
        public DateTime ThoiHanThucHien { get; set; }
        public string[] DsDonViThucHien { get; set; }
        public string[] DsChuyenVienTheoDoi { get; set; }
    }

    public class FileDinhKem
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
