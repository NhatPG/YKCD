using System.Collections.Generic;
using System.Linq;
using Framework.Helper;
using PetaPoco;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Services
{
    public class UserServices : BaseService<User>
    {
        public static List<User> GetList(int[] roles = null, bool isActive = true, string searchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<User>("WHERE [IsDeleted] = 0")
                        .Where(item => item.IsActive == isActive)
                        .Where(user => roles == null || roles.Any(role => role == user.Role))
                        .Where(user => string.IsNullOrEmpty(searchText) || user.FullName.ToUpper().ToUnsign().Contains(searchText.ToUpper().ToUnsign()) || user.UserName.ToUpper().ToUnsign().Contains(searchText.ToUpper().ToUnsign()))
                        .OrderBy(item => item.Department.DisplayOrder)
                        .ThenBy(user => user.DisplayOrder)
                        .ToList();
            }
        }

        public static User GetUser(string userName = "", string connectionName = "DatabaseConnection")
        {
            if (userName == null)
            {
                return null;
            }

            using (var db = new Database(connectionName))
            {
                return db
                    .Fetch<User>("WHERE [IsDeleted] = 0")
                    ?.SingleOrDefault(user => string.IsNullOrEmpty(userName.Trim()) || user.UserName.ToUpper().Equals(userName.ToUpper()));
            }
        }
    }
}