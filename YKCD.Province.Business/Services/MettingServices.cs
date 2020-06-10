using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.Helper;
using PetaPoco;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Services
{
    public class MettingServices : BaseService<Metting>
    {
        public static List<Metting> GetList(string searchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<Metting>("WHERE [IsDeleted] = 0")
                    .ToList();
            }
        }

        public static List<LichHop> GetListFromLichTuan(DateTime fromDate, string searchText = "", string connectionName = "LichTuanConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<LichHop>(@"SELECT tblLichtuan.*, DangKy.[user_name] AS NguoiDangKy, ChuTri.[user_name] AS NguoiChuTri FROM dbo.tblLichtuan
                                            LEFT join dbo.tblLDThamdu on tblLichtuan.iLichtuanId = tblLDThamdu.iLichtuanId 
                                            LEFT JOIN [dbo].[office_user] AS DangKy ON [iNguoiDangkyId] = DangKy.[user_id]
                                            LEFT JOIN [dbo].[office_user] AS ChuTri ON [iNguoiChutriId] = ChuTri.[user_id]
                                            WHERE iChutri = 1 AND (iDuyetLD = 5 or iDuyetLD > 8)").Where(item => item.Time.Date >= fromDate.Date)
                    .ToList();
            }
        }

        public static void SyncLichHop(DateTime fromDate)
        {
            var items = GetListFromLichTuan(fromDate);

            foreach (var item in items)
            {
                if (MettingServices.GetById(item.MettingID) == null)
                {
                    var metting = new Metting();
                    metting.MettingID = item.MettingID;
                    metting.Status = 0;
                    metting.PresidentID = UserServices.GetUser(item.PresidentUserName)?.UserID ?? 0;
                    metting.StaffID = UserServices.GetUser(item.StaffUserName)?.UserID ?? 0;
                    metting.Time = item.Time;
                    metting.MettingContent = item.MettingContent;
                    metting.CreatedTime = DateTime.Now;
                    metting.UpdatedTime = DateTime.Now;
                    MettingServices.Create(metting);
                }
            }
        }
    }
}