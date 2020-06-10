using System.Collections.Generic;
using System.Linq;
using Framework.Helper;
using PetaPoco;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Services
{
    public class AgencyServices : BaseService<Agency>
    {
        public static Agency GetAgency(string userName = "", string agencyIdentifierCode = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db
                    .Fetch<Agency>("WHERE [IsDeleted] = 0")
                    .Where(agency => string.IsNullOrEmpty(userName) || (!string.IsNullOrEmpty(agency.UserName) && agency.UserName.ToUpper().Equals(userName.ToUpper())))
                    .Where(agency => string.IsNullOrEmpty(agencyIdentifierCode) || (!string.IsNullOrEmpty(agency.AgencyIdentifierCode) && agency.AgencyIdentifierCode.Trim().ToUpper().Equals(agencyIdentifierCode.Trim().ToUpper())))
                    .SingleOrDefault();
            }
        }

        public static List<Agency> GetList(int AgencyGroupID = 0, string SearchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<Agency>("WHERE [IsDeleted] = 0")
                        .Where(agency => AgencyGroupID <= 0 || agency.AgencyGroupID == AgencyGroupID)
                        .Where(agency => string.IsNullOrEmpty(SearchText.Trim()) || agency.AgencyName.ToUpper().ToUnsign().Contains(SearchText.ToUpper().ToUnsign()))
                        .OrderBy(agency => agency.AgencyGroup.DisplayOrder)
                        .ThenBy(agency => agency.AgencyGroupID)
                        .ThenBy(agency => agency.DisplayOrder)
                        .ThenBy(agency => agency.AgencyName)
                        .ToList();
            }
        }
    }
}