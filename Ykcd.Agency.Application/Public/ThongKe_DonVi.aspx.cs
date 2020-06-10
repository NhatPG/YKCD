using System.Drawing;
using System.Linq;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Framework.Web;
using YKCD.Agency.Application.Helper;
using YKCD.Agency.Business.Components;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Application.Public
{
    public partial class ThongKe_DonVi : ListViewPageBase
    {
        public SoLieuThongKe tongCong = new SoLieuThongKe();

        protected override void SetDefaultValueForListView()
        {
            BaseListView = lvData;
        }

        protected override void GetDataList()
        {
            var ketQua = RequestServices.ThongKeDonViTheoNhom(Parameters.Id);

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

            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar, Height = 700 })
                .SetTitle(new Title { Text = "" })
                .SetXAxis(new XAxis { Categories = ketQua.Select(ag => ag.ObjectName).ToArray() })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Title = new YAxisTitle { Text = "Số lượng ý kiến chỉ đạo" }
                })
                .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +''; }" })
                .SetPlotOptions(new PlotOptions { Bar = new PlotOptionsBar { Stacking = Stackings.Normal } })
                .SetSeries(new[]
                {
                    new Series { Name = "Trong hạn", Data = new Data(ketQua.Select(ag => ag.NotPerformInTerm + ag.PerformingInTerm).Cast<object>().ToArray()), Color = Color.DarkOrange},
                    new Series { Name = "Quá hạn", Data = new Data(ketQua.Select(ag => ag.NotPerformOutTerm + ag.PerformingOutTerm).Cast<object>().ToArray()), Color = ColorTranslator.FromHtml("#b22222")}
                });

            ltrChart.Text = chart.ToHtmlString();
        }
    }
}