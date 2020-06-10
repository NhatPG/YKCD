using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Services
{
    public class DepartmentServices : BaseService<Department>
    {
        public static List<Department> GetList(bool? isShowStatistic = null, string searchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<Department>("WHERE [IsDeleted] = 0")
                    .Where(item => isShowStatistic == null || item.IsShowStatistic == true)
                    .Where(item => string.IsNullOrEmpty(searchText) || item.DepartmentName.Contains(searchText))
                    .OrderBy(item => item.DisplayOrder).ThenBy(item => item.DepartmentName)
                    .ToList();
            }
        }
    }
}