using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Helper;
using PetaPoco;
using YKCD.SubAgency.Business.Entities;

namespace YKCD.SubAgency.Business.Services
{
    public class PerformServices : BaseService<Perform>
    {
        public static void CreatePerform(Perform item)
        {
            Create(item);

            string notify = item.Request.DocumentID > 0 ?
                        $"Đ/c {UserServices.GetById(item.Request.RequesterID)?.FullName} có văn bản số {item.Request?.Document?.DocumentCode} ban hành ngày {item.Request?.Document.ReleaseDate.ToDateString()}, yêu cầu báo cáo kết quả trước ngày {item?.RequiredDate.ToDateTime()}."
                        : $"Đ/c {UserServices.GetById(item.Request.RequesterID)?.FullName} có ý kiến chỉ đạo mới, yêu cầu báo cáo kết quả trước ngày {item.RequiredDate.ToDateTime()}.";

            NotificationServices.Create(new Notification(item.RequestID, item.UserID, item.DepartmentID, notify));
        }

        public static void Update(long performId, int status, DateTime performOnDate, bool isNeedConfirm)
        {
            var perform = PerformServices.GetById(performId);

            if (perform != null)
            {
                perform.Status = status;

                if (status == 2 && perform.IsFinishedConfirm == true && isNeedConfirm)
                {
                    perform.Status = 3;
                    perform.FinishedOnDate = performOnDate;
                }
                else if (status == 2 && (!isNeedConfirm || !perform.IsFinishedConfirm))
                {
                    perform.Status = 2;
                    perform.FinishedOnDate = performOnDate;
                }

                PerformServices.Update(perform);
            }

            RequestServices.CapNhatTrangThaiYKCD(perform.RequestID);
        }

        public static List<Perform> GetList(long requestId = 0, int departmentGroupId = 0, int departmentId = 0, int userId = 0, int maNguoiTheoDoi = 0, int maNguoiGiaoViec = 0, DateTime? fromDate = null, DateTime? toDate = null, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                List<Perform> result = null;

                if (requestId > 0)
                {
                    result = db.Fetch<Perform>("WHERE [RequestID] = @0", requestId);
                }

                if (departmentGroupId > 0)
                {
                    result = db.Fetch<Perform>(
                        @"SELECT [Performs].* FROM [Performs] 
                            INNER JOIN [Requests] ON [Performs].[RequestID] = [Requests].[RequestID]
                            INNER JOIN [Departments] ON [Departments].[DepartmentID] = [Performs].[DepartmentID]
                          WHERE Departments.[DepartmentGroupID] = @0 AND [Requests].[IsDeleted] = 0", departmentGroupId)
                          .Where(perform => requestId <= 0 || perform.RequestID == requestId).ToList();
                }

                if (departmentId > 0)
                {
                    result = db.Fetch<Perform>(
                        @"SELECT [Performs].* FROM [Performs]
                            INNER JOIN [Requests] ON [Performs].[RequestID] = [Requests].[RequestID]
                          WHERE Performs.[DepartmentID] = @0 AND [Requests].[IsDeleted] = 0", departmentId)
                        .Where(perform => requestId <= 0 || perform.RequestID == requestId).ToList();
                }

                if (userId > 0)
                {
                    result = db.Fetch<Perform>(
                        @"SELECT [Performs].* FROM [Performs]
                            INNER JOIN [Requests] ON [Performs].[RequestID] = [Requests].[RequestID]
                          WHERE Performs.[UserID] = @0 AND [Requests].[IsDeleted] = 0", userId)
                        .Where(perform => requestId <= 0 || perform.RequestID == requestId).ToList();
                }

                if (maNguoiTheoDoi > 0)
                {
                    result = db.Fetch<Perform>(
                        @"SELECT [Performs].* FROM [Performs]
                            INNER JOIN [Requests] ON [Performs].[RequestID] = [Requests].[RequestID]
                            INNER JOIN [Trackings] ON [Trackings].[RequestID] = [Performs].[RequestID]
                          WHERE Trackings.[UserID] = @0 AND [Requests].[IsDeleted] = 0", maNguoiTheoDoi)
                        .Where(perform => requestId <= 0 || perform.RequestID == requestId).ToList();
                }

                if (maNguoiGiaoViec > 0)
                {
                    result = db.Fetch<Perform>(
                        @"SELECT [Performs].* FROM [Performs] 
                            INNER JOIN [Requests] ON [Performs].[RequestID] = [Requests].[RequestID]
                            INNER JOIN [Documents] ON [Documents].[DocumentID] = [Requests].[DocumentID]
                          WHERE 
                            (Documents.[SignerID] = @0 OR [Requests].[RequesterID] = @0)
                            AND [Requests].[IsDeleted] = 0", maNguoiGiaoViec)
                        .Where(perform => requestId <= 0 || perform.RequestID == requestId).ToList();
                }

                if (fromDate != null)
                {
                    result = result?.Where(item => item.RequiredDate.Date >= fromDate.Value.Date).ToList();
                }

                if (toDate != null)
                {
                    result = result?.Where(item => item.RequiredDate.Date <= toDate.Value.Date).ToList();
                }

                return result;
            }
        }
    }
}
