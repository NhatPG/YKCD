using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Helper;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Province
{
    public partial class BangThongKe : System.Web.UI.Page
    {
        public int NoDocumentOnTime = 0;
        public int NoDocumentLate = 0;
        public int DocumentOnTime = 0;
        public int DocumentLate = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authenticator.CheckRole(UserRole.ChuyenVienVPUBNDTinh))
            {
                RequestDetail.BindData(RequestServices.LaySoLieuThongKe(MaNguoiTheoDoi: Sessions.UserID).WrapInList());

                NoDocumentOnTime = MettingServices.GetList()
                                     ?.Where(item => item.StaffID == Sessions.UserID)
                                     ?.Where(item => (item.DocumentID <= 0 && HolidayServices.GetBusinessDays(item.Time, DateTime.Now) <= 3)).Count() ?? 0;

                NoDocumentLate = MettingServices.GetList()
                                       ?.Where(item => item.StaffID == Sessions.UserID)
                                       ?.Where(item => (item.DocumentID <= 0 && HolidayServices.GetBusinessDays(item.Time, DateTime.Now) > 3)).Count() ?? 0;

                DocumentOnTime = MettingServices.GetList()
                    ?.Where(item => item.StaffID == Sessions.UserID)
                    ?.Where(item => (item.DocumentID > 0 && HolidayServices.GetBusinessDays(item.Time, item.Document.ReleaseDate) <= 3)).Count() ?? 0;

                DocumentLate = MettingServices.GetList()
                    ?.Where(item => item.StaffID == Sessions.UserID)
                    ?.Where(item => (item.DocumentID > 0 && HolidayServices.GetBusinessDays(item.Time, item.Document.ReleaseDate) > 3)).Count() ?? 0;
            }
            else if (Authenticator.CheckRole(UserRole.LanhDaoUBNDTinh, UserRole.LanhDaoVPUBNDTinh))
            {
                RequestDetail.BindData(RequestServices.LaySoLieuThongKe(MaLanhDao: Sessions.UserID).WrapInList());

                NoDocumentOnTime = MettingServices.GetList()
                                       ?.Where(item => item.PresidentID == Sessions.UserID)
                                       ?.Where(item => (item.DocumentID <= 0 && HolidayServices.GetBusinessDays(item.Time, DateTime.Now) <= 3)).Count() ?? 0;

                NoDocumentLate = MettingServices.GetList()
                                     ?.Where(item => item.PresidentID == Sessions.UserID)
                                     ?.Where(item => (item.DocumentID <= 0 && HolidayServices.GetBusinessDays(item.Time, DateTime.Now) > 3)).Count() ?? 0;

                DocumentOnTime = MettingServices.GetList()
                                     ?.Where(item => item.PresidentID == Sessions.UserID)
                                     ?.Where(item => (item.DocumentID > 0 && HolidayServices.GetBusinessDays(item.Time, item.Document.ReleaseDate) <= 3)).Count() ?? 0;

                DocumentLate = MettingServices.GetList()
                                   ?.Where(item => item.PresidentID == Sessions.UserID)
                                   ?.Where(item => (item.DocumentID > 0 && HolidayServices.GetBusinessDays(item.Time, item.Document.ReleaseDate) > 3)).Count() ?? 0;
            }
            else if (Sessions.AgencyID > 0)
            {
                RequestDetail.BindData(RequestServices.LaySoLieuThongKe(MaDonVi: Sessions.AgencyID).WrapInList());
            }

        }

        
    }
}