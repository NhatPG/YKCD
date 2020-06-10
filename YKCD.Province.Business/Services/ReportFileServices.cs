using Framework.Helper;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Services
{
    public class ReportFileServices : BaseService<ReportFile>
    {
        /// <summary>
        /// Lưu file báo cáo
        /// </summary>
        /// <param name="report"></param>
        /// <param name="fileContent"></param>
        /// <param name="fileName"></param>
        /// <param name="uploadFolder"></param>
        /// <param name="connectionName"></param>
        public static void CreateReportFile(Report report, object fileContent, string fileName, string uploadFolder, string connectionName = "DatabaseConnection")
        {
            if (fileContent != null && !string.IsNullOrEmpty(fileName.Trim()) && !string.IsNullOrEmpty(uploadFolder))
            {
                File file = new File
                {
                    FileName = fileContent.SaveFile(fileName, uploadFolder)
                };

                if (!string.IsNullOrEmpty(file.FileName))
                {
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
}