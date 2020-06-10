using System;
using System.Linq;
using Framework.Helper;
using Framework.Web;
using YKCD.Agency.Business.Components;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Agency
{
    public partial class ThongKe_TheoChuyenVien : ListViewPageBase
    {
        public SoLieuThongKe tongCong = new SoLieuThongKe();

        protected override void SetDefaultValueForListView()
        {
            BaseListView = lvData;
            GroupColumn = "GroupName";
            TotalColumns = 9;
        }

        protected override void BindOtherValue()
        {
            TuNgay.Text = new DateTime(DateTime.Now.Year, 1, 1).ToDateString();
            DenNgay.Text = DateTime.Now.ToDateString();
        }

        protected override void GetDataList()
        {
            var ketQua = RequestServices.ThongKeTheoTatCaChuyenVienTheoDoi(TuNgay.Text.ToDateTime());

            BaseCollection = ketQua;

            tongCong = new SoLieuThongKe
            {
                NotPerformInTerm = ketQua.Sum(i => i.NotPerformInTerm),
                NotPerformOutTerm = ketQua.Sum(i => i.NotPerformOutTerm),
                PerformingInTerm = ketQua.Sum(i => i.PerformingInTerm),
                PerformingOutTerm = ketQua.Sum(i => i.PerformingOutTerm),
                WaitToConfirm = ketQua.Sum(i => i.WaitToConfirm),
                DoneInTerm = ketQua.Sum(i => i.DoneInTerm),
                DoneOutTerm = ketQua.Sum(i => i.DoneOutTerm)
            };
        }

        protected void btnThongKe_OnClick(object sender, EventArgs e)
        {
            BindDataToListView();
        }
    }
}