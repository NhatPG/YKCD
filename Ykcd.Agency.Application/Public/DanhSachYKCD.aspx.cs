using System.Linq;
using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Public
{
    public partial class DanhSachYKCD : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
            if (Parameters.DepartmentID > 0)
            {
                lbSmallHeader.Text = $"Đơn vị thực hiện: {DepartmentServices.GetById(Parameters.DepartmentID).DepartmentName}";
            }
            else if (Parameters.PresidentID > 0)
            {
                lbSmallHeader.Text = $"Người ký văn bản: {UserServices.GetById(Parameters.PresidentID).FullName}";
            }
            else if (Parameters.StaffID > 0)
            {
                lbSmallHeader.Text = $"Chuyên viên theo dõi: {UserServices.GetById(Parameters.StaffID).FullName}";
            }
            else if (Parameters.YkcdUbndTinh)
                lbMainHeader.Text = "DANH SÁCH Ý KIẾN CHỈ ĐẠO CỦA UBND TỈNH";
        }

        protected override void GetDataList()
        {
            if (Parameters.DepartmentID > 0)
            {
                BaseCollection = RequestServices.GetList(MaDonViThucHien: Parameters.DepartmentID, TrangThai: Parameters.Status).OrderByDescending(item => item.RequiredDate).ToList();
            }
            else if (Parameters.PresidentID > 0)
            {
                BaseCollection = RequestServices.GetList(MaNguoiGiaoViec: Parameters.PresidentID, TrangThai: Parameters.Status).OrderByDescending(item => item.RequiredDate).ToList();
            }
            else if (Parameters.StaffID > 0)
            {
                BaseCollection = RequestServices.GetList(MaNguoiTheoDoi: Parameters.StaffID, TrangThai: Parameters.Status).OrderByDescending(item => item.RequiredDate).ToList();
            }
            else if (Parameters.YkcdUbndTinh)
            {
                BaseCollection = RequestServices.GetList(YkcdCuaUbndTinh:true, TrangThai: Parameters.Status).OrderByDescending(item => item.RequiredDate).ToList();
            }
        }
    }
}