using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using YKCD.Agency.Business.Entities;

namespace YKCD.Agency.Business.Services
{
    public class FileServices : BaseService<File>
    {
        public static List<File> GetList(long documentId = 0, long reportId = 0, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                if (documentId > 0)
                {
                    return db.Fetch<File>(@"SELECT [Files].* 
                                            FROM [Files] 
                                            INNER JOIN [DocumentFiles] 
                                                ON [Files].[FileID] = [DocumentFiles].[FileID]
                                            WHERE [DocumentFiles].[DocumentID] = @0", documentId).Where(file => !string.IsNullOrEmpty(file.FileName)).ToList();
                }

                if (reportId > 0)
                {
                    return db.Fetch<File>(@"SELECT [Files].* 
                                            FROM [Files] 
                                            INNER JOIN [ReportFiles] 
                                                ON [Files].[FileID] = [ReportFiles].[FileID]
                                            WHERE [ReportFiles].[ReportID] = @0", reportId).Where(file => !string.IsNullOrEmpty(file.FileName)).ToList();
                }
                return null;
            }
        }
    }
}
