using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Services
{
    public class ReportServices : BaseService<Report>
    {
        public static List<Report> GetList(long requestId = 0, long agencyId = 0,
            string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<Report>("WHERE [RequestID] = @0 AND [IsDeleted] = 0", requestId)
                    .Where(report => agencyId <= 0 || report.AgencyID == 0 || report.AgencyID == agencyId)
                    .OrderBy(report => report.CreatedByName).ThenBy(report => report.CreatedTime)
                    .ToList();
            }
        }

        public static void Create(Report report, List<long> performIds, object fileContent, string fileName, string uploadFolder, bool isStaffReport)
        {
            if (!string.IsNullOrEmpty(report.ReportContent))
            {
                Create(report);

                ReportFileServices.CreateReportFile(report, fileContent: fileContent, fileName: fileName, uploadFolder: uploadFolder);
            }

            if (isStaffReport)
            {
                if (performIds != null && performIds.Count > 0)
                {
                    foreach (var performId in performIds)
                    {
                        PerformServices.Update(performId: performId, status: report.Status, performOnDate: report.PerformOnDate, isNeedConfirm: false);
                    }
                }
                //Trường hợp giao việc ko chọn đơn vị, cập nhật trạng thái YKCD theo trạng thái báo cáo
                else
                {
                    var request = report.Request;

                    if (request.Performs == null || request.Performs.Count == 0)
                    {
                        request.Status = report.Status;

                        if (request.Status == 2)
                        {
                            request.FinishedOnDate = report.PerformOnDate;
                        }
                        RequestServices.Update(request);
                    }
                }
            }
            else
            {
                if (performIds != null && performIds.Count > 0)
                {
                    PerformServices.Update(performId: performIds.FirstOrDefault(), status: report.Status, performOnDate: report.PerformOnDate, isNeedConfirm: true);
                }
            }

            RequestServices.CapNhatTrangThaiYKCD(report.RequestID);
        }
    }
}