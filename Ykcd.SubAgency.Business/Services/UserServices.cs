using System.Collections.Generic;
using System.Linq;
using Framework.Helper;
using PetaPoco;
using YKCD.SubAgency.Business.Entities;

namespace YKCD.SubAgency.Business.Services
{
    public class UserServices : BaseService<User>
    {
        public static List<User> GetList(int[] roles = null, bool isActive = true, int departmentId = 0, string searchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                searchText = searchText.ToUnsign().ToUpper();

                return db.Fetch<User>("WHERE [IsDeleted] = 0")
                        ?.Where(user => departmentId <= 0 || user.DepartmentID == departmentId)
                        ?.Where(user => string.IsNullOrEmpty(searchText) || user.FullName.ToUnsign().ToUpper().Contains(searchText))
                        ?.Where(user => roles == null || roles.Length == 0 || roles.Any(role => role == user.Role))
                        ?.Where(user => user.IsActive == isActive)
                        ?.OrderBy(item => item.Department?.DisplayOrder)
                        ?.ThenBy(user => user.DepartmentName)
                        ?.ThenBy(user => user.FullName)
                        ?.ToList();
            }
        }

        public static User GetUser(string userName = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<User>("WHERE [IsDeleted] = 0")
                    ?.SingleOrDefault(user => string.IsNullOrEmpty(userName.Trim()) || user.UserName.ToUpper().Equals(userName.ToUpper()));
            }
        }
    }
}
