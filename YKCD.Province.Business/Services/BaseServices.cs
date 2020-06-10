using System;
using System.Web;
using Framework.Helper;
using PetaPoco;

namespace YKCD.Province.Business.Services
{
    public class BaseService<T> where T : class, new()
    {
        public static T GetById(object id, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.SingleOrDefault<T>(id);
            }
        }

        public static void Create(T item, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                typeof(T).GetProperty("IsDeleted")?.SetValue(item, false, null);
                if (HttpContext.Current?.Session != null)
                    typeof(T).GetProperty("CreatedBy")?.SetValue(item, HttpContext.Current.Session["UserID"].ToInteger(), null);
                typeof(T).GetProperty("CreatedTime")?.SetValue(item, DateTime.Now, null);

                db.Insert(item);
            }
        }

        public static void Update(T item, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                db.Update(item);
            }
        }

        public static void Delete(object id, string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                var item = db.SingleOrDefault<T>(id);

                if (typeof(T).GetProperty("IsDeleted") != null)
                {
                    typeof(T).GetProperty("IsDeleted").SetValue(item, true, null);
                    Update(item);
                }
                else
                {
                    db.Delete(item);
                }
            }
        }
    }
}