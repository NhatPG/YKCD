using System;
using System.Collections.Generic;
using System.Web;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Business.Components
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
                    HttpRuntime.Cache.Insert("SoLieuThongKeTatCaNhomDonVi", items, null, DateTime.Now.AddSeconds(3600), TimeSpan.Zero);
                    return items;
                }

                return HttpRuntime.Cache["SoLieuThongKeTatCaNhomDonVi"] as List<SoLieuThongKe>;
            }
            set
            {
                if (value != null)
                    HttpRuntime.Cache.Insert("SoLieuThongKeTatCaNhomDonVi", value, null, DateTime.Now.AddSeconds(3600), TimeSpan.Zero);
                else
                    HttpRuntime.Cache.Remove("SoLieuThongKeTatCaNhomDonVi");
            }
        }
    }
}