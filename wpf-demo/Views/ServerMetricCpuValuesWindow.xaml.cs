using LiveCharts;
using LiveCharts.Wpf;
using lkcode.hetznercloudapi.Api;
using MahApps.Metro.Controls;

namespace wpf_demo.Views
{
    /// <summary>
    /// Interaktionslogik für ServerMetricCpuValuesWindow.xaml
    /// </summary>
    public partial class ServerMetricCpuValuesWindow : MetroWindow
    {
        public ServerMetric ServerMetric { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public ServerMetricCpuValuesWindow(ServerMetric serverMetric)
        {
            InitializeComponent();

            this.ServerMetric = serverMetric;

            ChartValues<double> cpuValues = new ChartValues<double>();

            foreach (ServerMetricCpuValue cpuValue in ServerMetric.TimeSeries.CpuValues)
            {
                cpuValues.Add(cpuValue.Value);
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "CPU-Values",
                    Values = cpuValues
                }
            };

            DataContext = this;
        }
    }
}