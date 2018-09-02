using LiveCharts;
using LiveCharts.Wpf;
using lkcode.hetznercloudapi.Api;
using MahApps.Metro.Controls;
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
    /// Interaktionslogik für ServerMetricDiskValuesWindow.xaml
    /// </summary>
    public partial class ServerMetricDiskValuesWindow : MetricBaseWindow
    {
        public ServerMetric ServerMetric { get; set; }

        public SeriesCollection IopsReadSeriesCollection { get; set; }
        public SeriesCollection IopsWriteSeriesCollection { get; set; }
        public SeriesCollection BandwidthReadSeriesCollection { get; set; }
        public SeriesCollection BandwidthWriteSeriesCollection { get; set; }

        public ServerMetricDiskValuesWindow(ServerMetric serverMetric)
        {
            InitializeComponent();

            this.ServerMetric = serverMetric;
            
            IopsReadSeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "IOPS Read",
                    Values = this.GetChartValues(this.ServerMetric.TimeSeries.DiskValues[0].IOPSRead)
                }
            };

            IopsWriteSeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "IOPS Write",
                    Values = this.GetChartValues(this.ServerMetric.TimeSeries.DiskValues[0].IOPSWrite)
                }
            };

            BandwidthReadSeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Bandwidth Read",
                    Values = this.GetChartValues(this.ServerMetric.TimeSeries.DiskValues[0].BandwithRead)
                }
            };

            BandwidthWriteSeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Bandwidth Write",
                    Values = this.GetChartValues(this.ServerMetric.TimeSeries.DiskValues[0].BandwithWrite)
                }
            };

            DataContext = this;
        }
    }
}
