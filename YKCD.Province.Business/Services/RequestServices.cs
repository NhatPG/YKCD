using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.Helper;
using PetaPoco;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Services
{
    public class RequestServices : BaseService<Request>
    {
        /// <summary>
        /// Lấy kết quả thống kê tình hình thực hiện YKCD của tất cả các đơn vị
        /// </summary>
        /// <param name="tuNgay">Mốc thống kê từ ngày</param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static List<SoLieuThongKe> ThongKeTheoTatCaNhomDonVi(DateTime? tuNgay = null, string connectionName = "DatabaseConnection")
        {
            var result = new List<SoLieuThongKe>();

            foreach (var agencyGroup in AgencyGroupServices.GetList(isShowStatistic: true))
            {
                var items = PerformServices.GetList(AgencyGroupID: agencyGroup.AgencyGroupID, FromDate: tuNgay);

                result.Add(new SoLieuThongKe
                {
                    ObjectID = agencyGroup.AgencyGroupID,
                    ObjectName = agencyGroup.AgencyGroupName,
                    NotPerform = items.Count(item => item.Status == 0),
                    NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date),
                    NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date),
                    Performing = items.Count(item => item.Status == 1),
                    PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date),
                    PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date),
                    WaitToConfirm = items.Count(item => item.Status == 3),
                    Done = items.Count(item => item.Status == 2),
                    DoneInTerm = items.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                    DoneOutTerm = items.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
                    Total = items.Count
                });
            }

            return result;
        }

        public static List<SoLieuThongKe> ThongKeDonViTheoNhom(int AgencyGroupID = 0, DateTime? tuNgay = null, string connectionName = "DatabaseConnection")
        {
            var result = new List<SoLieuThongKe>();

            foreach (var agency in AgencyServices.GetList(AgencyGroupID: AgencyGroupID).OrderBy(agency => agency.AgencyGroupID).ThenBy(agency => agency.DisplayOrder).ThenBy(agency => agency.AgencyName))
            {
                var items = PerformServices.GetList(AgencyID: agency.AgencyID, FromDate: tuNgay);

                result.Add(new SoLieuThongKe
                {
                    ObjectID = agency.AgencyID,
                    ObjectName = agency.AgencyName,
                    GroupID = agency.AgencyGroupID,
                    GroupName = agency.AgencyGroupName,
                    NotPerform = items.Count(item => item.Status == 0),
                    NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date),
                    NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date),
                    Performing = items.Count(item => item.Status == 1),
                    PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date),
                    PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date),
                    WaitToConfirm = items.Count(item => item.Status == 3),
                    Done = items.Count(item => item.Status == 2),
                    DoneInTerm = items.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                    DoneOutTerm = items.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
                    Total = items.Count
                });
            }

            return result;
        }

        public static List<SoLieuThongKe> ThongKeTheoTatCaLanhDaoGiaoViec(DateTime? tuNgay = null)
        {
            var result = new List<SoLieuThongKe>();

            foreach (var user in UserServices.GetList(roles: new[] { 3, 4 }))
            {
                var items = GetList(MaNguoiGiaoViec: user.UserID, tuNgay: tuNgay);

                result.Add(new SoLieuThongKe
                {
                    ObjectID = user.UserID,
                    ObjectName = user.FullName,
                    GroupID = user.DepartmentID,
                    GroupName = user.Department.DepartmentName,
                    NotPerform = items.Count(item => item.Status == 0),
                    NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date),
                    NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date),
                    Performing = items.Count(item => item.Status == 1),
                    PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date),
                    PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date),
                    WaitToConfirm = PerformServices.GetList(MaNguoiGiaoViec: user.UserID).Count(item => item.Status == 3),
                    Done = items.Count(item => item.Status == 2),
                    DoneInTerm = items.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                    DoneOutTerm = items.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
                    Total = items.Count
                });
            }

            return result;
        }

        public static List<SoLieuThongKe> ThongKeTheoTatCaChuyenVienTheoDoi(DateTime? tuNgay = null)
        {
            var result = new List<SoLieuThongKe>();

            foreach (var user in UserServices.GetList(roles: new[] { 5 }))
            {
                var items = GetList(MaNguoiTheoDoi: user.UserID, tuNgay: tuNgay);

                result.Add(new SoLieuThongKe
                {
                    ObjectID = user.UserID,
                    ObjectName = user.FullName,
                    GroupID = user.DepartmentID,
                    GroupName = user.Department.DepartmentName,
                    NotPerform = items.Count(item => item.Status == 0),
                    NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate.Date >= DateTime.Now.Date),
                    NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate.Date < DateTime.Now.Date),
                    Performing = items.Count(item => item.Status == 1),
                    PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate.Date >= DateTime.Now.Date),
                    PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate.Date < DateTime.Now.Date),
                    WaitToConfirm = PerformServices.GetList(MaNguoiTheoDoi: user.UserID, FromDate: tuNgay).Where(item => item.Status == 3).Select(item => item.RequestID).Distinct().Count(),
                    Done = items.Count(item => item.Status == 2),
                    DoneInTerm = items.Count(item => item.Status == 2 && item.RequiredDate.Date >= item.FinishedOnDate.Date),
                    DoneOutTerm = items.Count(item => item.Status == 2 && item.RequiredDate.Date < item.FinishedOnDate.Date),
                    DocumentLate = MettingServices.GetList()
                                       ?.Where(item => item.StaffID == user.UserID)
                                       ?.Where(item => (item.DocumentID > 0 && HolidayServices.GetBusinessDays(item.Time, item.Document.ReleaseDate) > 3)).Count() ?? 0,
                    NoDocumentLate = MettingServices.GetList()
                                       ?.Where(item => item.StaffID == user.UserID)
                                       ?.Where(item => (item.DocumentID <= 0 && HolidayServices.GetBusinessDays(item.Time, DateTime.Now) > 3)).Count() ?? 0,
                    DocumentOnTime = MettingServices.GetList()
                                       ?.Where(item => item.StaffID == user.UserID)
                                       ?.Where(item => (item.DocumentID > 0 && HolidayServices.GetBusinessDays(item.Time, item.Document.ReleaseDate) <= 3)).Count() ?? 0,
                    Total = items.Count
                });
            }

            return result;
        }

        /// <summary>
        /// Lấy số liệu thống kê tình hình thực hiện ý kiến chỉ đạo
        /// </summary>
        /// <param name="MaNguoiTheoDoi"></param>
        /// <param name="MaLanhDao"></param>
        /// <param name="MaDonVi"></param>
        /// <returns></returns>
        public static SoLieuThongKe LaySoLieuThongKe(int MaNguoiTheoDoi = 0, int MaLanhDao = 0, int MaDonVi = 0)
        {
            if (MaNguoiTheoDoi > 0)
            {
                var items = GetList(MaNguoiTheoDoi: MaNguoiTheoDoi);

                if (items != null)
                    return new SoLieuThongKe
                    {
                        NotPerform = items.Count(item => item.Status == 0),
                        NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date),
                        NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date),
                        Performing = items.Count(item => item.Status == 1),
                        PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date),
                        PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date),
                        WaitToConfirm = PerformServices.GetList(MaNguoiTheoDoi: MaNguoiTheoDoi).Where(item => item.Status == 3).Select(item => item.RequestID).Distinct().Count(),
                        Done = items.Count(item => item.Status == 2),
                        DoneInTerm = items.Count(item => item.Status == 2 && item.RequiredDate.Date >= item.FinishedOnDate.Date),
                        DoneOutTerm = items.Count(item => item.Status == 2 && item.RequiredDate.Date < item.FinishedOnDate.Date),
                        Total = items.Count
                    };
            }
            else if (MaLanhDao > 0)
            {
                var items = GetList(MaNguoiGiaoViec: MaLanhDao);

                if (items != null)
                    return new SoLieuThongKe
                    {
                        NotPerform = items.Count(item => item.Status == 0),
                        NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date),
                        NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date),
                        Performing = items.Count(item => item.Status == 1),
                        PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date),
                        PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date),
                        WaitToConfirm = PerformServices.GetList(MaNguoiGiaoViec: MaLanhDao).Where(item => item.Status == 3).Select(item => item.RequestID).Distinct().Count(),
                        Done = items.Count(item => item.Status == 2),
                        DoneInTerm = items.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                        DoneOutTerm = items.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
                        Total = items.Count
                    };
            }
            else if (MaDonVi > 0)
            {
                var items = GetList(MaDonVi: MaDonVi);

                if (items != null)
                    return new SoLieuThongKe
                    {
                        NotPerform = items.Count(item => item.Status == 0),
                        NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date),
                        NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date),
                        Performing = items.Count(item => item.Status == 1),
                        PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date),
                        PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date),
                        WaitToConfirm = items.Count(item => item.Status == 3),
                        Done = items.Count(item => item.Status == 2),
                        DoneInTerm = items.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                        DoneOutTerm = items.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
                        Total = items.Count
                    };
            }

            return new SoLieuThongKe();
        }

        /// <summary>
        /// Lấy danh sách ý kiến chỉ đạo
        /// </summary>
        /// <param name="MaNguoiGiaoViec"></param>
        /// <param name="MaNguoiTheoDoi"></param>
        /// <param name="MaDonVi"></param>
        /// <param name="MaVanBan"></param>
        /// <param name="TrangThai"></param>
        /// <param name="searchText"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static List<Request> GetList(int MaNguoiGiaoViec = 0, int MaNguoiTheoDoi = 0, int MaDonVi = 0, int MaTruongPhong = 0, long MaVanBan = 0, string TrangThai = "", DateTime? tuNgay = null, string searchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                List<Request> result = null;

                if (MaNguoiGiaoViec > 0)
                    result = db.Fetch<Request>(
                        @"SELECT [Requests].* 
                          FROM [Requests]
                            LEFT JOIN [Documents] ON [Requests].[DocumentID] = [Documents].[DocumentID]
                          WHERE
                            ([Documents].[SignerID] = @0 OR [Requests].[RequesterID] = @0)
                            AND [Requests].[IsDeleted] = 0", MaNguoiGiaoViec).GroupBy(item => item.RequestID).Select(g => g.First()).ToList();

                if (MaNguoiTheoDoi > 0)
                    result = db.Fetch<Request>(
                        @"SELECT [Requests].*
                          FROM [Requests]                            
                            LEFT JOIN [Trackings] ON [Requests].[RequestID] = [Trackings].[RequestID]
                          WHERE 
                            [Trackings].[UserID] = @0
                            AND [Requests].[IsDeleted] = 0", MaNguoiTheoDoi).GroupBy(item => item.RequestID).Select(g => g.First()).ToList();

                if (MaDonVi > 0)
                    result = db.Fetch<Request>(
                        @"SELECT [Requests].RequestID,
                                 [Requests].[DocumentID],
                                 [Requests].[RequestContent],
                                 [Requests].[RequesterID],
                                 [Requests].[RequesterName],
                                 [Requests].[RequestContent],
                                 [Performs].[PerformID],
                                 [Performs].[Status],
                                 [Performs].[RequiredDate],
                                 [Performs].[FinishedOnDate],
                                 [Performs].[IsSynced]
                          FROM [Requests]                            
                            INNER JOIN [Performs] ON [Requests].[RequestID] = [Performs].[RequestID]
                          WHERE 
                            [Performs].[AgencyID] = @0
                            AND [Requests].[IsDeleted] = 0", MaDonVi).GroupBy(item => item.RequestID).Select(g => g.First()).ToList();

                if (MaVanBan > 0)
                {
                    result = db.Fetch<Request>(
                        @"SELECT [Requests].* 
                          FROM [Requests]
                          WHERE
                            [Requests].[DocumentID] = @0
                            AND [Requests].[IsDeleted] = 0", MaVanBan);
                }

                if (tuNgay != null)
                {
                    result = result?.Where(item => item.RequiredDate.Date >= tuNgay.Value.Date).ToList();
                }

                if (!string.IsNullOrEmpty(searchText.Trim()))
                {
                    result =
                        result?.Where(
                                item => item.RequestContent.ToUnsign().ToUpper().Contains(searchText.ToUnsign().ToUpper())
                                || item.Document.DocumentCode.ToUnsign().ToUpper().Contains(searchText.ToUnsign().ToUpper())
                                )
                            .ToList();
                }

                switch (TrangThai)
                {
                    case Components.TrangThai.ChuaThucHien:
                        result = result?.Where(item => item.Status == 0).ToList();
                        break;
                    case Components.TrangThai.ChuaThucHienTrongHan:
                        result = result?.Where(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date).ToList();
                        break;
                    case Components.TrangThai.ChuaThucHienQuaHan:
                        result = result?.Where(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date).ToList();
                        break;
                    case Components.TrangThai.DangThucHien:
                        result = result?.Where(item => item.Status == 1).ToList();
                        break;
                    case Components.TrangThai.DangThucHienTrongHan:
                        result = result?.Where(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date).ToList();
                        break;
                    case Components.TrangThai.DangThucHienQuaHan:
                        result = result?.Where(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date).ToList();
                        break;
                    case Components.TrangThai.ChoXacNhan:
                        {
                            result = MaDonVi > 0 ?
                                    result?.Where(item => item.Status == 3).ToList()
                                    : result?.Where(item => item.Performs.Any(perform => perform.Status == 3)).ToList();
                            break;
                        }
                    case Components.TrangThai.DaThucHien:
                        result = result?.Where(item => item.Status == 2).ToList();
                        break;
                    case Components.TrangThai.DaThucHienDungHan:
                        result = result?.Where(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date).ToList();
                        break;
                    case Components.TrangThai.DaThucHienTreHan:
                        result = result?.Where(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date).ToList();
                        break;
                }

                return result;
            }
        }

        /// <summary>
        /// Tìm kiếm ý kiến chỉ đạo theo các tiêu chí
        /// </summary>
        /// <param name="SoHieuVanBan"></param>
        /// <param name="TrichYeu"></param>
        /// <param name="MaNguoiGiaoViec"></param>
        /// <param name="MaNguoiTheoDoi"></param>
        /// <param name="NoiDungChiDao"></param>
        /// <param name="MaDonViThucHien"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static List<Request> Search(string SoHieuVanBan = "", string TrichYeu = "",
            List<int> MaNguoiGiaoViec = null, List<int> MaNguoiTheoDoi = null, string NoiDungChiDao = null, List<int> MaDonViThucHien = null, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                List<Request> items = db.Fetch<Request>(
                        @"SELECT [Requests].*, [Documents].[DocumentCode] as [DocumentCode], [Documents].[MainContent] as [MainContent] FROM [Requests]
                        INNER JOIN [Documents] ON [Requests].[DocumentID] = [Documents].[DocumentID]
                      WHERE [Requests].[IsDeleted] = 0");

                if (!string.IsNullOrEmpty(SoHieuVanBan))
                {
                    SoHieuVanBan = SoHieuVanBan.ToUnsign().ToUpper();
                    items = items.Where(request => request.DocumentCode != null && request.DocumentCode.ToUnsign().ToUpper().Contains(SoHieuVanBan)).ToList();
                }

                if (!string.IsNullOrEmpty(TrichYeu))
                {
                    TrichYeu = TrichYeu.ToUpper();
                    items = items.Where(request => request.MainContent.ToUpper().Contains(TrichYeu)).ToList();
                }

                if (!string.IsNullOrEmpty(NoiDungChiDao))
                {
                    NoiDungChiDao = NoiDungChiDao.ToUpper();
                    items = items.Where(request => request.RequestContent.ToUpper().Contains(NoiDungChiDao)).ToList();
                }

                if (MaNguoiGiaoViec != null && MaNguoiGiaoViec.Count > 0)
                {
                    items = items.Where(request => MaNguoiGiaoViec.Contains(request.RequesterID)).ToList();
                }

                if (MaNguoiTheoDoi != null && MaNguoiTheoDoi.Count > 0)
                {
                    items = items.Where(request => request.Trackings.Select(i => i.UserID).Contains(MaNguoiTheoDoi.First())).ToList();
                }

                if (MaDonViThucHien != null && MaDonViThucHien.Count > 0)
                {
                    items = items.Where(request => MaDonViThucHien.Exists(id => request.Performs.Select(i => i.AgencyID).Contains(id))).ToList();
                }

                return items;
            }
        }

        /// <summary>
        /// Cập nhật lại trạng thái thực hiện ý kiến chỉ đạo
        /// </summary>
        /// <param name="MaYKCD"></param>
        /// <param name="connectionName"></param>
        public static void CapNhatTrangThaiYKCD(long MaYKCD, string connectionName = "DatabaseConnection")
        {
            var request = GetById(MaYKCD);

            if (request.Performs.TrueForAll(item => item.Status == 0))
            {
                request.Status = 0;
            }
            else if (request.Performs.TrueForAll(item => item.Status == 2))
            {
                request.Status = 2;
                request.FinishedOnDate = request.Performs.OrderByDescending(p => p.FinishedOnDate).First().FinishedOnDate;
            }
            else
            {
                request.Status = 1;
            }

            Update(request);

            HttpContext.Current.Cache.Remove("SoLieuThongKeTatCaNhomDonVi");
        }

        public static void Create(Request request, List<int> trackerIds, List<int> agencyIds)
        {
            Create(request);

            foreach (var trackerId in trackerIds)
            {
                Tracking tracking = new Tracking(request.RequestID, trackerId);
                TrackingServices.Create(tracking);
            }

            foreach (var agencyId in agencyIds)
            {
                Perform perform = new Perform(request.RequestID, agencyId, request.RequiredDate);
                PerformServices.Create(perform);

                NotificationServices.Create(new Notification(perform.RequestID, 0, perform.AgencyID, $"UBND tỉnh có văn bản số {request.Document.DocumentCode} ban hành ngày {request.Document.ReleaseDate.ToDateString()}, yêu cầu {perform.Agency.AgencyName} báo cáo kết quả trước ngày {perform.RequiredDate.ToDateTime()}."));
            }

            HttpContext.Current.Cache.Remove("SoLieuThongKeTatCaNhomDonVi");
        }

        public static void Update(Request request, List<int> trackerIds, List<int> agencyIds)
        {
            Update(request);

            #region Lưu thông tin chuyên viên theo dõi
            var oldTrackers = TrackingServices.GetList(request.RequestID);

            foreach (var oldTracker in oldTrackers)
            {
                if (!trackerIds.Contains(oldTracker.UserID))
                {
                    TrackingServices.Delete(oldTracker.TrackingID);
                }
            }

            foreach (var trackerId in trackerIds)
            {
                if (!oldTrackers.Select(t => t.UserID).Contains(trackerId))
                {
                    TrackingServices.Create(new Tracking(request.RequestID, trackerId));
                }
            }
            #endregion

            #region Lưu thông tin đơn vị thực hiện
            var oldPerforms = request.Performs;

            foreach (var oldPerform in oldPerforms)
            {
                if (!agencyIds.Contains(oldPerform.AgencyID))
                {
                    PerformServices.Delete(oldPerform.PerformID);
                    AgencyServiceHelper.DeleteRequest(oldPerform.Agency.ServiceUrl, oldPerform.RequestID);
                }
            }

            foreach (var agencyId in agencyIds)
            {
                if (!oldPerforms.Select(t => t.AgencyID).Contains(agencyId))
                {
                    Perform perform = new Perform(request.RequestID, agencyId, request.RequiredDate);
                    PerformServices.Create(perform);

                    NotificationServices.Create(new Notification(perform.RequestID, 0, perform.AgencyID, $"UBND tỉnh có văn bản số {request.Document.DocumentCode} ban hành ngày {request.Document.ReleaseDate.ToDateString()}, yêu cầu {perform.Agency.AgencyName} báo cáo kết quả trước ngày {perform.RequiredDate.ToDateTime()}."));
                }
                else
                {
                    var perform = PerformServices.GetList(request.RequestID, AgencyID: agencyId).FirstOrDefault();

                    if (perform != null)
                    {
                        perform.RequiredDate = request.RequiredDate;
                        perform.IsSynced = false;
                        PerformServices.Update(perform);
                    }
                }
            }
            #endregion

            HttpContext.Current.Cache.Remove("SoLieuThongKeTatCaNhomDonVi");
        }

        public static void DeleteRequest(long requestID)
        {
            var request = RequestServices.GetById(requestID);

            if (request != null)
            {
                foreach (Perform perform in request.Performs)
                {
                    if (perform != null)
                    {
                        if (!string.IsNullOrEmpty(perform.Agency?.ServiceUrl))
                        {
                            AgencyServiceHelper.DeleteRequest(perform.Agency.ServiceUrl, perform.RequestID);
                        }

                        PerformServices.Delete(perform.PerformID);
                    }
                }

                Delete(requestID);
            }
        }
    }
}