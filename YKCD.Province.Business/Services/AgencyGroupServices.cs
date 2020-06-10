using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Services
{
    public class AgencyGroupServices : BaseService<AgencyGroup>
    {
        public static List<AgencyGroup> GetList(bool? isShowStatistic = null, string searchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<AgencyGroup>("WHERE [IsDeleted] = 0")
                    .Where(item => isShowStatistic == null || item.IsShowStatistic == true)
                    .Where(item => string.IsNullOrEmpty(searchText) || item.AgencyGroupName.Contains(searchText))
                    .OrderBy(item => item.DisplayOrder).ThenBy(item => item.AgencyGroupName)
                    .ToList();
            }
        }
    }
}