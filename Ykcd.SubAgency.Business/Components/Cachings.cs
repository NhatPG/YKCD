using System;
using System.Collections.Generic;
using System.Web;
using Framework.Helper;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Business.Components
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

        public static bool IsSyncedFromAgency
        {
            get
            {
                if (HttpRuntime.Cache["IsSyncedFromAgency"] == null)
                {
                    HttpRuntime.Cache.Insert("IsSyncedFromAgency", false, null, DateTime.Now.AddSeconds(900), TimeSpan.Zero);
                    return false;
                }

                return HttpRuntime.Cache["IsSyncedFromAgency"].ToBoolean();
            }
            set
            {
                HttpRuntime.Cache.Insert("IsSyncedFromAgency", value, null, DateTime.Now.AddSeconds(900), TimeSpan.Zero);
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
