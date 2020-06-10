using System;
using System.Linq;
using Framework.Helper;
using Framework.Web;
using GemBox.Document;
using GemBox.Document.Tables;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.SubAgency
{
    public partial class ThongKe_CapNhatVanBan : ListViewPageBase
    {
        protected override void SetDefaultValueForListView()
        {
            BaseListView = lvData;
            GroupColumn = "WriterName";
            TotalColumns = 6;
        }

        protected override void BindOtherValue()
        {
            TuNgay.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToDateString();
            DenNgay.Text = DateTime.Now.ToDateString();
        }

        protected override void GetDataList()
        {
            BaseCollection = DocumentServices.GetList(fromDate: TuNgay.Text.ToDateTime(),
                toDate: DenNgay.Text.ToDateTime(), agencyDocument: true)
                .OrderBy(item => item?.Writer?.Department?.DisplayOrder)
                .ThenBy(item => item?.Writer?.DepartmentID);
        }

        protected void btnThongKe_OnClick(object sender, EventArgs e)
        {
            BindDataToListView();
        }

        protected DocumentModel CreateDocument()
        {
            DocumentModel document = WordExtensions.Load($@"{AppSettings.UploadFolder}\MauVanBan\MauDanhSachYKCD.docx");

            document.Content.Find("(TieuDe)")?.First()?.LoadText($"THỐNG KÊ VIỆC CẬP NHẬT VĂN BẢN CHỈ ĐẠO CỦA {AppSettings.AGENCY_NAME.ToUpper()}");
            document.Content.Find("(ThoiDiemThongKe)")?.First()?.LoadText($"Thống kê từ ngày {TuNgay.Text} đến ngày {DenNgay.Text}");
            document.Content.Find("(NgayBaoCao)")?.First()?.LoadText($"{AppSettings.DateRegion}, ngày {DateTime.Now.Date.Day} tháng {DateTime.Now.Date.Month} năm {DateTime.Now.Date.Year}");

            var dataTable = document.GetChildElements(true, ElementType.Table).Cast<Table>().ToArray()[1];
            int stt = 1;
            int currentIndex = 1;
            int currentGroup = 0;

            foreach (var item in DocumentServices.GetList(fromDate: TuNgay.Text.ToDateTime(),
                toDate: DenNgay.Text.ToDateTime())
                .OrderBy(item => item?.Writer?.Department?.DisplayOrder)
                .ThenBy(item => item?.Writer?.DepartmentID))
            {
                if (currentGroup != item.WriterID)
                {
                    currentGroup = item.WriterID;
                    dataTable.Rows.Insert(currentIndex, new TableRow(document, new TableCell(document, new Paragraph(document, item.WriterName).SetStyle(WordExtensions.BoldTextStyle)) { ColumnSpan = 6 }));
                    currentIndex++;
                }

                dataTable.Rows.Insert(
                    currentIndex, new TableRow(document,
                        new TableCell(document, new Paragraph(document, stt.ToString()).Center()),
                        new TableCell(document, new Paragraph(document, item.DocumentCode?.RemoveBreakLineCharacters())),
                        new TableCell(document, new Paragraph(document, item.ReleaseDate?.ToDateString()).Center()),
                        new TableCell(document, new Paragraph(document, item.CreatedTime?.ToDateString()).Center()),
                        new TableCell(document, new Paragraph(document, item.MainContent?.RemoveBreakLineCharacters())),
                        new TableCell(document, new Paragraph(document, new Run(document, "")).Center())
                        ));
                stt++;
                currentIndex++;
            }
            return document;
        }

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            CreateDocument()?.Print();
        }

        protected void btnDownLoad_OnClick(object sender, EventArgs e)
        {
            CreateDocument()?.Save(Response, "BC_ThongKe_CapNhatVanBan.docx", SaveOptions.DocxDefault);
        }
    }
}