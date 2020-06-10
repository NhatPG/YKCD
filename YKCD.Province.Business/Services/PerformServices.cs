using System;
using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Services
{
    public class PerformServices : BaseService<Perform>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="AgencyGroupID"></param>
        /// <param name="MaNguoiTheoDoi"></param>
        /// <param name="MaNguoiGiaoViec"></param>
        /// <param name="FromDate"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static List<Perform> GetList(long RequestID = 0, int AgencyGroupID = 0, int AgencyID = 0, int MaNguoiTheoDoi = 0, int MaNguoiGiaoViec = 0, DateTime? FromDate = null, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                List<Perform> result = null;

                if (RequestID > 0)
                {
                    result = db.Fetch<Perform>("WHERE [RequestID] = @0", RequestID);
                }

                if (AgencyGroupID > 0)
                {
                    result = db.Fetch<Perform>(
                        @"SELECT [Performs].* FROM [Performs] 
                            INNER JOIN [Requests] ON [Performs].[RequestID] = [Requests].[RequestID]
                            INNER JOIN [Agencies] ON [Agencies].[AgencyID] = [Performs].[AgencyID]
                          WHERE Agencies.[AgencyGroupID] = @0 AND [Requests].[IsDeleted] = 0", AgencyGroupID)
                        .Where(perform => RequestID <= 0 || perform.RequestID == RequestID).ToList();
                }

                if (AgencyID > 0)
                {
                    result = db.Fetch<Perform>(
                        @"SELECT [Performs].* FROM [Performs]
                            INNER JOIN [Requests] ON [Performs].[RequestID] = [Requests].[RequestID]
                          WHERE Performs.[AgencyID] = @0 AND [Requests].[IsDeleted] = 0", AgencyID)
                          .Where(perform => RequestID <= 0 || perform.RequestID == RequestID).ToList();
                }

                if (MaNguoiTheoDoi > 0)
                {
                    result = db.Fetch<Perform>(
                        @"SELECT [Performs].* FROM [Performs]
                            INNER JOIN [Requests] ON [Performs].[RequestID] = [Requests].[RequestID]
                            INNER JOIN [Trackings] ON [Trackings].[RequestID] = [Performs].[RequestID]
                          WHERE Trackings.[UserID] = @0 AND [Requests].[IsDeleted] = 0", MaNguoiTheoDoi)
                        .Where(perform => RequestID <= 0 || perform.RequestID == RequestID).ToList(); ;
                }

                if(MaNguoiGiaoViec > 0)
                {
                    result = db.Fetch<Perform>(
                        @"SELECT [Performs].* FROM [Performs] 
                            INNER JOIN [Requests] ON [Performs].[RequestID] = [Requests].[RequestID]
                            INNER JOIN [Documents] ON [Documents].[DocumentID] = [Requests].[DocumentID]
                          WHERE 
                            (Documents.[SignerID] = @0 OR [Requests].[RequesterID] = @0)
                            AND [Requests].[IsDeleted] = 0", MaNguoiGiaoViec)
                        .Where(perform => RequestID <= 0 || perform.RequestID == RequestID).ToList();
                }

                if (FromDate != null)
                {
                    result = result?.Where(item => item.RequiredDate.Date >= FromDate.Value.Date).ToList();
                }

                return result;
            }
        }

        public static void Update(long performId, int status, DateTime performOnDate, bool isNeedConfirm)
        {
            var perform = PerformServices.GetById(performId);

            perform.Status = status;

            if (status == 2 && isNeedConfirm)
            {
                perform.Status = 3;
                perform.FinishedOnDate = performOnDate;
            }
            else if (status == 2 && !isNeedConfirm)
            {
                perform.Status = 2;
                perform.FinishedOnDate = performOnDate;
            }
            perform.IsSynced = false;

            PerformServices.Update(perform);

            RequestServices.CapNhatTrangThaiYKCD(perform.RequestID);
        }
    }
}