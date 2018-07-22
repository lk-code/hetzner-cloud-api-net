using System;
using System.Collections.Generic;
using System.Windows;

namespace demo_wpf
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CloudApiNet.Api.Server server = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void GetAllButton_Click(object sender, RoutedEventArgs e)
        {
            this.AddLogMessage("load servers");

            CloudApiNet.Core.ApiCore.ApiToken = ApiConfig.API_TOKEN;
            List<CloudApiNet.Api.Server> serverList = await CloudApiNet.Api.Server.GetAsync();

            this.server = serverList[0];

            this.AddLogMessage(string.Format("loaded {0} servers", serverList.Count));
        }

        private void ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            this.AddLogMessage(string.Format("shutdown server '{0}'", this.server.Name));

            this.server.Shutdown();
        }

        private void AddLogMessage(string message)
        {
            this.Log.Text += message + Environment.NewLine;
        }
    }
}
