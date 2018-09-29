using LiveCharts;
using LiveCharts.Wpf;
using lkcode.hetznercloudapi.Api;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;

namespace wpf_demo.Views
{
    /// <summary>
    /// Interaktionslogik für ServerMetricNetworkValuesWindow.xaml
    /// </summary>
    public partial class ServerMetricNetworkValuesWindow : MetricBaseWindow
    {
        public ServerMetric ServerMetric { get; set; }

        public SeriesCollection PpsInSeriesCollection { get; set; }
        public SeriesCollection PpsOutSeriesCollection { get; set; }
        public SeriesCollection BandwidthInSeriesCollection { get; set; }
        public SeriesCollection BandwidthOutSeriesCollection { get; set; }

        public ServerMetricNetworkValuesWindow(ServerMetric serverMetric)
        {
            InitializeComponent();

            this.ServerMetric = serverMetric;

            if (this.ServerMetric.TimeSeries.NetworkValues.Count() > 0)
            {
                PpsInSeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "PPS In",
                        Values = this.GetChartValues(this.ServerMetric.TimeSeries.NetworkValues[0].PPSIn)
                    }
                };

                PpsOutSeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "PPS Out",
                        Values = this.GetChartValues(this.ServerMetric.TimeSeries.NetworkValues[0].PPSOut)
                    }
                };

                BandwidthInSeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Bandwidth In",
                        Values = this.GetChartValues(this.ServerMetric.TimeSeries.NetworkValues[0].BandwithIn)
                    }
                };

                BandwidthOutSeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Bandwidth Out",
                        Values = this.GetChartValues(this.ServerMetric.TimeSeries.NetworkValues[0].BandwithOut)
                    }
                };
            } else
            {
                this.ShowMessageAsync("note", "no entries available");
            }

            DataContext = this;
        }
    }
}
