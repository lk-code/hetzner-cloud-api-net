using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace demo_wpf
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MainTabControl.Visibility = Visibility.Collapsed;

        }

        /// <summary>
        /// logs a message ni the textbox
        /// </summary>
        /// <param name="message">the message to log</param>
        private void AddLogMessage(string message)
        {
            this.Log.Text += message + Environment.NewLine;
        }

        private void ApiTokenTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            this.MainTabControl.Visibility = Visibility.Collapsed;
            this.ApiTokenLoadButton.IsEnabled = true;
        }

        private void ApiTokenLoadButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.MainTabControl.Visibility = Visibility.Visible;
            this.ApiTokenLoadButton.IsEnabled = false;

            lkcode.hetznercloudapi.Core.ApiCore.ApiToken = this.ApiTokenTextBox.Text;
        }

        private void GetAllButton_Click(object sender, RoutedEventArgs e)
        {
            LoadServerData(1);
        }

        private void ServerDataGridLastPageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadServerData((lkcode.hetznercloudapi.Api.Server.CurrentPage - 1));
        }

        private void ServerDataGridNextPageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadServerData((lkcode.hetznercloudapi.Api.Server.CurrentPage + 1));
        }

        private async void LoadServerData(int page)
        {
            try
            {
                this.AddLogMessage(string.Format("load servers in page {0}", page));

                List<lkcode.hetznercloudapi.Api.Server> serverList = await lkcode.hetznercloudapi.Api.Server.GetAsync();
                this.ServerDataGrid.ItemsSource = serverList;

                this.ServerDataGridCurrentPageTextBlock.Text = lkcode.hetznercloudapi.Api.Server.CurrentPage.ToString();
                this.ServerDataGridMaxPageTextBlock.Text = lkcode.hetznercloudapi.Api.Server.MaxPages.ToString();
                this.ServerDataGridLastPageButton.IsEnabled = true;
                this.ServerDataGridNextPageButton.IsEnabled = true;
                if (lkcode.hetznercloudapi.Api.Server.CurrentPage == 1)
                {
                    this.ServerDataGridLastPageButton.IsEnabled = false;
                }
                if (lkcode.hetznercloudapi.Api.Server.CurrentPage == lkcode.hetznercloudapi.Api.Server.MaxPages)
                {
                    this.ServerDataGridNextPageButton.IsEnabled = false;
                }

                this.AddLogMessage(string.Format("loaded {0} servers", serverList.Count));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void GetOneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage("load server");

                string serverId = await this.ShowInputAsync(
                    "Server-ID",
                    "enter the server id");

                long id = Convert.ToInt64(serverId);

                lkcode.hetznercloudapi.Api.Server server = await lkcode.hetznercloudapi.Api.Server.GetAsync(id);
                List<lkcode.hetznercloudapi.Api.Server> serverList = new List<lkcode.hetznercloudapi.Api.Server>();
                serverList.Add(server);
                
                this.ServerDataGrid.ItemsSource = null;
                this.ServerDataGrid.ItemsSource = serverList;

                this.AddLogMessage(string.Format("loaded server with id {0} and name '{1}'", server.Id, server.Name));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void GetAllServerWithFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage("load servers with filter");

                string filter = await this.ShowInputAsync(
                    "Server-Name",
                    "enter the server name to filter");

                List<lkcode.hetznercloudapi.Api.Server> serverList = await lkcode.hetznercloudapi.Api.Server.GetAsync(filter);

                this.ServerDataGrid.ItemsSource = serverList;

                this.AddLogMessage(string.Format("loaded {0} servers with filter", serverList.Count));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void RebuildFromImageButton_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            try
            {
                this.AddLogMessage(string.Format("rebuild from image for server '{0}'", server.Name));

                lkcode.hetznercloudapi.Api.ServerActionResponse actionResponse = await server.RebuildImage("test-snapshot");

                if (actionResponse.Error != null)
                {
                    this.AddLogMessage(string.Format("error: {0} ({1})", actionResponse.Error.Message, actionResponse.Error.Code));
                }
                else
                {
                    this.AddLogMessage(string.Format("success: rebuild image for server '{0}' - actionId '{1}' - actionId '{2}'", server.Name, actionResponse.Id, actionResponse.Command));
                }
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void GetOneFloatingIpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage("load floating ip");

                string serverId = await this.ShowInputAsync(
                    "FloatingIp-ID",
                    "enter the floating-ip id");

                long id = Convert.ToInt64(serverId);

                lkcode.hetznercloudapi.Api.FloatingIp floatingIp = await lkcode.hetznercloudapi.Api.FloatingIp.GetAsync(id);
                List<lkcode.hetznercloudapi.Api.FloatingIp> floatingIpList = new List<lkcode.hetznercloudapi.Api.FloatingIp>();
                floatingIpList.Add(floatingIp);

                this.FloatingIpDataGrid.ItemsSource = null;
                this.FloatingIpDataGrid.ItemsSource = floatingIpList;

                this.AddLogMessage(string.Format("loaded floating ip with id {0}", floatingIp.Id));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void DeleteFloatingIpButton_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.FloatingIp floatingIp = this.FloatingIpDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.FloatingIp;

            try
            {
                this.AddLogMessage("delete floating ip");

                await floatingIp.DeleteAsync();

                this.AddLogMessage(string.Format("deleted floating ip with id {0}", floatingIp.Id));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void ServerShutdownContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            try
            {
                this.AddLogMessage(string.Format("shutdown server '{0}'", server.Name));

                lkcode.hetznercloudapi.Api.ServerActionResponse actionResponse = await server.Shutdown();

                this.AddLogMessage(string.Format("success: shutdown server '{0}' - actionId '{1}' - actionId '{2}'", server.Name, actionResponse.Id, actionResponse.Command));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void ServerRebootContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            try
            {
                this.AddLogMessage(string.Format("reboot server '{0}'", server.Name));

                lkcode.hetznercloudapi.Api.ServerActionResponse actionResponse = await server.Reboot();

                this.AddLogMessage(string.Format("success: reboot server '{0}' - actionId '{1}' - actionId '{2}'", server.Name, actionResponse.Id, actionResponse.Command));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void ServerPowerOnContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            try
            {
                this.AddLogMessage(string.Format("poweron server '{0}'", server.Name));

                lkcode.hetznercloudapi.Api.ServerActionResponse actionResponse = await server.PowerOn();

                this.AddLogMessage(string.Format("success: poweron server '{0}' - actionId '{1}' - actionId '{2}'", server.Name, actionResponse.Id, actionResponse.Command));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void ServerPowerOffContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;
            
            try
            {
                this.AddLogMessage(string.Format("poweroff server '{0}'", server.Name));

                lkcode.hetznercloudapi.Api.ServerActionResponse actionResponse = await server.PowerOff();

                this.AddLogMessage(string.Format("success: poweroff server '{0}' - actionId '{1}' - actionId '{2}'", server.Name, actionResponse.Id, actionResponse.Command));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void ServerResetContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            try
            {
                this.AddLogMessage(string.Format("reset server '{0}'", server.Name));

                lkcode.hetznercloudapi.Api.ServerActionResponse actionResponse = await server.Reset();

                this.AddLogMessage(string.Format("success: reset server '{0}' - actionId '{1}' - actionId '{2}'", server.Name, actionResponse.Id, actionResponse.Command));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void ServerResetPasswordContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            try
            {
                this.AddLogMessage(string.Format("reset password for server '{0}'", server.Name));

                lkcode.hetznercloudapi.Api.ServerActionResponse actionResponse = await server.ResetPassword();

                string password = (string)actionResponse.AdditionalActionContent;

                this.AddLogMessage(string.Format("success: reset password for server '{0}' - actionId '{1}' - actionId '{2}' - new password '{3}'", server.Name, actionResponse.Id, actionResponse.Command, password));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void ServerCreateImageBackupContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            try
            {
                this.AddLogMessage(string.Format("createimage backup server '{0}'", server.Name));

                string imageName = await this.ShowInputAsync(
                    "Image Name",
                    "enter the image-name");

                lkcode.hetznercloudapi.Api.ServerActionResponse actionResponse = await server.CreateImage(imageName, lkcode.hetznercloudapi.Components.ServerImageType.BACKUP);

                if (actionResponse.Error != null)
                {
                    this.AddLogMessage(string.Format("error: {0} ({1})", actionResponse.Error.Message, actionResponse.Error.Code));
                }
                else
                {
                    this.AddLogMessage(string.Format("success: createimage backup server '{0}' - actionId '{1}' - actionId '{2}'", server.Name, actionResponse.Id, actionResponse.Command));
                }
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void ServerCreateImageSnapshotContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            try
            {
                this.AddLogMessage(string.Format("createimage snapshot server '{0}'", server.Name));

                string imageName = await this.ShowInputAsync(
                    "Image Name",
                    "enter the image-name");

                lkcode.hetznercloudapi.Api.ServerActionResponse actionResponse = await server.CreateImage(imageName, lkcode.hetznercloudapi.Components.ServerImageType.SNAPSHOT);

                if (actionResponse.Error != null)
                {
                    this.AddLogMessage(string.Format("error: {0} ({1})", actionResponse.Error.Message, actionResponse.Error.Code));
                }
                else
                {
                    this.AddLogMessage(string.Format("success: createimage snapshot server '{0}' - actionId '{1}' - actionId '{2}'", server.Name, actionResponse.Id, actionResponse.Command));
                }
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void GetPricingsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage("load pricings");

                lkcode.hetznercloudapi.Api.Pricing pricing = await lkcode.hetznercloudapi.Api.Pricing.GetAsync();
                
                // set values
                this.PricingCurrencyTextBlock.Text = pricing.Currency;
                this.PricingVatTextBlock.Text = Math.Round(Convert.ToDecimal(pricing.VatRate, CultureInfo.InvariantCulture.NumberFormat), 0) + " %";

                this.PricingImageNetTextBlock.Text = Math.Round(Convert.ToDecimal(pricing.Image.PricePerGbMonth.Net, CultureInfo.InvariantCulture.NumberFormat), 2) + " " + pricing.Currency;
                this.PricingImageGrossTextBlock.Text = Math.Round(Convert.ToDecimal(pricing.Image.PricePerGbMonth.Gross, CultureInfo.InvariantCulture.NumberFormat), 2) + " " + pricing.Currency;

                this.PricingFloatingIpNetTextBlock.Text = Math.Round(Convert.ToDecimal(pricing.FloatingIp.PriceMontly.Net, CultureInfo.InvariantCulture.NumberFormat), 2) + " " + pricing.Currency;
                this.PricingFloatingIpGrossTextBlock.Text = Math.Round(Convert.ToDecimal(pricing.FloatingIp.PriceMontly.Gross, CultureInfo.InvariantCulture.NumberFormat), 2) + " " + pricing.Currency;

                this.PricingTrafficNetTextBlock.Text = Math.Round(Convert.ToDecimal(pricing.Traffic.PricePerTb.Net, CultureInfo.InvariantCulture.NumberFormat), 2) + " " + pricing.Currency;
                this.PricingTrafficGrossTextBlock.Text = Math.Round(Convert.ToDecimal(pricing.Traffic.PricePerTb.Gross, CultureInfo.InvariantCulture.NumberFormat), 2) + " " + pricing.Currency;
                
                this.PricingServerBackupPercentageTextBlock.Text = Math.Round(Convert.ToDecimal(pricing.ServerBackup.Percentage, CultureInfo.InvariantCulture.NumberFormat), 0) + " %";

                this.ServerTypePricingDataGrid.ItemsSource = pricing.ServerTypes;

                this.AddLogMessage(string.Format("loaded pricings"));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private void GetAllIsoButton_Click(object sender, RoutedEventArgs e)
        {
            LoadIsoData(1);
        }

        private void IsoDataGridLastPageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadIsoData((lkcode.hetznercloudapi.Api.IsoImage.CurrentPage - 1));
        }

        private void IsoDataGridNextPageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadIsoData((lkcode.hetznercloudapi.Api.IsoImage.CurrentPage + 1));
        }

        private async void LoadIsoData(int page)
        {
            try
            {
                this.AddLogMessage(string.Format("load iso images in page {0}", page));

                List<lkcode.hetznercloudapi.Api.IsoImage> imagesList = await lkcode.hetznercloudapi.Api.IsoImage.GetAsync(page);
                this.IsoDataGrid.ItemsSource = imagesList;

                this.IsoDataGridCurrentPageTextBlock.Text = lkcode.hetznercloudapi.Api.IsoImage.CurrentPage.ToString();
                this.IsoDataGridMaxPageTextBlock.Text = lkcode.hetznercloudapi.Api.IsoImage.MaxPages.ToString();
                this.IsoDataGridLastPageButton.IsEnabled = true;
                this.IsoDataGridNextPageButton.IsEnabled = true;
                if (lkcode.hetznercloudapi.Api.IsoImage.CurrentPage == 1)
                {
                    this.IsoDataGridLastPageButton.IsEnabled = false;
                }
                if (lkcode.hetznercloudapi.Api.IsoImage.CurrentPage == lkcode.hetznercloudapi.Api.IsoImage.MaxPages)
                {
                    this.IsoDataGridNextPageButton.IsEnabled = false;
                }

                this.AddLogMessage(string.Format("loaded {0} iso images", imagesList.Count));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private void ServerDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RefreshFloatingIpDataGridBinding();
        }

        private void RefreshDataGridBindingButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshFloatingIpDataGridBinding();
        }

        private void RefreshFloatingIpDataGridBinding()
        {
            lkcode.hetznercloudapi.Api.Server server = (ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server);

            if (server == null) {
                return;
            }

            this.FloatingIpDataGrid.ItemsSource = null;
            this.FloatingIpDataGrid.ItemsSource = server.Network.FloatingIps;
        }

        private void GetAllDatacenterButton_Click(object sender, RoutedEventArgs e)
        {
            this.LoadDatacenterData(1);
        }

        private async void LoadDatacenterData(int page)
        {
            try
            {
                this.AddLogMessage(string.Format("load datacenter in page {0}", page));

                List<lkcode.hetznercloudapi.Api.Datacenter> datacenterList = await lkcode.hetznercloudapi.Api.Datacenter.GetAsync(page);
                this.DatacenterDataGrid.ItemsSource = datacenterList;
                
                this.DatacenterDataGridCurrentPageTextBlock.Text = lkcode.hetznercloudapi.Api.Datacenter.CurrentPage.ToString();
                this.DatacenterDataGridMaxPageTextBlock.Text = lkcode.hetznercloudapi.Api.Datacenter.MaxPages.ToString();
                this.DatacenterDataGridLastPageButton.IsEnabled = true;
                this.DatacenterDataGridNextPageButton.IsEnabled = true;
                if (lkcode.hetznercloudapi.Api.Datacenter.CurrentPage == 1)
                {
                    this.DatacenterDataGridLastPageButton.IsEnabled = false;
                }
                if (lkcode.hetznercloudapi.Api.Datacenter.CurrentPage == lkcode.hetznercloudapi.Api.Datacenter.MaxPages)
                {
                    this.DatacenterDataGridNextPageButton.IsEnabled = false;
                }

                this.AddLogMessage(string.Format("loaded {0} datacenter", datacenterList.Count));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private void DatacenterDataGridNextPageButton_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void DatacenterDataGridLastPageButton_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}