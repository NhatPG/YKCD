using Framework.Helper;
using YKCD.Agency.Business.Entities;

namespace YKCD.Agency.Business.Services
{
    public class ReportFileServices : BaseService<ReportFile>
    {
        public static void CreateReportFile(Report report, object fileContent, string fileName, string uploadFolder, string connectionName = "DatabaseConnection")
        {
            if (!string.IsNullOrEmpty(fileName) && fileContent != null)
            {
                File file = new File();
                file.FileName = fileContent.SaveFile(fileName, uploadFolder);
                FileServices.Create(file);

                ReportFile reportFile = new ReportFile
                {
                    ReportID = report.ReportID,
                    FileID = file.FileID
                };
                Create(reportFile);
            }
        }
    }
}
