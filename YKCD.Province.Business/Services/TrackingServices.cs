using System.Collections.Generic;
using PetaPoco;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Services
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