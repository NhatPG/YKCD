using System;
using System.Collections.Generic;
using System.Web;
using Framework.Helper;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Business.Components
{
    public static class Cachings
    {
        public static List<SoLieuThongKe> SoLieuThongKeTatCaNhomDonVi
        {
            get
            {
                if (HttpRuntime.Cache["SoLieuThongKeTatCaNhomDonVi"] == null)
                {
                    var items = RequestServices.ThongKeTheoTatCaNhomDonVi();
                    HttpRuntime.Cache.Insert("SoLieuThongKeTatCaNhomDonVi", items, null, DateTime.Now.AddSeconds(900), TimeSpan.Zero);
                    return items;
                }

                return HttpRuntime.Cache["SoLieuThongKeTatCaNhomDonVi"] as List<SoLieuThongKe>;
            }
            set
            {
                if (value != null)
                    HttpRuntime.Cache.Insert("SoLieuThongKeTatCaNhomDonVi", value, null, DateTime.Now.AddSeconds(900), TimeSpan.Zero);
                else
                    HttpRuntime.Cache.Remove("SoLieuThongKeTatCaNhomDonVi");
            }
        }

        public static bool IsSyncedFromProvince
        {
            get
            {
                if (HttpRuntime.Cache["IsSyncedFromProvince"] == null)
                {
                    HttpRuntime.Cache.Insert("IsSyncedFromProvince", false, null, DateTime.Now.AddSeconds(900), TimeSpan.Zero);
                    return false;
                }

                return HttpRuntime.Cache["IsSyncedFromProvince"].ToBoolean();
            }
            set
            {
                HttpRuntime.Cache.Insert("IsSyncedFromProvince", value, null, DateTime.Now.AddSeconds(900), TimeSpan.Zero);
            }
        }

        public static bool IsSyncing
        {
            get
            {
                if (HttpRuntime.Cache["IsSyncing"] == null)
                {
                    HttpRuntime.Cache.Insert("IsSyncing", false, null, DateTime.Now.AddSeconds(900), TimeSpan.Zero);
                    return false;
                }

                return HttpRuntime.Cache["IsSyncing"].ToBoolean();
            }
            set
            {
                HttpRuntime.Cache.Insert("IsSyncing", value, null, DateTime.Now.AddSeconds(900), TimeSpan.Zero);
            }
        }
    }
}
