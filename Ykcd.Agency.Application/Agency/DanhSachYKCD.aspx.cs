using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Framework.Helper;
using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Components;
using YKCD.Agency.Business.Entities;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Agency
{
    public partial class DanhSachYKCD : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
            if (Parameters.YkcdUbndTinh)
                lbSmallHeader.Text = $"Đơn vị thực hiện: {AppSettings.AGENCY_NAME}";
            else if (Parameters.DepartmentID > 0)
                lbSmallHeader.Text = $"Đơn vị thực hiện: {DepartmentServices.GetById(Parameters.DepartmentID).DepartmentName}";
            else if (Parameters.UserID > 0)
                lbSmallHeader.Text = $"Người thực hiện: {UserServices.GetById(Parameters.UserID).FullName}";
            else if (Parameters.StaffID > 0)
                lbSmallHeader.Text = $"Người theo dõi: {UserServices.GetById(Parameters.StaffID).FullName}";
            else if (Parameters.PresidentID > 0)
                lbSmallHeader.Text = $"Người ký văn bản: {UserServices.GetById(Parameters.PresidentID).FullName}";
            else if (Sessions.DepartmentID > 0 || Authenticator.CheckRole(UserRole.TruongPhongBan))
                lbSmallHeader.Text = $"Đơn vị thực hiện: {Sessions.DisplayName}";
            else if (Authenticator.CheckRole(UserRole.LanhDaoVP, UserRole.LanhDaoDonVi))
                lbSmallHeader.Text = $"Người ký văn bản: {Sessions.DisplayName}";
            else if (Authenticator.CheckRole(UserRole.ChuyenVienVP, UserRole.Administrator))
                lbSmallHeader.Text = $"Chuyên viên theo dõi: {Sessions.DisplayName}";
        }

        protected override void GetDataList()
        {
            DateTime? tuNgay = Parameters.FromDate;

            if (Parameters.YkcdUbndTinh)
                BaseCollection = RequestServices.GetList(YkcdCuaUbndTinh: true, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
            else if (Parameters.DepartmentID > 0)
                BaseCollection = RequestServices.GetList(MaDonViThucHien: Parameters.DepartmentID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
            else if (Parameters.UserID > 0)
                BaseCollection = RequestServices.GetList(MaNguoiThucHien: Parameters.UserID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
            else if (Parameters.StaffID > 0)
                BaseCollection = RequestServices.GetList(MaNguoiTheoDoi: Parameters.StaffID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
            else if (Parameters.PresidentID > 0)
                BaseCollection = RequestServices.GetList(MaNguoiGiaoViec: Parameters.PresidentID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
            else if (Sessions.DepartmentID > 0 || Authenticator.CheckRole(UserRole.TruongPhongBan))
                BaseCollection = RequestServices.GetList(MaDonViThucHien: Sessions.DepartmentID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
            else if (Authenticator.CheckRole(UserRole.LanhDaoDonVi, UserRole.LanhDaoVP))
                BaseCollection = RequestServices.GetList(MaNguoiGiaoViec: Sessions.UserID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
            else if (Authenticator.CheckRole(UserRole.ChuyenVienVP))
                BaseCollection = RequestServices.GetList(MaNguoiTheoDoi: Sessions.UserID, TrangThai: Parameters.Status, searchText: txtSearchText.Text, tuNgay: tuNgay).OrderByDescending(item => item.RequiredDate).ToList();
        }

        protected string DisplayReports(object reports)
        {
            if (reports != null)
            {
                return (reports as List<Report>)?.Select(report => $"Ngày <strong>{report.PerformOnDate.ToDateString()}</strong>: {report.ReportContent} ({report.ReporterName})").ToList().DisplayInBreakLine();
            }

            return string.Empty;
        }
    }
}