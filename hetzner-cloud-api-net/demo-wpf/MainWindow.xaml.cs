using System.Collections.Generic;
using System.Windows;

namespace demo_wpf
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            CloudApiNet.Core.ApiCore.ApiToken = ApiConfig.API_TOKEN;
            List<CloudApiNet.Api.Server> serverList = await CloudApiNet.Api.Server.GetAsync();

            int i = 0;
        }
    }
}
