using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Services;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.WebServices
{
    /// <summary>
    /// Summary description for PortalServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PortalServices : System.Web.Services.WebService
    {

        [WebMethod]
        public List<SoLieuDonVi> getSoLieuCuaTatCaCacDonViTrucThuocUBNDTinh()
        {
            List<SoLieuDonVi> listSoLieu = new List<SoLieuDonVi>();
            var result = RequestServices.ThongKeDonViTheoNhom();

            foreach (var item in result)
            {
                string domain = string.Empty;

                if (string.IsNullOrEmpty(AgencyServices.GetById(item.ObjectID)?.AgencyDomain))
                    domain = $"http://ykcd.thuathienhue.egov.vn/Public/DanhSachYKCD.aspx?agid={item.ObjectID}";
                else
                    domain = $"{AgencyServices.GetById(item.ObjectID).AgencyDomain}/Public/DanhSachYKCD.aspx";

                listSoLieu.Add(new SoLieuDonVi()
                {
                    MaDonVi = item.ObjectID.ToString(),
                    TenDonVi = item.ObjectName,
                    MaNhom = item.GroupID,
                    TenNhom = item.GroupName,
                    TrongHan_DangThucHien = item.PerformingInTerm,
                    Url_TrongHan_DangThucHien = $"{domain}?status=dangthuchientronghan&ubtinh=True",
                    QuaHan_DangThucHien = item.PerformingOutTerm,
                    Url_QuaHan_DangThucHien = $"{domain}?status=dangthuchienquahan&ubtinh=True",
                    TrongHan_DaHoanThanh = item.DoneInTerm,
                    Url_TrongHan_DaHoanThanh = $"{domain}?status=dathuchiendunghan&ubtinh=True",
                    QuaHan_DaHoanThanh = item.DoneOutTerm,
                    Url_QuaHan_DaHoanThanh = $"{domain}?status=dathuchientrehan&ubtinh=True",
                    TrongHan_ChuaThucHien = item.NotPerformInTerm,
                    Url_TrongHan_ChuaThucHien = $"{domain}?status=chuathuchientronghan&ubtinh=True",
                    QuaHan_ChuaThucHien = item.NotPerformOutTerm,
                    Url_QuaHan_ChuaThucHien = $"{domain}?status=chuathuchienquahan&ubtinh=True",
                });
            }

            return listSoLieu;
        }

        [WebMethod]
        public List<SoLieuDonVi> getSoLieuCuaCacDonViTrucThuocUBNDTinh(params string[] listMaDonVi)
        {
            List<SoLieuDonVi> listSoLieu = new List<SoLieuDonVi>();

            foreach (var maDonVi in listMaDonVi)
            {
                var agency = AgencyServices.GetAgency(agencyIdentifierCode: maDonVi);
                string domain = string.Empty;

                if (agency != null)
                {
                    if (string.IsNullOrEmpty(agency.AgencyDomain))
                        domain = $"http://ykcd.thuathienhue.egov.vn/Public/DanhSachYKCD.aspx?agid={agency.AgencyID}";
                    else
                        domain = $"{agency.AgencyDomain}/Public/DanhSachYKCD.aspx";

                    var sltk = RequestServices.LaySoLieuThongKe(MaDonVi: agency.AgencyID);

                    listSoLieu.Add(new SoLieuDonVi()
                    {
                        MaDonVi = agency.AgencyID.ToString(),
                        TenDonVi = agency.AgencyName,
                        TrongHan_DangThucHien = sltk.PerformingInTerm,
                        Url_TrongHan_DangThucHien = $"{domain}?status=dangthuchientronghan&ubtinh=True",
                        QuaHan_DangThucHien = sltk.PerformingOutTerm,
                        Url_QuaHan_DangThucHien = $"{domain}?status=dangthuchienquahan&ubtinh=True",
                        TrongHan_DaHoanThanh = sltk.DoneInTerm,
                        Url_TrongHan_DaHoanThanh = $"{domain}?status=dathuchiendunghan&ubtinh=True",
                        QuaHan_DaHoanThanh = sltk.DoneOutTerm,
                        Url_QuaHan_DaHoanThanh = $"{domain}?status=dathuchientrehan&ubtinh=True",
                        TrongHan_ChuaThucHien = sltk.NotPerformInTerm,
                        Url_TrongHan_ChuaThucHien = $"{domain}?status=chuathuchientronghan&ubtinh=True",
                        QuaHan_ChuaThucHien = sltk.NotPerformOutTerm,
                        Url_QuaHan_ChuaThucHien = $"{domain}?status=chuathuchienquahan&ubtinh=True",
                    });
                }
            }

            return listSoLieu;
        }

        [WebMethod]
        public List<SoLieuDonVi> getSoLieuCuaTatCaCacPhongBanThuocDonvi(string maDonVi)
        {
            List<SoLieuDonVi> listSoLieuThangCoQuan = new List<SoLieuDonVi>();
            return listSoLieuThangCoQuan;
        }

        [WebMethod]
        public List<SoLieuDonVi> getSoLieuPhongBan(string maDonVi, params string[] listUserName)
        {
            List<SoLieuDonVi> listSoLieuThangCoQuan = new List<SoLieuDonVi>();
            return listSoLieuThangCoQuan;
        }

        [WebMethod]
        public SoLieuCaNhan getSoLieuCaNhanChuyenVienTheoDoi(string maDonVi, string userName)
        {
            var user = UserServices.GetUser(userName: userName);

            if (user != null)
            {
                var sltk = RequestServices.LaySoLieuThongKe(MaNguoiTheoDoi: user.UserID);
                string domain = $"http://ykcd.thuathienhue.egov.vn/Public/DanhSachYKCD.aspx?sid={user.UserID}";

                return new SoLieuCaNhan()
                {
                    MaCaNhan = sltk.ObjectID.ToString(),
                    HoVaTen = sltk.ObjectName,
                    TrongHan_DangThucHien = sltk.PerformingInTerm,
                    Url_TrongHan_DangThucHien = $"{domain}?status=dangthuchientronghan&ubtinh=True",
                    QuaHan_DangThucHien = sltk.PerformingOutTerm,
                    Url_QuaHan_DangThucHien = $"{domain}?status=dangthuchienquahan&ubtinh=True",
                    TrongHan_DaHoanThanh = sltk.DoneInTerm,
                    Url_TrongHan_DaHoanThanh = $"{domain}?status=dathuchiendunghan&ubtinh=True",
                    QuaHan_DaHoanThanh = sltk.DoneOutTerm,
                    Url_QuaHan_DaHoanThanh = $"{domain}?status=dathuchientrehan&ubtinh=True",
                    TrongHan_ChuaThucHien = sltk.NotPerformInTerm,
                    Url_TrongHan_ChuaThucHien = $"{domain}?status=chuathuchientronghan&ubtinh=True",
                    QuaHan_ChuaThucHien = sltk.NotPerformOutTerm,
                    Url_QuaHan_ChuaThucHien = $"{domain}?status=chuathuchienquahan&ubtinh=True",
                };
            }
            return new SoLieuCaNhan(); ;
        }


        [WebMethod]
        public SoLieuCaNhan getSoLieuCaNhanLanhDaoGiaoViec(string maDonVi, string userName)
        {
            var user = UserServices.GetUser(userName: userName);

            if (user != null)
            {
                var sltk = RequestServices.LaySoLieuThongKe(MaLanhDao: user.UserID);
                string domain = $"http://ykcd.thuathienhue.egov.vn/Public/DanhSachYKCD.aspx?pid={user.UserID}";

                return new SoLieuCaNhan()
                {
                    MaCaNhan = sltk.ObjectID.ToString(),
                    HoVaTen = sltk.ObjectName,
                    TrongHan_DangThucHien = sltk.PerformingInTerm,
                    Url_TrongHan_DangThucHien = $"{domain}?status=dangthuchientronghan&ubtinh=True",
                    QuaHan_DangThucHien = sltk.PerformingOutTerm,
                    Url_QuaHan_DangThucHien = $"{domain}?status=dangthuchienquahan&ubtinh=True",
                    TrongHan_DaHoanThanh = sltk.DoneInTerm,
                    Url_TrongHan_DaHoanThanh = $"{domain}?status=dathuchiendunghan&ubtinh=True",
                    QuaHan_DaHoanThanh = sltk.DoneOutTerm,
                    Url_QuaHan_DaHoanThanh = $"{domain}?status=dathuchientrehan&ubtinh=True",
                    TrongHan_ChuaThucHien = sltk.NotPerformInTerm,
                    Url_TrongHan_ChuaThucHien = $"{domain}?status=chuathuchientronghan&ubtinh=True",
                    QuaHan_ChuaThucHien = sltk.NotPerformOutTerm,
                    Url_QuaHan_ChuaThucHien = $"{domain}?status=chuathuchienquahan&ubtinh=True",
                };
            }
            return new SoLieuCaNhan();
        }

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
