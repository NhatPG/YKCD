using System.Collections.Generic;
using PetaPoco;
using YKCD.SubAgency.Business.Entities;

namespace YKCD.SubAgency.Business.Services
{
    public class TrackingServices : BaseService<Tracking>
    {
        public static List<Tracking> GetList(long RequestID = 0, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<Tracking>("WHERE [RequestID] = @0", RequestID);
            }
        }
    }
}
