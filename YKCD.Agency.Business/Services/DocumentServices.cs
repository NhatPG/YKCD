using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Helper;
using PetaPoco;
using YKCD.Agency.Business.Entities;

namespace YKCD.Agency.Business.Services
{
    public class DocumentServices : BaseService<Document>
    {
        public static List<Document> GetList(int categoryId = 0, int userId = 0, DateTime? fromDate = null, DateTime? toDate = null, bool agencyDocument = false, string searchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<Document>("WHERE [IsDeleted] = 0")
                    .Where(document => agencyDocument == false || document.ProvinceDocumentID <= 0)
                    .Where(document => categoryId <= 0 || document.DocumentCategoryID == categoryId)
                    .Where(document => userId <= 0 || document.SignerID == userId || document.WriterID == userId || document.CreatedBy == userId)
                    .Where(document => string.IsNullOrEmpty(searchText)
                            || document.DocumentCode.ToUnsign().ToUpper().Contains(searchText.ToUnsign().ToUpper())
                            || document.MainContent.ToUnsign().ToUpper().Contains(searchText.ToUnsign().ToUpper())
                    )
                    .Where(item => fromDate == null || item.ReleaseDate >= fromDate.Value)
                    .Where(item => toDate == null || item.ReleaseDate <= toDate.Value)
                    .ToList();
            }
        }

        public static Document GetByVbdhCode(string vBDHCode, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<Document>("WHERE [IsDeleted] = 0 AND [VBDHCode] = @0", vBDHCode).FirstOrDefault();
            }
        }

        public static Document GetByProvinceID(long provinceID, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<Document>("WHERE [IsDeleted] = 0 AND [ProvinceDocumentID] = @0", provinceID).FirstOrDefault();
            }
        }
    }
}
