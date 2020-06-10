using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using YKCD.Agency.Business.Entities;

namespace YKCD.Agency.Business.Services
{
    public class HolidayServices : BaseService<Holiday>
    {
        public static List<Holiday> GetList(string searchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<Holiday>("WHERE [IsDeleted] = 0")
                    .Where(item => string.IsNullOrEmpty(searchText) || item.HolidayName.Contains(searchText))
                    .OrderBy(item => item.FromDate)
                    .ToList();
            }
        }
    }
}