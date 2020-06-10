using System.Linq;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Public
{
    public partial class DanhSachYKCD : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
            if (Parameters.AgencyID > 0)
                lbSmallHeader.Text = $"Đơn vị thực hiện: {AgencyServices.GetById(Parameters.AgencyID).AgencyName}";
            else if (Parameters.PresidentID > 0)
                lbSmallHeader.Text = $"Người ký văn bản: {UserServices.GetById(Parameters.PresidentID).FullName}";
            else if (Parameters.StaffID > 0)
                lbSmallHeader.Text = $"Chuyên viên theo dõi: {UserServices.GetById(Parameters.StaffID).FullName}";
        }

        protected override void GetDataList()
        {
            if (Parameters.AgencyID > 0)
            {
                BaseCollection = RequestServices.GetList(MaDonVi: Parameters.AgencyID, TrangThai: Parameters.Status).OrderByDescending(item => item.RequiredDate).ToList();
            }
            else if (Parameters.PresidentID > 0)
            {
                BaseCollection = RequestServices.GetList(MaNguoiGiaoViec: Parameters.PresidentID, TrangThai: Parameters.Status).OrderByDescending(item => item.RequiredDate).ToList();
            }
            else if (Parameters.StaffID > 0)
            {
                BaseCollection = RequestServices.GetList(MaNguoiTheoDoi: Parameters.StaffID, TrangThai: Parameters.Status).OrderByDescending(item => item.RequiredDate).ToList();
            }
        }
    }
}