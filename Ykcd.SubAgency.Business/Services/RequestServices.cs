using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Framework.Helper;
using PetaPoco;
using YKCD.SubAgency.Business.Components;
using YKCD.SubAgency.Business.Entities;
using Framework.Util;

namespace YKCD.SubAgency.Business.Services
{
    public class RequestServices : BaseService<Request>
    {
        /// <summary>
        /// Lấy kết quả thống kê tình hình thực hiện YKCD của tất cả các đơn vị
        /// </summary>
        /// <param name="tuNgay">Mốc thống kê từ ngày</param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static List<SoLieuThongKe> ThongKeTheoTatCaNhomDonVi(DateTime? tuNgay = null, DateTime? denNgay = null, string connectionName = "DatabaseConnection")
        {
            var result = new List<SoLieuThongKe>();

            foreach (var departmentGroup in DepartmentGroupServices.GetList(isShowStatistic: true))
            {
                var items = PerformServices.GetList(departmentGroupId: departmentGroup.DepartmentGroupID, fromDate: tuNgay, toDate: denNgay);

                result.Add(new SoLieuThongKe
                {
                    ObjectID = departmentGroup.DepartmentGroupID,
                    ObjectName = departmentGroup.DepartmentGroupName,
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

        public static List<SoLieuThongKe> ThongKeTheoTatCaNhomDonVi_TrangChu(string connectionName = "DatabaseConnection")
        {
            var result = new List<SoLieuThongKe>();

            foreach (var departmentGroup in DepartmentGroupServices.GetList(isShowStatistic: true))
            {
                var items = PerformServices.GetList(departmentGroupId: departmentGroup.DepartmentGroupID);
                var doneItems = PerformServices.GetList(departmentGroupId: departmentGroup.DepartmentGroupID, fromDate: new DateTime(DateTime.Now.Year, 1, 1));

                result.Add(new SoLieuThongKe
                {
                    ObjectID = departmentGroup.DepartmentGroupID,
                    ObjectName = departmentGroup.DepartmentGroupName,
                    NotPerform = items.Count(item => item.Status == 0),
                    NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date),
                    NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date),
                    Performing = items.Count(item => item.Status == 1),
                    PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date),
                    PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date),
                    WaitToConfirm = items.Count(item => item.Status == 3),
                    Done = doneItems.Count(item => item.Status == 2),
                    DoneInTerm = doneItems.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                    DoneOutTerm = doneItems.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
                    Total = items.Count
                });
            }

            return result;
        }

        public static List<SoLieuThongKe> ThongKeDonViTheoNhom(int DepartmentGroupID, DateTime? tuNgay = null, DateTime? denNgay = null, string connectionName = "DatabaseConnection")
        {
            var result = new List<SoLieuThongKe>();

            foreach (var department in DepartmentServices.GetList(DepartmentGroupID: DepartmentGroupID).OrderBy(department => department.DisplayOrder).ThenBy(department => department.DepartmentName))
            {
                var items = PerformServices.GetList(departmentId: department.DepartmentID, fromDate: tuNgay, toDate: denNgay);

                result.Add(new SoLieuThongKe
                {
                    ObjectID = department.DepartmentID,
                    ObjectName = department.DepartmentName,
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

        public static List<SoLieuThongKe> ThongKeTheoTatCaLanhDaoGiaoViec(DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            var result = new List<SoLieuThongKe>();

            foreach (var user in UserServices.GetList(roles: new[] { 6, 9 }))
            {
                var items = GetList(MaNguoiGiaoViec: user.UserID, tuNgay: tuNgay, denNgay: denNgay);

                result.Add(new SoLieuThongKe
                {
                    ObjectID = user.UserID,
                    ObjectName = user.FullName,
                    GroupID = user.DepartmentID,
                    GroupName = user.Department?.DepartmentName,
                    NotPerform = items.Count(item => item.Status == 0),
                    NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date),
                    NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date),
                    Performing = items.Count(item => item.Status == 1),
                    PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date),
                    PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date),
                    WaitToConfirm = PerformServices.GetList(maNguoiGiaoViec: user.UserID).Where(item => item.Status == 3).Select(item => item.RequestID).Distinct().Count(),
                    Done = items.Count(item => item.Status == 2),
                    DoneInTerm = items.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                    DoneOutTerm = items.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
                    Total = items.Count
                });
            }

            return result;
        }

        public static List<SoLieuThongKe> ThongKeTheoTatCaChuyenVienTheoDoi(DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            var result = new List<SoLieuThongKe>();

            foreach (var user in UserServices.GetList(roles: new[] { UserRole.ChuyenVienVP, UserRole.Administrator }))
            {
                var items = GetList(MaNguoiTheoDoi: user.UserID, tuNgay: tuNgay, denNgay: denNgay);

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
                    WaitToConfirm = PerformServices.GetList(maNguoiTheoDoi: user.UserID, fromDate: tuNgay).Where(item => item.Status == 3).Select(item => item.RequestID).Distinct().Count(),
                    Done = items.Count(item => item.Status == 2),
                    DoneInTerm = items.Count(item => item.Status == 2 && item.RequiredDate.Date >= item.FinishedOnDate.Date),
                    DoneOutTerm = items.Count(item => item.Status == 2 && item.RequiredDate.Date < item.FinishedOnDate.Date),
                    Total = items.Count
                });
            }

            return result;
        }

        public static SoLieuThongKe LaySoLieuThongKe(int MaNguoiTheoDoi = 0, int MaLanhDao = 0, int MaDonVi = 0, int MaNguoiThucHien = 0, bool YkcdCuaUbndTinh = false, DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            if (MaNguoiTheoDoi > 0)
            {
                var items = GetList(MaNguoiTheoDoi: MaNguoiTheoDoi, tuNgay: tuNgay);

                if (items != null)
                    return new SoLieuThongKe
                    {
                        NotPerform = items.Count(item => item.Status == 0),
                        NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date),
                        NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date),
                        Performing = items.Count(item => item.Status == 1),
                        PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date),
                        PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date),
                        WaitToConfirm = PerformServices.GetList(maNguoiTheoDoi: MaNguoiTheoDoi).Where(item => item.Status == 3).Select(item => item.RequestID).Distinct().Count(),
                        Done = items.Count(item => item.Status == 2),
                        DoneInTerm = items.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                        DoneOutTerm = items.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
                        Total = items.Count
                    };
            }
            else if (MaLanhDao > 0)
            {
                var items = GetList(MaNguoiGiaoViec: MaLanhDao, tuNgay: tuNgay);

                if (items != null)
                    return new SoLieuThongKe
                    {
                        NotPerform = items.Count(item => item.Status == 0),
                        NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date),
                        NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date),
                        Performing = items.Count(item => item.Status == 1),
                        PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date),
                        PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date),
                        WaitToConfirm = PerformServices.GetList(maNguoiGiaoViec: MaLanhDao).Where(item => item.Status == 3).Select(item => item.RequestID).Distinct().Count(),
                        Done = items.Count(item => item.Status == 2),
                        DoneInTerm = items.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                        DoneOutTerm = items.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
                        Total = items.Count
                    };
            }
            else if (MaDonVi > 0)
            {
                var items = GetList(MaDonViThucHien: MaDonVi, tuNgay: tuNgay);

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
            else if (MaNguoiThucHien > 0)
            {
                var items = GetList(MaNguoiThucHien: MaNguoiThucHien, tuNgay: tuNgay);

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
            else if (YkcdCuaUbndTinh)
            {
                var items = GetList(YkcdCuaUbndTinh: true, tuNgay: tuNgay);

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

        public static SoLieuThongKe LaySoLieuThongKe_TrangChu(int MaNguoiTheoDoi = 0, int MaLanhDao = 0, int MaDonVi = 0, bool YkcdCuaUbndTinh = false)
        {
            if (MaNguoiTheoDoi > 0)
            {
                var items = GetList(MaNguoiTheoDoi: MaNguoiTheoDoi);

                var doneItems = GetList(MaNguoiTheoDoi: MaNguoiTheoDoi, tuNgay: new DateTime(DateTime.Now.Year, 1, 1));

                if (items != null)
                    return new SoLieuThongKe
                    {
                        NotPerform = items.Count(item => item.Status == 0),
                        NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date),
                        NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date),
                        Performing = items.Count(item => item.Status == 1),
                        PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date),
                        PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date),
                        WaitToConfirm = PerformServices.GetList(maNguoiTheoDoi: MaNguoiTheoDoi).Where(item => item.Status == 3).Select(item => item.RequestID).Distinct().Count(),
                        Done = doneItems.Count(item => item.Status == 2),
                        DoneInTerm = doneItems.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                        DoneOutTerm = doneItems.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
                        Total = items.Count
                    };
            }
            else if (MaLanhDao > 0)
            {
                var items = GetList(MaNguoiGiaoViec: MaLanhDao);
                var doneItems = GetList(MaNguoiGiaoViec: MaLanhDao, tuNgay: new DateTime(DateTime.Now.Year, 1, 1));

                if (items != null)
                    return new SoLieuThongKe
                    {
                        NotPerform = items.Count(item => item.Status == 0),
                        NotPerformInTerm = items.Count(item => item.Status == 0 && item.RequiredDate >= DateTime.Now.Date),
                        NotPerformOutTerm = items.Count(item => item.Status == 0 && item.RequiredDate < DateTime.Now.Date),
                        Performing = items.Count(item => item.Status == 1),
                        PerformingInTerm = items.Count(item => item.Status == 1 && item.RequiredDate >= DateTime.Now.Date),
                        PerformingOutTerm = items.Count(item => item.Status == 1 && item.RequiredDate < DateTime.Now.Date),
                        WaitToConfirm = PerformServices.GetList(maNguoiGiaoViec: MaLanhDao).Where(item => item.Status == 3).Select(item => item.RequestID).Distinct().Count(),
                        Done = doneItems.Count(item => item.Status == 2),
                        DoneInTerm = doneItems.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                        DoneOutTerm = doneItems.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
                        Total = items.Count
                    };
            }
            else if (MaDonVi > 0)
            {
                var items = GetList(MaDonViThucHien: MaDonVi);
                var doneItems = GetList(MaDonViThucHien: MaDonVi, tuNgay: new DateTime(DateTime.Now.Year, 1, 1));

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
                        Done = doneItems.Count(item => item.Status == 2),
                        DoneInTerm = doneItems.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                        DoneOutTerm = doneItems.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
                        Total = items.Count
                    };
            }
            else if (YkcdCuaUbndTinh)
            {
                var items = GetList(YkcdCuaUbndTinh: true);
                var doneItems = GetList(YkcdCuaUbndTinh: true, tuNgay: new DateTime(DateTime.Now.Year, 1, 1));

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
                        Done = doneItems.Count(item => item.Status == 2),
                        DoneInTerm = doneItems.Count(item => item.Status == 2 && item.RequiredDate >= item.FinishedOnDate.Date),
                        DoneOutTerm = doneItems.Count(item => item.Status == 2 && item.RequiredDate < item.FinishedOnDate.Date),
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
        /// <param name="MaDonViThucHien"></param>
        /// <param name="MaNguoiThucHien"></param>
        /// <param name="MaVanBan"></param>
        /// <param name="YkcdCuaUbndTinh"></param>
        /// <param name="TrangThai"></param>
        /// <param name="tuNgay"></param>
        /// <param name="denNgay"></param>
        /// <param name="searchText"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static List<Request> GetList(int MaNguoiGiaoViec = 0, int MaNguoiTheoDoi = 0, int MaDonViThucHien = 0, int MaNguoiThucHien = 0, long MaVanBan = 0, bool YkcdCuaUbndTinh = false, string TrangThai = "", DateTime? tuNgay = null, DateTime? denNgay = null, string searchText = "", string connectionName = "DatabaseConnection")
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
                            AND [Requests].[IsDeleted] = 0", MaNguoiGiaoViec).Distinct().ToList();

                if (MaNguoiTheoDoi > 0)
                    result = db.Fetch<Request>(
                        @"SELECT [Requests].*
                          FROM [Requests]
                            LEFT JOIN [Documents] ON [Requests].[DocumentID] = [Documents].[DocumentID]
                            INNER JOIN [Trackings] ON [Requests].[RequestID] = [Trackings].[RequestID]
                          WHERE 
                            [Trackings].[UserID] = @0
                            AND [Requests].[IsDeleted] = 0", MaNguoiTheoDoi).Distinct().ToList();

                if (MaDonViThucHien > 0)
                    result = db.Fetch<Request>(
                        @"SELECT [Requests].RequestID,
                                 [Requests].[DocumentID],
                                 [Requests].[RequestContent],
                                 [Requests].[RequesterID],
                                 [Requests].[RequesterName],
                                 [Requests].[RequestContent],
                                 [Performs].[Status],
                                 [Performs].[RequiredDate],
                                 [Performs].[FinishedOnDate]
                          FROM [Requests]
                            LEFT JOIN [Documents] ON [Requests].[DocumentID] = [Documents].[DocumentID]
                            LEFT JOIN [Performs] ON [Requests].[RequestID] = [Performs].[RequestID]
                          WHERE 
                            [Performs].[DepartmentID] = @0
                            AND [Requests].[IsDeleted] = 0", MaDonViThucHien).Distinct().ToList();

                if (MaNguoiThucHien > 0)
                    result = db.Fetch<Request>(
                        @"SELECT [Requests].RequestID,
                                 [Requests].[DocumentID],
                                 [Requests].[RequestContent],
                                 [Requests].[RequesterID],
                                 [Requests].[RequesterName],
                                 [Requests].[RequestContent],
                                 [Performs].[Status],
                                 [Performs].[RequiredDate],
                                 [Performs].[FinishedOnDate]
                          FROM [Requests]
                            LEFT JOIN [Documents] ON [Requests].[DocumentID] = [Documents].[DocumentID]
                            LEFT JOIN [Performs] ON [Requests].[RequestID] = [Performs].[RequestID]
                          WHERE 
                            [Performs].[UserID] = @0
                            AND [Requests].[IsDeleted] = 0", MaNguoiThucHien).Distinct().ToList();

                if (MaVanBan > 0)
                {
                    result = db.Fetch<Request>(
                        @"SELECT [Requests].* 
                          FROM [Requests]
                          WHERE
                            [Requests].[DocumentID] = @0
                            AND [Requests].[IsDeleted] = 0", MaVanBan);
                }

                if (YkcdCuaUbndTinh)
                {
                    result = db.Fetch<Request>(
                        @"SELECT [Requests].* 
                          FROM [Requests]
                          WHERE
                            [Requests].[IsAgencyRequest] = 1
                            AND [Requests].[IsDeleted] = 0");
                }

                if (tuNgay != null)
                {
                    result = result?.Where(item => item.RequiredDate.Date >= tuNgay.Value.Date).ToList();
                }

                if (denNgay != null)
                {
                    result = result?.Where(item => item.RequiredDate.Date <= tuNgay.Value.Date).ToList();
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
                            result = (MaDonViThucHien > 0 || YkcdCuaUbndTinh) ?
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

        public static List<Request> Search(string SoHieuVanBan = null, string TrichYeu = null,
            List<int> MaNguoiGiaoViec = null, List<int> MaNguoiTheoDoi = null, string NoiDungChiDao = null, List<int> MaDonViThucHien = null, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                var items = db.Fetch<Request>(
                    @"SELECT [Requests].*, [Documents].[DocumentCode] as [DocumentCode], [Documents].[MainContent] as [MainContent] FROM [Requests]
                        LEFT JOIN [Documents] ON [Requests].[DocumentID] = [Documents].[DocumentID]
                      WHERE [Requests].[IsDeleted] = 0");

                if (!string.IsNullOrEmpty(SoHieuVanBan))
                {
                    SoHieuVanBan = SoHieuVanBan.ToUnsign().ToUpper();
                    items = items.Where(request => request.DocumentCode != null && request.DocumentCode.ToUnsign().ToUpper().Contains(SoHieuVanBan)).ToList();
                }

                if (!string.IsNullOrEmpty(TrichYeu))
                {
                    TrichYeu = TrichYeu.ToUnsign().ToUpper();
                    items = items.Where(request => request.MainContent.ToUnsign().ToUpper().Contains(TrichYeu)).ToList();
                }

                if (MaNguoiGiaoViec != null && MaNguoiGiaoViec.Count > 0)
                {
                    items = items.Where(request => MaNguoiGiaoViec.Contains(request.RequesterID)).ToList();
                }

                if (MaNguoiTheoDoi != null && MaNguoiTheoDoi.Count > 0)
                {
                    items = items.Where(request => MaNguoiTheoDoi.Exists(id => request.Trackings.Select(i => i.UserID).Contains(id))).ToList();
                }

                if (!string.IsNullOrEmpty(NoiDungChiDao))
                {
                    NoiDungChiDao = NoiDungChiDao.ToUnsign().ToUpper();
                    items = items.Where(request => request.RequestContent.ToUnsign().ToUpper().Contains(NoiDungChiDao)).ToList();
                }

                if (MaDonViThucHien != null && MaDonViThucHien.Count > 0)
                {
                    items = items.Where(request => MaDonViThucHien.Exists(id => request.Performs.Select(i => i.DepartmentID).Contains(id))).ToList();
                }

                return items;
            }
        }

        /// <summary>
        /// Cập nhật lại trạng thái request sau khi cập nhật trạng thái perform bất kì
        /// </summary>
        /// <param name="MaYKCD"></param>
        /// <param name="connectionName"></param>
        public static int CapNhatTrangThaiYKCD(long MaYKCD, string connectionName = "DatabaseConnection")
        {
            var request = GetById(MaYKCD);

            if (request == null)
                return -1;

            //Nếu đây là ý kiến chỉ đạo của UBND tỉnh
            if (request.IsAgencyRequest)
            {
                //Nếu ykcd của UB tỉnh đã hoàn thành, cập nhật các nội dung giao việc trong đơn vị thành đã hoàn thành
                if (request.Status == 2)
                {
                    foreach (var perform in request.Performs)
                    {
                        perform.Status = 2;
                        perform.FinishedOnDate = request.FinishedOnDate;
                        PerformServices.Update(perform);
                    }
                }
                //Nếu ykcd đã giao cho phòng ban thực hiện, cập nhật trạng thái ykcd thành đang thực hiện
                else if (request.Status == 0 && request.IsAssignPerform)
                {
                    request.Status = 1;
                }
                //Nếu các nội dung giao việc đã hoàn thành, cập nhật trạng thái ykcd thành chờ xác nhận
                else if (request.Performs != null && request.Performs?.Count > 0 && request.Performs.TrueForAll(item => item.Status == 2) && request.Status != 2)
                {
                    request.Status = 3;
                }
                else if (request.Performs != null && request.Performs?.Count > 0 && request.Performs.TrueForAll(item => item.Status == 2))
                {
                    request.Status = 2;
                }
                else
                {
                    request.Status = 1;
                }
            }
            //Nếu đây là ykcd nội bộ đơn vị
            else
            {
                if (request.Performs.TrueForAll(item => item.Status == 0))
                {
                    request.Status = 0;
                }
                else if (request.Performs.TrueForAll(item => item.Status == 2))
                {
                    request.Status = 2;
                }
                else
                {
                    request.Status = 1;
                }
            }

            Update(request);

            return request.Status;
        }

        /// <summary>
        /// Lấy thông tin ý kiến chỉ đạo theo mã ý kiến chỉ đạo đơn vị cha
        /// </summary>
        /// <param name="AgencyID"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static Request GetByAgencyID(long AgencyID, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.FirstOrDefault<Request>("WHERE [AgencyRequestID] = @0 AND [IsDeleted] = 0", AgencyID);
            }
        }

        /// <summary>
        /// Lưu thông tin ý kiến chỉ đạo mới
        /// </summary>
        /// <param name="request"></param>
        /// <param name="trackerIds"></param>
        /// <param name="departmentIds"></param>
        /// <param name="userIds"></param>
        public static void Create(Request request, List<int> trackerIds, List<int> departmentIds = null, List<int> userIds = null, bool isFinishedConfirm = false)
        {
            Create(request);

            if (trackerIds != null)
            {
                foreach (var trackerId in trackerIds)
                {
                    Tracking tracking = new Tracking(request.RequestID, trackerId);
                    TrackingServices.Create(tracking);
                }
            }

            if (departmentIds != null)
            {
                foreach (var departmentId in departmentIds)
                {
                    Perform perform = new Perform(requestId: request.RequestID, departmentId: departmentId, requiredDate: request.RequiredDate);
                    perform.IsFinishedConfirm = isFinishedConfirm;
                    PerformServices.CreatePerform(perform);
                }
            }

            if (userIds != null)
            {
                foreach (var userId in userIds.Where(u => !departmentIds.Contains(UserServices.GetById(u).DepartmentID)))
                {
                    Perform perform = new Perform(requestId: request.RequestID, userId: userId, requiredDate: request.RequiredDate);
                    PerformServices.CreatePerform(perform);
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin ý kiến chỉ đạo
        /// </summary>
        /// <param name="request"></param>
        /// <param name="trackerIds"></param>
        /// <param name="departmentIds"></param>
        /// <param name="userIds"></param>
        public static void Update(Request request, List<int> trackerIds, List<int> departmentIds = null, List<int> userIds = null, bool isFinishedConfirm = false)
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
            if (departmentIds != null)
            {
                var oldDepartmentPerforms = request.Performs.Where(p => p.DepartmentID > 0);

                foreach (var oldPerform in oldDepartmentPerforms)
                {
                    if (!departmentIds.Contains(oldPerform.DepartmentID))
                    {
                        PerformServices.Delete(oldPerform.PerformID);
                    }
                }

                foreach (var departmentId in departmentIds)
                {
                    if (!oldDepartmentPerforms.Select(t => t.DepartmentID).Contains(departmentId))
                    {
                        Perform perform = new Perform(requestId: request.RequestID, departmentId: departmentId, requiredDate: request.RequiredDate);
                        perform.IsFinishedConfirm = isFinishedConfirm;
                        PerformServices.CreatePerform(perform);
                    }
                    else
                    {
                        var perform = PerformServices.GetList(request.RequestID, departmentId: departmentId).FirstOrDefault();

                        if (perform != null)
                        {
                            perform.RequiredDate = request.RequiredDate;
                            perform.IsFinishedConfirm = isFinishedConfirm;
                            PerformServices.Update(perform);
                        }
                    }
                }
            }
            #endregion

            #region Lưu thông tin người thực hiện
            if (userIds != null)
            {
                var oldUserPerforms = request.Performs.Where(p => p.UserID > 0);

                foreach (var oldPerform in oldUserPerforms)
                {
                    if (!userIds.Contains(oldPerform.UserID))
                    {
                        PerformServices.Delete(oldPerform.PerformID);
                    }
                }

                foreach (var userId in userIds)
                {
                    if (!oldUserPerforms.Select(t => t.UserID).Contains(userId))
                    {
                        Perform perform = new Perform(requestId: request.RequestID, userId: userId, requiredDate: request.RequiredDate);
                        PerformServices.CreatePerform(perform);
                    }
                    else
                    {
                        var perform = PerformServices.GetList(request.RequestID, userId: userId).FirstOrDefault();

                        if (perform != null)
                        {
                            perform.RequiredDate = request.RequiredDate;
                            PerformServices.Update(perform);
                        }
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// Giao việc một ý kiến chỉ đạo cho cá nhân, đơn vị thực hiện
        /// </summary>
        /// <param name="request">Thông tin ý kiến chỉ đạo</param>
        /// <param name="requesterId">Mã người giao việc</param>
        /// <param name="trackerIds">Mã người theo dõi</param>
        /// <param name="departmentIds">Mã đơn vị thực hiện</param>
        /// <param name="isFinishedConfirm">Có cần xác nhận</param>
        public static void Assign(Request request, int requesterId, List<int> trackerIds, List<int> departmentIds, bool isFinishedConfirm)
        {
            foreach (var trackerId in trackerIds)
            {
                Tracking tracking = new Tracking(request.RequestID, trackerId);
                TrackingServices.Create(tracking);
            }

            foreach (var departmentId in departmentIds)
            {
                Perform perform = new Perform(requestId: request.RequestID, departmentId: departmentId, requiredDate: request.RequiredDate);
                perform.IsFinishedConfirm = isFinishedConfirm;
                PerformServices.CreatePerform(perform);
            }

            var orgRequest = GetById(request.RequestID);
            orgRequest.Status = 1;
            orgRequest.IsAssignPerform = true;
            orgRequest.RequesterID = requesterId;

            Update(orgRequest);

            if (request.IsAgencyRequest)
            {
                orgRequest.CreatedBy = CommonSessions.UserID;
                Update(orgRequest);

                Report report = new Report()
                {
                    ReportContent = $"Đã giao cho {request.Departments?.Select(i => i.DepartmentName)?.ToList()?.DisplayInline()} thực hiện",
                    PerformOnDate = DateTime.Now,
                    RequestID = request.RequestID,
                    Status = 1
                };

                AgencyServiceHelper.ReceiveReport(ConfigurationManager.AppSettings["Agency_Service"], report);
            }
        }
    }
}
