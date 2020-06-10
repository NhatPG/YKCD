using System;
using System.Linq;
using Framework.Helper;
using Framework.Web;
using GemBox.Document;
using GemBox.Document.Tables;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Province
{
    public partial class ThongKe_KinhTeNganhKntc : ListViewPageBase
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
            TuNgay.Text = DateTime.Now.AddDays(-7).ToDateString();
            DenNgay.Text = DateTime.Now.ToDateString();
        }

        protected override void GetDataList()
        {
            var ketQua = RequestServices.ThongKeTheoTatCaChuyenVienTheoDoi()
                .Where(item => (item.GroupID == 2 || item.GroupID == 6) && !item.ObjectName.Equals("Nguyễn Hữu Phước"))
                .ToList();

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

            var requestInWeek = DocumentServices.GetList(fromDate: TuNgay.Text.ToDateTimeNullable(),
                    toDate: DenNgay.Text.ToDateTimeNullable())
                .Where(doc => doc.Writer.DepartmentID == 2 || doc.Writer.DepartmentID == 6 && doc.WriterID != 26).ToList().Sum(doc => doc.Requests.Count);

            lbYkcdBanHanhTrongTuan.Text = requestInWeek.ToString();
            lbChuaThucHien.Text = (tongCong.NotPerformInTerm + tongCong.NotPerformOutTerm).ToString();
            lbChuaDenHan.Text = tongCong.NotPerformInTerm.ToString();
            lbTreHan.Text = tongCong.NotPerformOutTerm.ToString();
            lbDangThucHien.Text = (tongCong.PerformingInTerm + tongCong.PerformingOutTerm).ToString();
        }

        protected void btnThongKe_OnClick(object sender, EventArgs e)
        {
            BindDataToListView();
        }

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            CreateDocument().Print();
        }

        protected void btnDownLoad_OnClick(object sender, EventArgs e)
        {
            CreateDocument().Save(Response, "BC_ThongKe_TheoChuyenVien.docx", SaveOptions.DocxDefault);
        }

        protected DocumentModel CreateDocument()
        {
            DocumentModel document = WordExtensions.Load($@"{AppSettings.UploadFolder}\MauVanBan\MauThongKeYKCD.docx");

            document.Content.Find("(TieuDe)")?.First()?.LoadText("BÁO CÁO TÌNH HÌNH THEO DÕI VIỆC THỰC HIỆN Ý KIẾN CHỈ ĐẠO CỦA UBND TỈNH");
            document.Content.Find("(ThoiDiemThongKe)")?.First()?.LoadText($"Thống kê từ ngày {TuNgay.Text} đến ngày {DenNgay.Text}");
            document.Content.Find("(TenDonVi)")?.First().LoadText(@"UBND TỈNH THỪA THIÊN HUẾ");
            document.Content.Find("(NgayBaoCao)")?.First()?.LoadText($"TP Huế, ngày {DateTime.Now.Date.Day} tháng {DateTime.Now.Date.Month} năm {DateTime.Now.Date.Year}");
            document.Content.Find("(SoBaoCao)")?.First()?.LoadText("");

            var dataTable = document.GetChildElements(true, ElementType.Table).Cast<Table>().ToArray()[1];
            int stt = 1;
            int currentIndex = 2;
            int currentGroup = 0;

            foreach (var item in RequestServices.ThongKeTheoTatCaChuyenVienTheoDoi(TuNgay.Text.ToDateTime()).Where(i => i.Total > 0).ToList())
            {
                if (currentGroup != item.GroupID)
                {
                    currentGroup = item.GroupID;
                    dataTable.Rows.Insert(currentIndex, new TableRow(document, new TableCell(document, new Paragraph(document, item.GroupName).SetStyle(WordExtensions.BoldTextStyle)) { ColumnSpan = 9 }));
                    currentIndex++;
                }

                dataTable.Rows.Insert(
                    currentIndex, new TableRow(document,
                        new TableCell(document, new Paragraph(document, stt.ToString()).Center()),
                        new TableCell(document, new Paragraph(document, item.ObjectName)),
                        new TableCell(document, new Paragraph(document, item.NotPerformInTerm.ToString()).Center()),
                        new TableCell(document, new Paragraph(document, item.NotPerformOutTerm.ToString()).Center()),
                        new TableCell(document, new Paragraph(document, item.PerformingInTerm.ToString()).Center()),
                        new TableCell(document, new Paragraph(document, item.PerformingOutTerm.ToString()).Center()),
                        new TableCell(document, new Paragraph(document, item.WaitToConfirm.ToString()).Center()),
                        new TableCell(document, new Paragraph(document, item.DoneInTerm.ToString()).Center()),
                        new TableCell(document, new Paragraph(document, item.DoneOutTerm.ToString()).Center())
                        ));
                stt++;

                currentIndex++;
            }
            return document;
        }
    }
}