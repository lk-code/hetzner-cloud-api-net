using LiveCharts;
using lkcode.hetznercloudapi.Api;
using MahApps.Metro.Controls;
using System.Collections.Generic;

namespace wpf_demo.Views
{
    public class MetricBaseWindow : MetroWindow
    {
        public MetricBaseWindow()
        {

        }

        public ChartValues<double> GetChartValues(List<ServerMetricValue> values)
        {
            ChartValues<double> cpuValues = new ChartValues<double>();

            foreach (ServerMetricValue value in values)
            {
                cpuValues.Add(value.Value);
            }

            return cpuValues;
        }
    }
}
