using System.Collections.Generic;
using System.Linq;
using Framework.Helper;
using Framework.Util;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Services
{
    public static class CommonServices
    {
        public static string RequestStatusToLabel(this object status)
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

        public static string ShowPerformObjects(long requestID)
        {
            string result = string.Empty;
            var items = PerformServices.GetList(requestID);

            if (CommonSessions.UserID > 0 &&
                (CommonSessions.UserID == requestID
                 || TrackingServices.GetList(requestID).Select(item => item.UserID).Contains(CommonSessions.UserID) || CommonSessions.Role == UserRole.Administrator))
            {
                foreach (var item in items)
                {
                    result += $"{item.Agency.AgencyName} (<a href=\"#\" data-toggle=\"modal\" data-target=\".my-modal-lg\" data-link=\"{Redirector.GetLink("Province/XacNhanHoanThanh.aspx", "id", item.PerformID)}\">{item.StatusString}</a>)<br/>";
                }
            }
            else
            {
                foreach (var item in items)
                {
                    result += $"{item.Agency.AgencyName} ({item.StatusString})<br/>";
                }
            }


            return result;
        }

        public static bool CheckReportPermission(long requestID)
        {
            var request = RequestServices.GetById(requestID);
            if (CommonSessions.UserID > 0 && (request.CreatedBy == CommonSessions.UserID || request.Trackings.Any(t => t.UserID == CommonSessions.UserID) || CommonSessions.Role == UserRole.Administrator))
                return true;

            return CommonSessions.AgencyID > 0 && PerformServices.GetList(requestID).Any(item => item.AgencyID == CommonSessions.AgencyID);
        }

        public static bool CheckUpdatePermission(long requestID)
        {
            var request = RequestServices.GetById(requestID);
            return CommonSessions.UserID > 0 &&
                    (request.CreatedBy == CommonSessions.UserID ||
                     request.Trackings.Any(t => t.UserID == CommonSessions.UserID) || CommonSessions.Role == UserRole.Administrator);
        }

        public static string ShowFiles(this List<File> files)
        {
            var result = "";

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    result += $"<a href=\"{Redirector.GetLink("Public/TaiFile.aspx", "fname", file.FileName)}\">{file.FileName}</a><br/>";
                }
            }
            return result;
        }
    }
}