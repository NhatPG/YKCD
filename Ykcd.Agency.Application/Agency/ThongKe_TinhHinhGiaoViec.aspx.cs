using System;
using System.Linq;
using Framework.Helper;
using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Entities;
using YKCD.Agency.Business.Services;
using GemBox.Document;
using GemBox.Document.Tables;
using System.Collections.Generic;
using YKCD.Agency.Business.Components;

namespace YKCD.Agency.Application.Agency
{
    public partial class ThongKe_TinhHinhGiaoViec : ListViewPageBase
    {
        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
        }

        protected override void BindOtherValue()
        {
            TuNgay.Text = new DateTime(DateTime.Now.Year, 1, 1).ToDateString();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            CreateDocument().Print();
        }

        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            CreateDocument().Save(Response, "BC_ThongKe_GiaoViec.docx", SaveOptions.DocxDefault);
        }

        protected override void GetDataList()
        {
            BaseCollection = RequestServices.GetList(YkcdCuaUbndTinh: true, tuNgay: TuNgay.Text.ToDateTimeNullable(), denNgay: DenNgay.Text.ToDateTimeNullable()).Where(item => TrangThai.GetSelectedValues().Contains(item.Status)).OrderBy(item => item.RequiredDate).ToList();
        }

        protected void btnThongKe_Click(object sender, EventArgs e)
        {
            BindDataToListView();
        }

        protected string DisplayReports(object reports)
        {
            if (reports != null)
            {
                return (reports as List<Report>)?.Select(report => report.ReportContent)?.ToList()?.DisplayInBreakLine();
            }

            return string.Empty;
        }

        protected Paragraph DisplayDepartments(DocumentModel document, long requestID)
        {
            var items = RequestServices.GetById(requestID)?.Performs?.Select(i => i.DepartmentName);
            Paragraph result = null;
            List<Inline> inlineItems = new List<Inline>();

            foreach (var item in items)
            {
                inlineItems.Add(new Run(document, item));
                inlineItems.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
            }

            result = new Paragraph(document, inlineItems.ToArray());
            return result;
        }

        protected DocumentModel CreateDocument()
        {
            DocumentModel document = WordExtensions.Load($@"{AppSettings.UploadFolder}\MauVanBan\MauDanhSachYKCDGiaoViec.docx");

            document.Content.Find("(NgayBaoCao)")?.First()?.LoadText($"{AppSettings.DateRegion}, ngày {DateTime.Now.Date.Day} tháng {DateTime.Now.Date.Month} năm {DateTime.Now.Date.Year}");

            var dataTable = document.GetChildElements(true, ElementType.Table).Cast<GemBox.Document.Tables.Table>().ToArray()[1];
            int stt = 1;
            int currentIndex = 1;

            foreach (var item in RequestServices.GetList(YkcdCuaUbndTinh: true, tuNgay: TuNgay.Text.ToDateTimeNullable(), denNgay: DenNgay.Text.ToDateTimeNullable()).Where(item => TrangThai.GetSelectedValues().Contains(item.Status)).OrderBy(item => item.RequiredDate).ToList())
            {

                dataTable.Rows.Insert(
                    currentIndex, new TableRow(document,
                        new TableCell(document, new Paragraph(document, stt.ToString()).Center()),
                        new TableCell(document, new Paragraph(document, item?.Document?.DocumentCode?.RemoveBreakLineCharacters()).Center()),
                        new TableCell(document, new Paragraph(document, item?.Document?.ReleaseDate?.ToDateString())?.Center()),
                        new TableCell(document, new Paragraph(document, item?.RequestContent?.RemoveBreakLineCharacters())),
                        new TableCell(document, new Paragraph(document, item?.RequiredDate.ToDateString())?.Center()),
                        new TableCell(document, new Paragraph(document, item?.RequesterID > 0 ? UserServices.GetById(item.RequesterID).FullName?.RemoveBreakLineCharacters() : "")),
                        new TableCell(document, item?.Performs.Select(i => i.DepartmentName).ToList().DisplayList(document)),
                        new TableCell(document, item?.Reports?.Select(i => i.ReportContent).ToList().DisplayList(document))
                        ));
                stt++;
                currentIndex++;
            }

            return document;
        }
    }
}