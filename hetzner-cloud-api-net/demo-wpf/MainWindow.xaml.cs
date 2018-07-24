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

            CloudApiNet.Core.ApiCore.ApiToken = ApiConfig.API_TOKEN;
        }

        /// <summary>
        /// logs a message ni the textbox
        /// </summary>
        /// <param name="message">the message to log</param>
        private void AddLogMessage(string message)
        {
            this.Log.Text += message + Environment.NewLine;
        }

        private async void GetAllButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage("load servers");

                List<CloudApiNet.Api.Server> serverList = await CloudApiNet.Api.Server.GetAsync();

                this.server = serverList[0];

                this.AddLogMessage(string.Format("loaded {0} servers", serverList.Count));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage(string.Format("shutdown server '{0}'", this.server.Name));

                var actionResponse = await this.server.Shutdown();

                this.AddLogMessage(string.Format("success: shutdown server '{0}' - actionId '{1}' - actionId '{2}'", this.server.Name, actionResponse.ActionId, actionResponse.Command));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage(string.Format("reset server '{0}'", this.server.Name));

                var actionResponse = await this.server.Reset();

                this.AddLogMessage(string.Format("success: reset server '{0}' - actionId '{1}' - actionId '{2}'", this.server.Name, actionResponse.ActionId, actionResponse.Command));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void PowerOnButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage(string.Format("poweron server '{0}'", this.server.Name));

                var actionResponse = await this.server.PowerOn();

                this.AddLogMessage(string.Format("success: poweron server '{0}' - actionId '{1}' - actionId '{2}'", this.server.Name, actionResponse.ActionId, actionResponse.Command));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void RebootButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage(string.Format("reboot server '{0}'", this.server.Name));

                var actionResponse = await this.server.Reboot();

                this.AddLogMessage(string.Format("success: reboot server '{0}' - actionId '{1}' - actionId '{2}'", this.server.Name, actionResponse.ActionId, actionResponse.Command));
            } catch(Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }
    }
}
