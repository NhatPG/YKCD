using System;
using System.Linq;
using Framework.Helper;
using Framework.Web;
using GemBox.Document;
using GemBox.Document.Tables;
using YKCD.SubAgency.Application.Helper;
using YKCD.SubAgency.Business.Components;
using YKCD.SubAgency.Business.Entities;
using YKCD.SubAgency.Business.Services;
using System.Collections.Generic;

namespace YKCD.SubAgency.Application.SubAgency
{
    public partial class ThongKe_TheoDonVi : ListViewPageBase
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
            NhomDonVi.BindData(DepartmentGroupServices.GetList(), "DepartmentGroupID", "DepartmentGroupName");
        }

        protected override void GetDataList()
        {
            var ketQua = RequestServices.ThongKeDonViTheoNhom(NhomDonVi.SelectedValue.ToInteger(), TuNgay.Text.ToDateTime());

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

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            CreateDocument().Print();
        }

        protected void NhomDonVi_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataToListView();
        }

        protected void btnDownLoad_OnClick(object sender, EventArgs e)
        {
            CreateDocument().Save(Response, "BC_ThongKe_TheoDonVi.docx", SaveOptions.DocxDefault);
        }

        protected DocumentModel CreateDocument()
        {
            DocumentModel document = WordExtensions.Load($@"{AppSettings.UploadFolder}\MauVanBan\MauThongKeYKCD.docx");

            document.Content.Find("(TieuDe)")?.First()?.LoadText("BÁO CÁO TÌNH HÌNH THỰC HIỆN Ý KIẾN CHỈ ĐẠO CỦA UBND TỈNH");
            document.Content.Find("(ThoiDiemThongKe)")?.First()?.LoadText($"Thống kê từ ngày {TuNgay.Text} đến ngày {DenNgay.Text}");
            document.Content.Find("(TenDonVi)")?.First().LoadText(@"UBND TỈNH THỪA THIÊN HUẾ");
            document.Content.Find("(NgayBaoCao)")?.First()?.LoadText($"TP Huế, ngày {DateTime.Now.Date.Day} tháng {DateTime.Now.Date.Month} năm {DateTime.Now.Date.Year}");
            document.Content.Find("(SoBaoCao)")?.First()?.LoadText("");

            var dataTable = document.GetChildElements(true, ElementType.Table).Cast<Table>().ToArray()[1];
            int stt = 1;
            int currentIndex = 2;
            int currentGroup = 0;

            foreach (var item in RequestServices.ThongKeDonViTheoNhom(NhomDonVi.SelectedValue.ToInteger(), TuNgay.Text.ToDateTime()).Where(i => i.Total > 0).ToList())
            {
                if (currentGroup != item.GroupID)
                {
                    currentGroup = item.GroupID;
                    dataTable.Rows.Insert(currentIndex, new GemBox.Document.Tables.TableRow(document, new GemBox.Document.Tables.TableCell(document, new Paragraph(document, item.GroupName).SetStyle(WordExtensions.BoldTextStyle)) { ColumnSpan = 9 }));
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

        protected void btnList_OnClick(object sender, EventArgs e)
        {
            DocumentModel document = WordExtensions.Load($@"{AppSettings.UploadFolder}\MauVanBan\MauDanhSachYKCD_TheoDonVi.docx", fontSize: 11);

            var dataTable = document.GetChildElements(true, ElementType.Table).Cast<Table>().ToArray()[0];
            int stt = 1;
            int currentIndex = 1;
            int currentGroup = 0;

            foreach (var department in DepartmentServices.GetList(NhomDonVi.SelectedValue.ToInteger()))
            {
                var ykcdAll = RequestServices.GetList(MaDonViThucHien: department.DepartmentID, tuNgay: TuNgay.Text.ToDateTimeNullable());

                if (ykcdAll?.Count > 0)
                {
                    var chuaThucHien = ykcdAll.Where(item => item.Status == 0).ToList();

                    var chuaThucHienTrongHan = chuaThucHien.Where(item => item.RequiredDate >= DateTime.Now.Date).ToList();

                    var chuaThucHienQuaHan = chuaThucHien.Where(item => item.RequiredDate < DateTime.Now.Date).ToList();

                    var dangThucHien = ykcdAll.Where(item => item.Status == 1).ToList();

                    var dangThucHienTrongHan = dangThucHien.Where(item => item.RequiredDate >= DateTime.Now.Date).ToList();

                    var dangThucHienQuaHan = dangThucHien.Where(item => item.RequiredDate < DateTime.Now.Date).ToList();

                    var daHoanThanh = ykcdAll.Where(item => item.Status == 2).ToList();

                    var daHoanThanhTrongHan = daHoanThanh.Where(item => item.RequiredDate >= item.FinishedOnDate).ToList();

                    var daHoanThanhQuaHan = daHoanThanh.Where(item => item.RequiredDate < item.FinishedOnDate).ToList();

                    dataTable.Rows.Insert(
                        currentIndex,
                        new TableRow(document,
                            new TableCell(document,
                                new Paragraph(document,
                                    new Run(document, department.DepartmentName.ToUpper()),
                                    new Run(document, " (Chưa thực hiện: ") { CharacterFormat = new CharacterFormat() { Bold = true } },
                                    new Run(document, $"Quá hạn: {chuaThucHienQuaHan.Count}, Chưa đến hạn: {chuaThucHienTrongHan.Count}; ") { CharacterFormat = new CharacterFormat() { Bold = false } },
                                    new Run(document, " (Đang thực hiện: ") { CharacterFormat = new CharacterFormat() { Bold = true } },
                                    new Run(document, $"Quá hạn: {dangThucHienQuaHan.Count}, Chưa đến hạn: {dangThucHienTrongHan.Count}; ") { CharacterFormat = new CharacterFormat() { Bold = false } },
                                    new Run(document, " (Đã hoàn thành: ") { CharacterFormat = new CharacterFormat() { Bold = true } },
                                    new Run(document, $"Trễ hạn: {daHoanThanhQuaHan.Count}, Đúng hạn: {daHoanThanhTrongHan.Count}) ") { CharacterFormat = new CharacterFormat() { Bold = false } })
                                .SetStyle(WordExtensions.BoldTextStyle))
                            {
                                ColumnSpan = 6,
                                CellFormat = new TableCellFormat()
                                {
                                    BackgroundColor = new Color(251, 212, 180)
                                }
                            }));

                    currentIndex++;

                    if (chuaThucHien?.Count > 0)
                    {
                        if (chuaThucHienQuaHan?.Count > 0)
                        {
                            stt = 1;

                            dataTable.Rows.Insert(currentIndex,
                            new TableRow(document,
                                new TableCell(document,
                                    new Paragraph(document, "CHƯA THỰC HIỆN")
                                    .SetStyle(WordExtensions.BoldTextStyle)
                                    .Center())
                                {
                                    RowSpan = chuaThucHien.Count + 1 + (chuaThucHienTrongHan?.Count > 0 ? 1 : 0),
                                    CellFormat = new TableCellFormat
                                    {
                                        TextDirection = TableCellTextDirection.BottomToTop,
                                        BackgroundColor = new Color(217, 149, 148)
                                    }
                                },
                                new TableCell(document,
                                    new Paragraph(document, "Quá hạn").SetStyle(WordExtensions.BoldTextStyle))
                                {
                                    ColumnSpan = 5,
                                    CellFormat = new TableCellFormat
                                    {
                                        BackgroundColor = new Color(217, 149, 148)
                                    }
                                }));
                            currentIndex++;

                            foreach (var item in chuaThucHienQuaHan)
                            {
                                AddYkcdRow(ref dataTable, item, currentIndex, stt);

                                currentIndex++;
                                stt++;
                            }
                        }

                        if (chuaThucHienTrongHan?.Count > 0)
                        {
                            stt = 1;

                            if (chuaThucHienQuaHan.Count == 0)
                            {
                                dataTable.Rows.Insert(currentIndex,
                                    new TableRow(document,
                                        new TableCell(document,
                                            new Paragraph(document, "CHƯA THỰC HIỆN").Center()
                                            .SetStyle(WordExtensions.BoldTextStyle))
                                        {
                                            RowSpan = chuaThucHien.Count + 1,
                                            CellFormat = new TableCellFormat
                                            {
                                                TextDirection = TableCellTextDirection.BottomToTop,
                                                BackgroundColor = new Color(217, 149, 148)
                                            }
                                        },
                                        new TableCell(document,
                                            new Paragraph(document, "Chưa đến hạn").SetStyle(WordExtensions.BoldTextStyle))
                                        {
                                            ColumnSpan = 5,
                                            CellFormat = new TableCellFormat
                                            {
                                                BackgroundColor = new Color(194, 214, 155)
                                            }
                                        }));
                            }
                            else
                            {
                                dataTable.Rows.Insert(currentIndex,
                                    new TableRow(document,
                                        new TableCell(document,
                                            new Paragraph(document, "Chưa đến hạn").SetStyle(WordExtensions.BoldTextStyle))
                                        {
                                            ColumnSpan = 5,
                                            CellFormat = new TableCellFormat
                                            {
                                                BackgroundColor = new Color(194, 214, 155)
                                            }
                                        }));

                            }
                            currentIndex++;

                            foreach (var item in chuaThucHienTrongHan)
                            {
                                AddYkcdRow(ref dataTable, item, currentIndex, stt);

                                currentIndex++;
                                stt++;
                            }
                        }
                    }

                    if (dangThucHien?.Count > 0)
                    {


                        if (dangThucHienQuaHan?.Count > 0)
                        {
                            stt = 1;

                            dataTable.Rows.Insert(currentIndex,
                            new TableRow(document,
                                new TableCell(document,
                                    new Paragraph(document, "ĐANG THỰC HIỆN").Center()
                                    .SetStyle(WordExtensions.BoldTextStyle))
                                {
                                    RowSpan = dangThucHien.Count + 1 + (dangThucHienTrongHan?.Count > 0 ? 1 : 0),
                                    CellFormat = new TableCellFormat
                                    {
                                        TextDirection = TableCellTextDirection.BottomToTop,
                                        BackgroundColor = new Color(194, 214, 155)
                                    }
                                },
                                new TableCell(document,
                                    new Paragraph(document, "Quá hạn").SetStyle(WordExtensions.BoldTextStyle))
                                {
                                    ColumnSpan = 5,
                                    CellFormat = new TableCellFormat
                                    {
                                        BackgroundColor = new Color(217, 149, 148)
                                    }
                                }));
                            currentIndex++;

                            foreach (var item in dangThucHienQuaHan)
                            {
                                AddYkcdRow(ref dataTable, item, currentIndex, stt);

                                currentIndex++;
                                stt++;
                            }
                        }


                        if (dangThucHienTrongHan?.Count > 0)
                        {
                            stt = 1;

                            if (dangThucHienQuaHan == null || dangThucHienQuaHan.Count == 0)
                            {
                                dataTable.Rows.Insert(currentIndex,
                            new TableRow(document,
                                new TableCell(document,
                                    new Paragraph(document, "ĐANG THỰC HIỆN").Center()
                                    .SetStyle(WordExtensions.BoldTextStyle))
                                {
                                    RowSpan = dangThucHien.Count + 1,
                                    CellFormat = new TableCellFormat
                                    {
                                        TextDirection = TableCellTextDirection.BottomToTop,
                                        BackgroundColor = new Color(194, 214, 155)
                                    }
                                },
                                new TableCell(document,
                                    new Paragraph(document, "Chưa đến hạn").SetStyle(WordExtensions.BoldTextStyle))
                                {
                                    ColumnSpan = 5,
                                    CellFormat = new TableCellFormat
                                    {
                                        BackgroundColor = new Color(194, 214, 155)
                                    }
                                }));
                            }
                            else
                            {
                                dataTable.Rows.Insert(currentIndex,
                            new TableRow(document,
                                new TableCell(document,
                                    new Paragraph(document, "Chưa đến hạn").SetStyle(WordExtensions.BoldTextStyle))
                                {
                                    ColumnSpan = 5,
                                    CellFormat = new TableCellFormat
                                    {
                                        BackgroundColor = new Color(194, 214, 155)
                                    }
                                }));
                            }

                            currentIndex++;

                            foreach (var item in dangThucHienTrongHan)
                            {
                                AddYkcdRow(ref dataTable, item, currentIndex, stt);

                                currentIndex++;
                                stt++;
                            }
                        }
                    }

                    if (daHoanThanh?.Count > 0)
                    {
                        if (daHoanThanhQuaHan?.Count > 0)
                        {
                            stt = 1;

                            dataTable.Rows.Insert(currentIndex,
                            new TableRow(document,
                                new TableCell(document,
                                    new Paragraph(document, "ĐÃ HOÀN THÀNH").Center()
                                    .SetStyle(WordExtensions.BoldTextStyle))
                                {
                                    RowSpan = daHoanThanh.Count + 1 + (daHoanThanhTrongHan?.Count > 0 ? 1 : 0),
                                    CellFormat = new TableCellFormat
                                    {
                                        TextDirection = TableCellTextDirection.BottomToTop,
                                        BackgroundColor = new Color(194, 214, 155)
                                    }
                                },
                                new TableCell(document,
                                    new Paragraph(document, "Trễ hạn").SetStyle(WordExtensions.BoldTextStyle))
                                {
                                    ColumnSpan = 5,
                                    CellFormat = new TableCellFormat
                                    {
                                        BackgroundColor = new Color(217, 149, 148)
                                    }
                                }));
                            currentIndex++;

                            foreach (var item in daHoanThanhQuaHan)
                            {
                                AddYkcdRow(ref dataTable, item, currentIndex, stt);

                                currentIndex++;
                                stt++;
                            }
                        }

                        if (daHoanThanhTrongHan?.Count > 0)
                        {
                            stt = 1;

                            if (daHoanThanhQuaHan.Count == 0)
                            {
                                dataTable.Rows.Insert(currentIndex,
                            new TableRow(document,
                                new TableCell(document,
                                    new Paragraph(document, "ĐÃ HOÀN THÀNH").Center()
                                    .SetStyle(WordExtensions.BoldTextStyle))
                                {
                                    RowSpan = daHoanThanh.Count + 1,
                                    CellFormat = new TableCellFormat
                                    {
                                        TextDirection = TableCellTextDirection.BottomToTop,
                                        BackgroundColor = new Color(194, 214, 155)
                                    }
                                },
                                new TableCell(document,
                                    new Paragraph(document, "Đúng hạn").SetStyle(WordExtensions.BoldTextStyle))
                                {
                                    ColumnSpan = 5,
                                    CellFormat = new TableCellFormat
                                    {
                                        BackgroundColor = new Color(194, 214, 155)
                                    }
                                }));
                            }
                            else
                            {
                                dataTable.Rows.Insert(currentIndex,
                            new TableRow(document,
                                new TableCell(document,
                                    new Paragraph(document, "Đúng hạn").SetStyle(WordExtensions.BoldTextStyle))
                                {
                                    ColumnSpan = 5,
                                    CellFormat = new TableCellFormat
                                    {
                                        BackgroundColor = new Color(194, 214, 155)
                                    }
                                }));
                            }

                            currentIndex++;

                            foreach (var item in daHoanThanhTrongHan)
                            {
                                AddYkcdRow(ref dataTable, item, currentIndex, stt);

                                currentIndex++;
                                stt++;
                            }
                        }
                    }
                }
            }

            document.Save(Response, "DanhSachYKCD.docx", SaveOptions.DocxDefault);
        }

        protected void AddYkcdRow(ref Table table, Request request, int currentIndex, int stt)
        {
            table.Rows.Insert(currentIndex,
                new TableRow(table.Document,
                    new TableCell(table.Document,
                        new Paragraph(table.Document, stt.ToString()).SetStyle(WordExtensions.DefaultFontSizeStyle).Center()),
                    new TableCell(table.Document,
                        new Paragraph(table.Document, request?.Document?.DocumentCode?.RemoveBreakLineCharacters()).SetStyle(WordExtensions.DefaultFontSizeStyle).Center(),
                        new Paragraph(table.Document, request?.Document?.ReleaseDate?.ToDateString()).SetStyle(WordExtensions.DefaultFontSizeStyle).Center())
                    { CellFormat = new TableCellFormat { VerticalAlignment = VerticalAlignment.Center } },
                    new TableCell(table.Document,
                        new Paragraph(table.Document,
                            new Run(table.Document, request?.RequestContent?.RemoveBreakLineCharacters() + " - "),
                            new Run(table.Document, "Theo dõi: " + request?.Trackers?.FirstOrDefault()?.FullName) { CharacterFormat = new CharacterFormat { FontColor = Color.Red } }).SetStyle(WordExtensions.DefaultFontSizeStyle)),
                    new TableCell(table.Document,
                        new Paragraph(table.Document, request?.RequiredDate.ToDateString()).SetStyle(WordExtensions.DefaultFontSizeStyle))
                    { CellFormat = new TableCellFormat { VerticalAlignment = VerticalAlignment.Center } },
                    new TableCell(table.Document,
                        request?.Reports?.Select(report => $"{report.PerformOnDate.ToDateString()}: {report.ReportContent}").ToList().DisplayList(table.Document).SetStyle(WordExtensions.DefaultFontSizeStyle))
                        ));
        }

        public static Paragraph DisplayList(List<Report> items, DocumentModel document)
        {
            Paragraph result = null;
            List<Inline> inlineItems = new List<Inline>();

            foreach (var item in items)
            {
                inlineItems.Add(
                    new Run(document, item.PerformOnDate.ToDateString() + ": ")
                    {
                        CharacterFormat = new CharacterFormat() { Bold = true }
                    });

                inlineItems.Add(
                    new Run(document, item.ReportContent.RemoveBreakLineCharacters())
                    {
                        CharacterFormat = new CharacterFormat() { Bold = false }
                    });
                inlineItems.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
            }

            result = new Paragraph(document, inlineItems.ToArray());
            return result;
        }
    }
}