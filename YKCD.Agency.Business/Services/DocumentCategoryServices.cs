using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using YKCD.Agency.Business.Entities;

namespace YKCD.Agency.Business.Services
{
    public class DocumentCategoryServices : BaseService<DocumentCategory>
    {
        public static List<DocumentCategory> GetList(string SearchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<DocumentCategory>("WHERE [IsDeleted] = 0")
                    .Where(item => string.IsNullOrEmpty(SearchText) || item.DocumentCategoryName.Contains(SearchText))
                    .OrderBy(item => item.DisplayOrder).ThenBy(item => item.DocumentCategoryName)
                    .ToList();
            }
        }
    }
}
