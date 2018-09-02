using LiveCharts;
using LiveCharts.Wpf;
using lkcode.hetznercloudapi.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

            DataContext = this;
        }
    }
}
