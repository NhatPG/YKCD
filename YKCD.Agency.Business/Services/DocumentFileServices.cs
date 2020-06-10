using Framework.Helper;
using YKCD.Agency.Business.Entities;

namespace YKCD.Agency.Business.Services
{
    public class DocumentFileServices : BaseService<DocumentFile>
    {
        public static void CreateDocumentFile(Document document, object fileContent, string fileName, string uploadFolder, string connectionName = "DatabaseConnection")
        {
            if (fileContent != null)
            {
                File file = new File
                {
                    FileName = fileContent.SaveFile(fileName, uploadFolder)
                };

                if (!string.IsNullOrEmpty(file.FileName))
                {
                    FileServices.Create(file);

                    DocumentFile docFile = new DocumentFile
                    {
                        DocumentID = document.DocumentID,
                        FileID = file.FileID
                    };
                    Create(docFile);
                }
            }
        }
    }
}
