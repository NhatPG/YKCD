using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Helper;
using Framework.Web;
using GemBox.Document;
using GemBox.Document.Tables;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Province
{
    public partial class ThongKe_CapNhatVanBan : ListViewPageBase
    {
        public static List<DateTime> holidays = ToDateList(HolidayServices.GetList());

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
                toDate: DenNgay.Text.ToDateTime())
                .OrderBy(item => item.Writer.Department.DisplayOrder)
                .ThenBy(item => item.Writer.DepartmentID)
                .ThenBy(item => item.Writer.DisplayOrder)
                .ThenBy(item => item.WriterID);
        }

        protected void btnThongKe_OnClick(object sender, EventArgs e)
        {
            BindDataToListView();
        }

        protected DocumentModel CreateDocument()
        {
            DocumentModel document = WordExtensions.Load($@"{AppSettings.UploadFolder}\MauVanBan\MauDanhSachYKCD.docx");

            document.Content.Find("(TieuDe)")?.First()?.LoadText("THỐNG KÊ VIỆC CẬP NHẬT VĂN BẢN CHỈ ĐẠO CỦA UBND TỈNH");
            document.Content.Find("(ThoiDiemThongKe)")?.First()?.LoadText($"Thống kê từ ngày {TuNgay.Text} đến ngày {DenNgay.Text}");
            document.Content.Find("(NgayBaoCao)")?.First()?.LoadText($"TP Huế, ngày {DateTime.Now.Date.Day} tháng {DateTime.Now.Date.Month} năm {DateTime.Now.Date.Year}");

            var dataTable = document.GetChildElements(true, ElementType.Table).Cast<Table>().ToArray()[1];
            int stt = 1;
            int currentIndex = 1;
            int currentGroup = 0;

            foreach (var item in DocumentServices.GetList(fromDate: TuNgay.Text.ToDateTime(),
                toDate: DenNgay.Text.ToDateTime())
                .OrderBy(item => item.Writer.Department.DisplayOrder)
                .ThenBy(item => item.Writer.DepartmentID)
                .ThenBy(item => item.Writer.DisplayOrder)
                .ThenBy(item => item.WriterID))
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
                        new TableCell(document, new Paragraph(document, item.DocumentCode.RemoveBreakLineCharacters())),
                        new TableCell(document, new Paragraph(document, item.ReleaseDate.ToDateString()).Center()),
                        new TableCell(document, new Paragraph(document, item.CreatedTime.ToDateString()).Center()),
                        new TableCell(document, new Paragraph(document, item.MainContent.RemoveBreakLineCharacters())),
                        new TableCell(document, new Paragraph(document, new Run(document, ShowDocumentUpdateStatus(item.ReleaseDate, item.CreatedTime.Value.Date))).Center())
                        ));
                stt++;
                currentIndex++;
            }
            return document;
        }

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            CreateDocument().Print();
        }

        protected void btnDownLoad_OnClick(object sender, EventArgs e)
        {
            CreateDocument().Save(Response, "BC_ThongKe_TheoChuyenVien.docx", SaveOptions.DocxDefault);
        }

        public static string ShowDocumentUpdateStatus(DateTime startD, DateTime endD)
        {
            if (GetBusinessDays(startD.Date, endD.Date) > 2)
            {
                return $"{GetBusinessDays(startD, endD) - 2} ngày";
            }

            return "Đúng hạn";
        }

        public static double GetBusinessDays(DateTime startD, DateTime endD)
        {
            int result = 0;            

            DateTime check = startD.Date;

            while (check < endD.Date)
            {
                if (check.DayOfWeek != DayOfWeek.Saturday && check.DayOfWeek != DayOfWeek.Sunday &&
                    !holidays.Contains(check))
                {
                    result++;
                }

                check = check.AddDays(1);
            }

            return result;
        }

        public static List<DateTime> ToDateList(List<Holiday> holidayList)
        {
            List<DateTime> result = new List<DateTime>();

            foreach (var holiday in holidayList)
            {
                DateTime check = holiday.FromDate;

                while (check <= holiday.ToDate)
                {
                    if(!result.Contains(check))
                    {
                        result.Add(check.Date);
                        check = check.AddDays(1);
                    }
                }
            }

            return result;
        }
    }
}