using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Services;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Services
{
    /// <summary>
    /// Summary description for PortalService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PortalService : System.Web.Services.WebService
    {

        [WebMethod]
        public List<SoLieuDonVi> getSoLieuCuaTatCaCacDonViTrucThuocUBNDTinh()
        {
            List<SoLieuDonVi> result = new List<SoLieuDonVi>();

            foreach (var item in RequestServices.ThongKeDonViTheoNhom())
            {
                result.Add(
                    new SoLieuDonVi()
                    {
                        MaDonVi = item.ObjectID.ToString(),
                        TenDonVi = item.ObjectName,
                        TenNhom = item.GroupName,
                        MaNhom = item.GroupID,
                        QuaHan_ChuaThucHien = item.NotPerformOutTerm,                        
                        TrongHan_ChuaThucHien = item.NotPerformInTerm,
                        QuaHan_DangThucHien = item.PerformingOutTerm,
                        TrongHan_DangThucHien = item.PerformingInTerm,
                        QuaHan_DaHoanThanh = item.DoneOutTerm,
                        TrongHan_DaHoanThanh = item.DoneInTerm
                    }
                );
            }

            return result;
        }
    }

    public class AgencyInfo
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Service { get; set; }
    }

    /// <summary>
    /// Định nghĩa loại dữ liệu
    /// </summary>
    [DataContract]
    public enum LOAIDULIEU
    {
        /// <summary>
        /// Chua xac dinh du lieu phan mem nao
        /// </summary>
        CXD = -1,
        /// <summary>
        /// Du lieu phan mem van ban va ho so cong viec
        /// </summary>
        PM_HSCV = 0,
        /// <summary>
        /// Du lieu phan mem mot cua
        /// </summary>
        PM_MOTCUA = 1,
        /// <summary>
        /// Du lieu phan mem Y kien chi dao
        /// </summary>
        PM_YKCD = 2,
        /// <summary>
        /// Du lieu phan mem khieu nai to cao
        /// </summary>
        PM_KNTC = 3,
        /// <summary>
        /// Du lieu phan mem lich va phat hanh giay moi qua mang
        /// </summary>
        PM_LPHGM = 4
    }

    /// <summary>
    /// Cấu trúc số liệu tong hop cơ bản
    /// </summary>
    [DataContract]
    [KnownType(typeof(SoLieu))]
    public class SoLieu
    {
        /// <summary>
        /// gets/sets so lieu chua thuc hien trong han
        /// </summary>
        [DataMember]
        public int TrongHan_ChuaThucHien { get; set; }

        /// <summary>
        /// gets/sets Url so lieu chua thuc hien trong han
        /// </summary>
        [DataMember]
        public string Url_TrongHan_ChuaThucHien { get; set; }

        /// <summary>
        /// gets/sets so lieu dang thuc hien trong han
        /// </summary>
        [DataMember]
        public int TrongHan_DangThucHien { get; set; }

        /// <summary>
        /// gets/sets Url so lieu dang thuc hien trong han
        /// </summary>
        [DataMember]
        public string Url_TrongHan_DangThucHien { get; set; }

        /// <summary>
        /// gets/sets so lieu da hoan thanh trong han
        /// </summary>
        [DataMember]
        public int TrongHan_DaHoanThanh { get; set; }

        /// <summary>
        /// gets/sets Url so lieu da hoan thanh trong han
        /// </summary>
        [DataMember]
        public string Url_TrongHan_DaHoanThanh { get; set; }

        /// <summary>
        /// gets/sets so lieu qua han nhung chua thuc hien
        /// </summary>
        [DataMember]
        public int QuaHan_ChuaThucHien { get; set; }

        /// <summary>
        /// gets/sets Url so lieu qua han nhung chua thuc hien
        /// </summary>
        [DataMember]
        public string Url_QuaHan_ChuaThucHien { get; set; }

        /// <summary>
        /// gets/sets so lieu qua han va dang thuc hien (chua hoan thanh)
        /// </summary>
        [DataMember]
        public int QuaHan_DangThucHien { get; set; }

        /// <summary>
        /// gets/sets Url so lieu qua han va dang thuc hien (chua hoan thanh)
        /// </summary>
        [DataMember]
        public string Url_QuaHan_DangThucHien { get; set; }

        /// <summary>
        /// gets/sets so lieu da hoan thanh nhung hoan thanh qua han so voi yeu cau
        /// </summary>
        [DataMember]
        public int QuaHan_DaHoanThanh { get; set; }

        /// <summary>
        /// gets/sets Url so lieu da hoan thanh nhung hoan thanh qua han so voi yeu cau
        /// </summary>
        [DataMember]
        public string Url_QuaHan_DaHoanThanh { get; set; }
    }

    /// <summary>
    /// Số liệu tổng hợp một tháng về một loại PMDC của một cơ quan
    /// </summary>
    [DataContract]
    [KnownType(typeof(SoLieuThangCoQuan))]
    public class SoLieuThangCoQuan : SoLieu
    {
        LOAIDULIEU _LoaiDuLieu = LOAIDULIEU.CXD;
        [DataMember]
        public LOAIDULIEU LoaiDuLieu
        {
            get { return _LoaiDuLieu; }
            set { _LoaiDuLieu = value; }
        }

        int _Thang = 1;
        [DataMember]
        public int Thang
        {
            get { return _Thang; }
            set { _Thang = value; }
        }

        int _Nam = 1900;
        [DataMember]
        public int Nam
        {
            get { return _Nam; }
            set { _Nam = value; }
        }

        [DataMember]
        public string MaCoQuan { get; set; }

        [DataMember]
        public string TenCoQuan { get; set; }

        [DataMember]
        public string UrlCoQuan { get; set; }
    }

    /// <summary>
    /// Số liệu tổng hợp hiện trạng xử lý một đơn vị trong cơ quan từ đầu năm đến hiện tại
    /// </summary>
    [DataContract]
    [KnownType(typeof(SoLieuDonVi))]
    public class SoLieuDonVi : SoLieu
    {
        [DataMember]
        public string MaDonVi { get; set; }

        [DataMember]
        public string TenDonVi { get; set; }

        [DataMember]
        public string UrlDonVi { get; set; }

        [DataMember]
        public DateTime NgaySoLieu { get; set; }

        [DataMember]
        public int MaNhom { get; set; }

        [DataMember]
        public string TenNhom { get; set; }
    }

    /// <summary>
    /// Số liệu tổng hợp hiện trạng xử lý của một người từ đầu năm đến hiện tại
    /// </summary>
    [DataContract]
    [KnownType(typeof(SoLieuCaNhan))]
    public class SoLieuCaNhan : SoLieu
    {
        [DataMember]
        public string UseName { get; set; }

        [DataMember]
        public string MaCaNhan { get; set; }

        [DataMember]
        public string HoVaTen { get; set; }

        [DataMember]
        public string UrlCaNhan { get; set; }

        [DataMember]
        public DateTime NgaySoLieu { get; set; }
    }
}
