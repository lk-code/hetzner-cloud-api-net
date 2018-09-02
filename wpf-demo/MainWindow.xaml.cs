using lkcode.hetznercloudapi.Api;
using lkcode.hetznercloudapi.Helper;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using wpf_demo.Views;

namespace wpf_demo
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

            if (server == null)
            {
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

        private void DatacenterDataGridLastPageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadDatacenterData((lkcode.hetznercloudapi.Api.Datacenter.CurrentPage - 1));
        }

        private void DatacenterDataGridNextPageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadDatacenterData((lkcode.hetznercloudapi.Api.Datacenter.CurrentPage + 1));
        }

        private async void GetOneDatacenterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage("load datacenter");

                string datacenterId = await this.ShowInputAsync(
                    "Datacenter-ID",
                    "enter the datacenter id");

                long id = Convert.ToInt64(datacenterId);

                lkcode.hetznercloudapi.Api.Datacenter datacenter = await lkcode.hetznercloudapi.Api.Datacenter.GetAsync(id);
                List<lkcode.hetznercloudapi.Api.Datacenter> datacenterList = new List<lkcode.hetznercloudapi.Api.Datacenter>();
                datacenterList.Add(datacenter);

                this.DatacenterDataGrid.ItemsSource = null;
                this.DatacenterDataGrid.ItemsSource = datacenterList;

                this.AddLogMessage(string.Format("loaded datacenter with id {0} and name '{1}'", datacenter.Id, datacenter.Name));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private void GetAllLocationsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadLocationData(1);
        }

        private async void GetOneLocationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage("load location");

                string locationId = await this.ShowInputAsync(
                    "Location-ID",
                    "enter the location id");

                long id = Convert.ToInt64(locationId);

                lkcode.hetznercloudapi.Api.Location location = await lkcode.hetznercloudapi.Api.Location.GetAsync(id);
                List<lkcode.hetznercloudapi.Api.Location> locationList = new List<lkcode.hetznercloudapi.Api.Location>();
                locationList.Add(location);

                this.LocationsDataGrid.ItemsSource = null;
                this.LocationsDataGrid.ItemsSource = locationList;

                this.AddLogMessage(string.Format("loaded location with id {0} and name '{1}'", location.Id, location.Name));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private void LocationsDataGridLastPageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadLocationData((lkcode.hetznercloudapi.Api.Location.CurrentPage - 1));
        }

        private void LocationsDataGridNextPageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadLocationData((lkcode.hetznercloudapi.Api.Location.CurrentPage + 1));
        }

        private async void LoadLocationData(int page)
        {
            try
            {
                this.AddLogMessage(string.Format("load locations in page {0}", page));

                List<lkcode.hetznercloudapi.Api.Location> locationsList = await lkcode.hetznercloudapi.Api.Location.GetAsync(page);
                this.LocationsDataGrid.ItemsSource = locationsList;

                this.LocationsDataGridCurrentPageTextBlock.Text = lkcode.hetznercloudapi.Api.Location.CurrentPage.ToString();
                this.LocationsDataGridMaxPageTextBlock.Text = lkcode.hetznercloudapi.Api.Location.MaxPages.ToString();
                this.LocationsDataGridLastPageButton.IsEnabled = true;
                this.LocationsDataGridNextPageButton.IsEnabled = true;
                if (lkcode.hetznercloudapi.Api.Location.CurrentPage == 1)
                {
                    this.LocationsDataGridLastPageButton.IsEnabled = false;
                }
                if (lkcode.hetznercloudapi.Api.Location.CurrentPage == lkcode.hetznercloudapi.Api.Location.MaxPages)
                {
                    this.LocationsDataGridNextPageButton.IsEnabled = false;
                }

                this.AddLogMessage(string.Format("loaded {0} locations", locationsList.Count));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private void GetAllServerTypesButton_Click(object sender, RoutedEventArgs e)
        {
            LoadServerTypesData(1);
        }

        private async void GetOneServerTypeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage("load server-type");

                string serverTypeId = await this.ShowInputAsync(
                    "ServerType-ID",
                    "enter the serverType id");

                long id = Convert.ToInt64(serverTypeId);

                lkcode.hetznercloudapi.Api.ServerType serverType = await lkcode.hetznercloudapi.Api.ServerType.GetAsync(id);
                List<lkcode.hetznercloudapi.Api.ServerType> serverTypeList = new List<lkcode.hetznercloudapi.Api.ServerType>();
                serverTypeList.Add(serverType);

                this.ServerTypesDataGrid.ItemsSource = null;
                this.ServerTypesDataGrid.ItemsSource = serverTypeList;

                this.AddLogMessage(string.Format("loaded server-type with id {0} and name '{1}'", serverType.Id, serverType.Name));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private void ServerTypesDataGridLastPageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadServerTypesData((lkcode.hetznercloudapi.Api.ServerType.CurrentPage - 1));
        }

        private void ServerTypesDataGridNextPageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadServerTypesData((lkcode.hetznercloudapi.Api.ServerType.CurrentPage + 1));
        }

        private async void LoadServerTypesData(int page)
        {
            try
            {
                this.AddLogMessage(string.Format("load server-types in page {0}", page));

                List<lkcode.hetznercloudapi.Api.ServerType> serverTypesList = await lkcode.hetznercloudapi.Api.ServerType.GetAsync(page);
                this.ServerTypesDataGrid.ItemsSource = serverTypesList;

                this.ServerTypesDataGridCurrentPageTextBlock.Text = lkcode.hetznercloudapi.Api.ServerType.CurrentPage.ToString();
                this.ServerTypesDataGridMaxPageTextBlock.Text = lkcode.hetznercloudapi.Api.ServerType.MaxPages.ToString();
                this.ServerTypesDataGridLastPageButton.IsEnabled = true;
                this.ServerTypesDataGridNextPageButton.IsEnabled = true;
                if (lkcode.hetznercloudapi.Api.ServerType.CurrentPage == 1)
                {
                    this.ServerTypesDataGridLastPageButton.IsEnabled = false;
                }
                if (lkcode.hetznercloudapi.Api.ServerType.CurrentPage == lkcode.hetznercloudapi.Api.ServerType.MaxPages)
                {
                    this.ServerTypesDataGridNextPageButton.IsEnabled = false;
                }

                this.AddLogMessage(string.Format("loaded {0} server-types", serverTypesList.Count));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private void GetAllSshKeysButton_Click(object sender, RoutedEventArgs e)
        {
            LoadSshKeyData(1);
        }

        private async void GetOneSshKeyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AddLogMessage("load ssh-key");

                string sshKeyId = await this.ShowInputAsync(
                    "SSH-Key-ID",
                    "enter the SSH-Key id");

                long id = Convert.ToInt64(sshKeyId);

                lkcode.hetznercloudapi.Api.SshKey sshKey = await lkcode.hetznercloudapi.Api.SshKey.GetAsync(id);
                List<lkcode.hetznercloudapi.Api.SshKey> sshKeyList = new List<lkcode.hetznercloudapi.Api.SshKey>();
                sshKeyList.Add(sshKey);

                this.SshKeysDataGrid.ItemsSource = null;
                this.SshKeysDataGrid.ItemsSource = sshKeyList;

                this.AddLogMessage(string.Format("loaded ssh-key with id {0} and name '{1}'", sshKey.Id, sshKey.Name));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private void SshKeysDataGridLastPageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadSshKeyData((lkcode.hetznercloudapi.Api.SshKey.CurrentPage - 1));
        }

        private void SshKeysDataGridNextPageButton_Clicked(object sender, RoutedEventArgs e)
        {
            LoadSshKeyData((lkcode.hetznercloudapi.Api.SshKey.CurrentPage + 1));
        }

        private async void LoadSshKeyData(int page)
        {
            try
            {
                this.AddLogMessage(string.Format("load ssh-keys in page {0}", page));

                List<lkcode.hetznercloudapi.Api.SshKey> serverTypesList = await lkcode.hetznercloudapi.Api.SshKey.GetAsync(page);
                this.SshKeysDataGrid.ItemsSource = serverTypesList;

                this.SshKeysDataGridCurrentPageTextBlock.Text = lkcode.hetznercloudapi.Api.SshKey.CurrentPage.ToString();
                this.SshKeysDataGridMaxPageTextBlock.Text = lkcode.hetznercloudapi.Api.SshKey.MaxPages.ToString();
                this.SshKeysDataGridLastPageButton.IsEnabled = true;
                this.SshKeysDataGridNextPageButton.IsEnabled = true;
                if (lkcode.hetznercloudapi.Api.SshKey.CurrentPage == 1)
                {
                    this.SshKeysDataGridLastPageButton.IsEnabled = false;
                }
                if (lkcode.hetznercloudapi.Api.SshKey.CurrentPage == lkcode.hetznercloudapi.Api.SshKey.MaxPages)
                {
                    this.SshKeysDataGridNextPageButton.IsEnabled = false;
                }

                this.AddLogMessage(string.Format("loaded {0} ssh-keys", serverTypesList.Count));
            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void ServerDeleteContextMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult msgResult = await this.ShowMessageAsync(
                "Delete the Server",
                "Are you sure you want to delete the server?");

            if (msgResult == MessageDialogResult.Affirmative)
            {
                lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

                try
                {
                    this.AddLogMessage(string.Format("delete server '{0}'", server.Name));

                    lkcode.hetznercloudapi.Api.ServerActionResponse actionResponse = await server.Delete();

                    this.AddLogMessage(string.Format("success: deleted server '{0}' - actionId '{1}' - actionId '{2}'", server.Name, actionResponse.Id, actionResponse.Command));
                }
                catch (Exception err)
                {
                    this.AddLogMessage(string.Format("error: {0}", err.Message));
                }
            }
        }

        private async void ServerChangeNameContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            try
            {
                string newServerName = await this.ShowInputAsync(
                    "New Server-Name",
                    "enter the new server-name");

                if (!string.IsNullOrEmpty(newServerName.Trim()) &&
                    !string.IsNullOrWhiteSpace(newServerName.Trim()))
                {
                    this.AddLogMessage(string.Format("change server name '{0}'", server.Name));

                    await server.ChangeName(newServerName);

                    this.AddLogMessage(string.Format("success: changed server name '{0}'", server.Name));
                }
                else
                {
                    this.AddLogMessage(string.Format("empty or invalid server name '{0}'", newServerName));
                }

            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }

        private async void ServerCpuMetricsContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            ServerMetric serverMetric = null;

            try
            {
                this.AddLogMessage(string.Format("get cpu metrics for server '{0}'", server.Name));

                serverMetric = await server.GetMetrics(ServerMetricType.CPU, DateTimeHelper.GetAsIso8601String(DateTime.Now.AddDays(-30)), DateTimeHelper.GetAsIso8601String(DateTime.Now));

                this.AddLogMessage(string.Format("success: get cpu metrics for server '{0}'", server.Name));

            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }

            ServerMetricCpuValuesWindow win = new ServerMetricCpuValuesWindow(serverMetric);

            win.Show();
        }

        private async void ServerDiskMetricsContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            ServerMetric serverMetric = null;

            try
            {
                this.AddLogMessage(string.Format("get disk metrics for server '{0}'", server.Name));

                serverMetric = await server.GetMetrics(ServerMetricType.DISK, DateTimeHelper.GetAsIso8601String(DateTime.Now.AddDays(-30)), DateTimeHelper.GetAsIso8601String(DateTime.Now));

                this.AddLogMessage(string.Format("success: get disk metrics for server '{0}'", server.Name));

            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
            
            ServerMetricDiskValuesWindow win = new ServerMetricDiskValuesWindow(serverMetric);

            win.Show();
        }

        private async void ServerNetworkMetricsContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            ServerMetric serverMetric = null;

            try
            {
                this.AddLogMessage(string.Format("get network metrics for server '{0}'", server.Name));

                serverMetric = await server.GetMetrics(ServerMetricType.NETWORK, DateTimeHelper.GetAsIso8601String(DateTime.Now.AddDays(-30)), DateTimeHelper.GetAsIso8601String(DateTime.Now));

                this.AddLogMessage(string.Format("success: get network metrics for server '{0}'", server.Name));

            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }

            ServerMetricNetworkValuesWindow win = new ServerMetricNetworkValuesWindow(serverMetric);

            win.Show();
        }

        private async void ServerConsoleContextMenu_Click(object sender, RoutedEventArgs e)
        {
            lkcode.hetznercloudapi.Api.Server server = this.ServerDataGrid.SelectedItem as lkcode.hetznercloudapi.Api.Server;

            ServerActionResponse serverActionResponse = null;

            try
            {
                this.AddLogMessage(string.Format("get console for server '{0}'", server.Name));

                serverActionResponse = await server.RequestConsole();

                ServerConsoleData consoleData = (serverActionResponse.AdditionalActionContent as ServerConsoleData);
                string consoleUrl = consoleData.Url;
                string consolePassword = consoleData.Password;

                this.AddLogMessage(string.Format("success: get console for server '{0}'", server.Name));
                this.AddLogMessage(string.Format("note: url is '{0}'", consoleUrl));
                this.AddLogMessage(string.Format("note: password is '{0}'", consolePassword));

            }
            catch (Exception err)
            {
                this.AddLogMessage(string.Format("error: {0}", err.Message));
            }
        }
    }
}