using LiveCharts;
using LiveCharts.Wpf;
using lkcode.hetznercloudapi.Api;
using MahApps.Metro.Controls;
using System.Collections.Generic;

namespace wpf_demo.Views
{
    /// <summary>
    /// Interaktionslogik für ServerMetricCpuValuesWindow.xaml
    /// </summary>
    public partial class ServerMetricCpuValuesWindow : MetricBaseWindow
    {
        public ServerMetric ServerMetric { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public ServerMetricCpuValuesWindow(ServerMetric serverMetric)
        {
            InitializeComponent();

            this.ServerMetric = serverMetric;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "CPU-Values",
                    Values = this.GetChartValues(this.ServerMetric.TimeSeries.CpuValues)
                }
            };

            DataContext = this;
        }
    }
}