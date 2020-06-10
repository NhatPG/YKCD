using System.Collections.Generic;
using System.Linq;
using Framework.Helper;
using Framework.Util;
using YKCD.Agency.Business.Components;
using YKCD.Agency.Business.Entities;

namespace YKCD.Agency.Business.Services
{
    public static class CommonServices
    {
        public static string ToLabel(this object status)
        {
            switch (status.ToInteger())
            {
                case 0:
                    return "<span class=\"label label-danger\">Chưa thực hiện</span>";
                case 1:
                    return "<span class=\"label label-info\">Đang thực hiện</span>";
                case 2:
                    return "<span class=\"label label-success\">Đã hoàn thành</span>";
                case 3:
                    return "<span class=\"label label-warning\">Chờ xác nhận</span>";
            }
            return string.Empty;
        }

        public static string ToText(this object status)
        {
            switch (status.ToInteger())
            {
                case 0:
                    return "<span class=\"text-danger\">Chưa thực hiện</span>";
                case 1:
                    return "<span class=\"text-info\">Đang thực hiện</span>";
                case 2:
                    return "<span class=\"text-success\">Đã hoàn thành</span>";
                case 3:
                    return "<span class=\"text-warning\">Chờ xác nhận</span>";
            }
            return string.Empty;
        }

        /// <summary>
        /// Hiển thị danh sách đơn vị (cá nhân) thực hiện ý kiến chỉ đạo 
        /// </summary>
        /// <param name="requestID">Mã ý kiến chỉ đạo</param>
        /// <returns></returns>
        public static string ShowPerformObjects(long requestID)
        {
            var request = RequestServices.GetById(requestID);

            if (request == null)
                return string.Empty;

            string result = string.Empty;
            var performs = request.Performs;

            //Nếu tài khoản đăng nhập có quyền xác nhận hoàn thành, hiển thị đối tượng thực hiện và link xác nhận hoàn thành
            if (request.CoQuyenXacNhan)
            {
                foreach (var perform in performs)
                {
                    if (perform.DepartmentID > 0 && perform.Department != null)
                        result += $"{perform.Department?.DepartmentName} (<a href=\"#\" data-toggle=\"modal\" data-target=\".my-modal-lg\" data-link=\"{Redirector.GetLink("Agency/XacNhanHoanThanh.aspx", "id", perform.PerformID)}\">{perform.StatusString}</a>)<br/>";
                    else if (perform.UserID > 0 && perform.User != null)
                        result += $"{perform.User?.FullName} (<a href=\"#\" data-toggle=\"modal\" data-target=\".my-modal-lg\" data-link=\"{Redirector.GetLink("Agency/XacNhanHoanThanh.aspx", "id", perform.PerformID)}\">{perform.StatusString}</a>)<br/>";
                }
            }

            //Nếu tài khoản đăng nhập không có quyền xác nhận hoàn thành, hiển thị thông tin đối tượng thực hiện
            else
            {
                foreach (var item in performs)
                {
                    if (item.DepartmentID > 0 && item.Department != null)
                        result += $"{item.Department.DepartmentName} ({item.StatusString})<br/>";
                    else if (item.UserID > 0 && item.User != null)
                        result += $"{item.User.FullName} ({item.StatusString})<br/>";
                }
            }

            return result;
        }

        /// <summary>
        /// Kiểm tra quyền báo cáo tình hình thực hiện ý kiến chỉ đạo
        /// </summary>
        /// <param name="requestID"></param>
        /// <returns></returns>
        public static bool CheckReportPermission(long requestID)
        {
            //Nếu là tài khoản admin thì được báo cáo tình hình thực hiện tất cả những YKCD của đơn vị
            if (CommonSessions.Role == UserRole.Administrator)
                return true;

            var request = RequestServices.GetById(requestID);

            if (request == null)
                return false;

            //Nếu đây là ý kiến chỉ đạo của UBND tỉnh
            if (request.IsProvinceRequest)
            {
                //Lãnh đạo đơn vị, lãnh đạo VP, chuyên viên VP, quản trị hệ thống có quyền báo cáo trực tiếp
                if (CommonSessions.Role == UserRole.LanhDaoDonVi || CommonSessions.Role == UserRole.LanhDaoVP || CommonSessions.Role == UserRole.ChuyenVienVP || CommonSessions.Role == UserRole.Administrator)
                {
                    return true;
                }
            }

            //Nếu tài khoản đang đăng nhập là tài khoản cá nhân
            if (CommonSessions.UserID > 0)
            {
                //Người nhập YKCD, người theo dõi, người yêu cầu, người thực hiện có quyền báo cáo
                if (request.CreatedBy == CommonSessions.UserID || request.Trackings.Any(t => t.UserID == CommonSessions.UserID) || request.Performs.Any(p => p.UserID == CommonSessions.UserID) || request.RequesterID == CommonSessions.UserID)
                {
                    return true;
                }
            }

            //Nếu tài khoản đang đăng nhập là tài khoản đơn vị
            if (CommonSessions.DepartmentID > 0)
            {
                //Những đưn vị được phân công thực hiện có quyền báo cáo
                if (PerformServices.GetList(requestID).Any(item => item.DepartmentID == CommonSessions.DepartmentID))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestID"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public static bool CheckUpdatePermission(long requestID = 0, long documentId = 0)
        {
            var request = RequestServices.GetById(requestID);
            var document = DocumentServices.GetById(documentId);

            //Nếu đây là văn bản của UB tỉnh, thì ko được cập nhật
            if (document != null && document.ProvinceDocumentID > 0)
                return false;

            //Nếu đây là ykcd của UB tỉnh, thì ko được cập nhật nếu chưa giao việc
            if (request != null && request.IsProvinceRequest && !request.IsAssignPerform)
                return false;

            //Nếu là tài khoản admin thì được cập nhật tất cả những YKCD nội bộ đơn vị
            if (CommonSessions.Role == UserRole.Administrator)
                return true;

            if (requestID > 0 && request != null)
            {
                if (CommonSessions.UserID > 0 && (request.CreatedBy == CommonSessions.UserID || request.RequesterID == CommonSessions.UserID || request.Trackings.Any(t => t.UserID == CommonSessions.UserID)))
                    return true;
            }
            else if (documentId > 0 && document != null)
            {
                if ((document.ProvinceDocumentID <= 0 || document.ProvinceDocumentID == null) && CommonSessions.UserID > 0 &&
                    (document.CreatedBy == CommonSessions.UserID || document.SignerID == CommonSessions.UserID ||
                     document.WriterID == CommonSessions.UserID))
                    return true;
            }

            return false;
        }

        public static bool CheckConfirmPermission(long requestID = 0, long documentId = 0)
        {
            var request = RequestServices.GetById(requestID);

            if (CommonSessions.Role == UserRole.Administrator || CommonSessions.Role == UserRole.LanhDaoVP || CommonSessions.Role == UserRole.LanhDaoDonVi)
                return true;

            if (requestID > 0)
            {
                if (CommonSessions.UserID > 0 && (request.CreatedBy == CommonSessions.UserID || request.Trackings.Any(t => t.UserID == CommonSessions.UserID) || request.RequesterID == CommonSessions.UserID))
                    return true;
            }
            else if (documentId > 0)
            {
                var document = DocumentServices.GetById(documentId);

                if ((document.ProvinceDocumentID <= 0 || document.ProvinceDocumentID == null) && CommonSessions.UserID > 0 &&
                    (document.CreatedBy == CommonSessions.UserID || document.SignerID == CommonSessions.UserID ||
                     document.WriterID == CommonSessions.UserID))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Hiển thị danh sách file (trên giao diện)
        /// </summary>
        /// <param name="files">Danh sách file</param>
        /// <returns></returns>
        public static string ShowFiles(this List<File> files)
        {
            string result = string.Empty;

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (!string.IsNullOrEmpty(file.FileName))
                        result += $"<a href=\"{Redirector.GetLink("Public/TaiFile.aspx", "fname", file.FileName)}\">{file.FileName}</a><br/>";
                }
            }

            return result;
        }
    }
}
