using System.Collections.Generic;
using System.Linq;
using Framework.Helper;
using PetaPoco;
using YKCD.Agency.Business.Entities;

namespace YKCD.Agency.Business.Services
{
    public class DepartmentServices : BaseService<Department>
    {
        public static Department GetDepartment(string userName, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db
                    .Fetch<Department>("WHERE [IsDeleted] = 0")
                    .SingleOrDefault(department => !string.IsNullOrEmpty(department.UserName) && department.UserName.ToUpper().Equals(userName.ToUpper()));
            }
        }

        public static List<Department> GetList(int DepartmentGroupID = 0, string searchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                searchText = searchText.ToUnsign().ToUpper();

                return db.Fetch<Department>("WHERE [IsDeleted] = 0")
                        .Where(department => DepartmentGroupID <= 0 || department.DepartmentGroupID == DepartmentGroupID)
                        .Where(department => string.IsNullOrEmpty(searchText) || department.DepartmentName.ToUnsign().ToUpper().Contains(searchText))
                        .OrderBy(department => department.DepartmentGroup.DisplayOrder)
                        .ThenBy(department => department.DepartmentGroupID)
                        .ThenBy(department => department.DisplayOrder)
                        .ThenBy(department => department.DepartmentName)
                        .ToList();
            }
        }
    }
}
