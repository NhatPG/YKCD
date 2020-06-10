using System;
using System.Linq;
using Framework.Helper;
using Framework.Web;
using YKCD.Agency.Business.Components;
using YKCD.Agency.Business.Services;
using GemBox.Document;
using GemBox.Document.Tables;
using YKCD.Agency.Application.Helper;

namespace YKCD.Agency.Application.Agency
{
    public partial class ThongKe_YkcdUbndTinh : ListViewPageBase
    {
        public SoLieuThongKe tongCong = new SoLieuThongKe();

        protected override void SetDefaultValueForListView()
        {
            BaseListView = lvData;
        }

        protected override void BindOtherValue()
        {
            TuNgay.Text = new DateTime(DateTime.Now.Year, 1, 1).ToDateString();
            DenNgay.Text = DateTime.Now.ToDateString();
        }

        protected override void GetDataList()
        {
            var ketQua = RequestServices.LaySoLieuThongKe(YkcdCuaUbndTinh: true, tuNgay: TuNgay.Text.ToDateTime(), denNgay: DenNgay.Text.ToDateTime()).WrapInList();

            BaseCollection = ketQua;
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
            CreateDocument().Save(Response, "BC_ThongKe_YkcdUbndTinh.docx", SaveOptions.DocxDefault);
        }

        protected DocumentModel CreateDocument()
        {
            var item = RequestServices.LaySoLieuThongKe(YkcdCuaUbndTinh: true, tuNgay: TuNgay.Text.ToDateTime(), denNgay: DenNgay.Text.ToDateTime());

            DocumentModel document = WordExtensions.Load($@"{AppSettings.UploadFolder}\MauVanBan\MauThongKeYKCDUBTinh.docx");
            
            document.Content.Find("(TieuDe)")?.First()?.LoadText("TỔNG HỢP TÌNH HÌNH THỰC HIỆN Ý KIẾN CHỈ ĐẠO CỦA UBND TỈNH");
            document.Content.Find("(ThoiDiemThongKe)")?.First()?.LoadText($"Thống kê từ ngày {TuNgay.Text} đến ngày {DenNgay.Text}");
            document.Content.Find("(TenDonVi)")?.First().LoadText($@"{AppSettings.AGENCY_NAME}");
            document.Content.Find("(NgayBaoCao)")?.First()?.LoadText($"{AppSettings.DateRegion}, ngày {DateTime.Now.Date.Day} tháng {DateTime.Now.Date.Month} năm {DateTime.Now.Date.Year}");
            document.Content.Find("(SoBaoCao)")?.First()?.LoadText("");

            var dataTable = document.GetChildElements(true, ElementType.Table).Cast<Table>().ToArray()[1];

            dataTable.Rows.Insert(2, new TableRow(document,
                new TableCell(document, new Paragraph(document, item.NotPerformInTerm.ToString()).Center()),
                new TableCell(document, new Paragraph(document, item.NotPerformOutTerm.ToString()).Center()),
                new TableCell(document, new Paragraph(document, item.PerformingInTerm.ToString()).Center()),
                new TableCell(document, new Paragraph(document, item.PerformingOutTerm.ToString()).Center()),
                new TableCell(document, new Paragraph(document, item.WaitToConfirm.ToString()).Center()),
                new TableCell(document, new Paragraph(document, item.DoneInTerm.ToString()).Center()),
                new TableCell(document, new Paragraph(document, item.DoneOutTerm.ToString()).Center())
                ));

            return document;
        }
    }
}