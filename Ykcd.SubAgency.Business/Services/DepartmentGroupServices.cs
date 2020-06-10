using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using YKCD.SubAgency.Business.Entities;

namespace YKCD.SubAgency.Business.Services
{
    public class DepartmentGroupServices : BaseService<DepartmentGroup>
    {
        public static List<DepartmentGroup> GetList(bool? isShowStatistic = null, string searchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<DepartmentGroup>("WHERE [IsDeleted] = 0")
                    .Where(item => isShowStatistic == null || item.IsShowStatistic == true)
                    .Where(item => string.IsNullOrEmpty(searchText) || item.DepartmentGroupName.Contains(searchText))
                    .OrderBy(item => item.DisplayOrder).ThenBy(item => item.DepartmentGroupName)
                    .ToList();
            }
        }
    }
}
