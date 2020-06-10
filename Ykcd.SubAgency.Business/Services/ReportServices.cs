using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using PetaPoco;
using YKCD.SubAgency.Business.Components;
using YKCD.SubAgency.Business.Entities;

namespace YKCD.SubAgency.Business.Services
{
    public class ReportServices : BaseService<Report>
    {
        public static List<Report> GetList(long requestId = 0, long departmentId = 0,
            string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<Report>("WHERE [RequestID] = @0 AND [IsDeleted] = 0", requestId)
                    .Where(report => departmentId <= 0 || report.DepartmentID == departmentId)
                    .ToList();
            }
        }

        public static void Create(Report report, List<int> performIds, object fileContent, string fileName, string uploadFolder, bool isStaffReport = false, bool isSendToAgency = false, bool isSendFromAgency = false)
        {
            if (!string.IsNullOrEmpty(report.ReportContent))
            {
                Create(report);

                ReportFileServices.CreateReportFile(report, fileContent, fileName, uploadFolder);
            }

            Request request = RequestServices.GetById(report.RequestID);

            if (request.IsAgencyRequest)
            {
                if (request.Performs.Count > 0)
                {
                    if (isSendFromAgency)
                    {
                        if (request.Status == 2)
                        {
                            foreach (var perform in request.Performs)
                            {
                                PerformServices.Update(performId: perform.PerformID, status: report.Status, performOnDate: report.PerformOnDate, isNeedConfirm: false);
                            }
                        }
                    }
                    else if (isStaffReport)
                    {
                        if (performIds != null)
                        {
                            foreach (var performId in performIds)
                            {
                                PerformServices.Update(performId: performId, status: report.Status, performOnDate: report.PerformOnDate, isNeedConfirm: false);
                            }

                            report.Status = RequestServices.CapNhatTrangThaiYKCD(request.RequestID);
                        }
                    }
                    else
                    {
                        PerformServices.Update(performId: performIds.FirstOrDefault(), status: report.Status, performOnDate: report.PerformOnDate, isNeedConfirm: true);
                        report.Status = RequestServices.CapNhatTrangThaiYKCD(request.RequestID);
                    }

                    if (isSendToAgency)
                    {
                        if (report.Status == 3)
                            report.Status = 2;
                        AgencyServiceHelper.ReceiveReport(ConfigurationManager.AppSettings["Agency_Service"], report);
                    }
                        
                }
                else
                {
                    if (!isSendFromAgency)
                    {
                        request.Status = report.Status;

                        if (request.Status == 2)
                        {
                            request.FinishedOnDate = report.PerformOnDate;
                        }

                        RequestServices.Update(request);

                        if (isSendToAgency)
                        {
                            if (report.Status == 3)
                                report.Status = 2;

                            AgencyServiceHelper.ReceiveReport(ConfigurationManager.AppSettings["Agency_Service"], report);
                        }
                            
                    }
                }
            }
            else if (isStaffReport)
            {
                foreach (var performId in performIds)
                {
                    PerformServices.Update(performId: performId, status: report.Status, performOnDate: report.PerformOnDate, isNeedConfirm: false);
                }
            }
            else
            {
                PerformServices.Update(performId: performIds.FirstOrDefault(), status: report.Status, performOnDate: report.PerformOnDate, isNeedConfirm: true);
            }
        }
    }
}
