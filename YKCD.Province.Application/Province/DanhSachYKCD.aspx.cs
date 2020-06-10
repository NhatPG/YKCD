using System;
using System.Linq;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Province
{
    public partial class DanhSachYKCD : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
            if (Parameters.AgencyID > 0)
                lbSmallHeader.Text = $"Đơn vị thực hiện: {AgencyServices.GetById(Parameters.AgencyID).AgencyName}";
            else if(Parameters.StaffID > 0)
                lbSmallHeader.Text = $"Người theo dõi: {UserServices.GetById(Parameters.StaffID).FullName}";
            else if (Parameters.PresidentID > 0)
                lbSmallHeader.Text = $"Người ký văn bản: {UserServices.GetById(Parameters.PresidentID).FullName}";
            else if (Sessions.AgencyID > 0)
                lbSmallHeader.Text = $"Đơn vị thực hiện: {Sessions.DisplayName}";
            else if (Authenticator.CheckRole(UserRole.LanhDaoUBNDTinh, UserRole.LanhDaoVPUBNDTinh))
                lbSmallHeader.Text = $"Người ký văn bản: {Sessions.DisplayName}";
            else if (Authenticator.CheckRole(UserRole.ChuyenVienVPUBNDTinh))
                lbSmallHeader.Text = $"Chuyên viên theo dõi: {Sessions.DisplayName}";
        }

        protected override void GetDataList()
        {
            DateTime? tuNgay = Parameters.FromDate;

            if (Parameters.AgencyID > 0)
                BaseCollection = RequestServices.GetList(MaDonVi: Parameters.AgencyID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
            else if (Parameters.StaffID > 0)
                BaseCollection = RequestServices.GetList(MaNguoiTheoDoi: Parameters.StaffID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
            else if (Parameters.PresidentID > 0)
                BaseCollection = RequestServices.GetList(MaNguoiGiaoViec: Parameters.PresidentID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
            else if (Sessions.AgencyID > 0)
                BaseCollection = RequestServices.GetList(MaDonVi: Sessions.AgencyID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
            else if (Authenticator.CheckRole(UserRole.LanhDaoUBNDTinh, UserRole.LanhDaoVPUBNDTinh))
                BaseCollection = RequestServices.GetList(MaNguoiGiaoViec: Sessions.UserID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
            else if (Authenticator.CheckRole(UserRole.ChuyenVienVPUBNDTinh))
                BaseCollection = RequestServices.GetList(MaNguoiTheoDoi: Sessions.UserID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
        }
    }
}