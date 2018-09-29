using lkcode.hetznercloudapi.Api;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;

namespace wpf_demo.Views
{
    /// <summary>
    /// Interaktionslogik für ServerCreateWindow.xaml
    /// </summary>
    public partial class ServerCreateWindow : MetroWindow
    {
        public ServerCreateWindow()
        {
            InitializeComponent();

            this.loadData();
        }

        private async void loadData()
        {
            List<ServerType> serverTypesList = await ServerType.GetAsync(1);
            this.ServerTypesDataGrid.ItemsSource = serverTypesList;

            List<IsoImage> imagesList = await IsoImage.GetAsync(1);
            this.IsoImageDataGrid.ItemsSource = imagesList;
        }

        private async void CreateServerButton_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Server newServer = new Server();
                IsoImage createIsoImage = null;
                bool startAfterCreate = true;

                bool hasError = false;


                // required fields
                // server-name
                if (!string.IsNullOrEmpty(this.ServerNameTextBox.Text) &&
                    !string.IsNullOrWhiteSpace(this.ServerNameTextBox.Text))
                {
                    newServer.Name = this.ServerNameTextBox.Text;
                }
                else
                {
                    hasError = true;
                    await this.ShowMessageAsync("error at server.create", "server-name is empty or invalid");
                    return;
                }

                // server-type
                if (this.ServerTypesDataGrid.SelectedItem != null)
                {
                    ServerType ServerType = (this.ServerTypesDataGrid.SelectedItem as ServerType);
                    newServer.ServerType = ServerType;
                }
                else
                {
                    hasError = true;
                    await this.ShowMessageAsync("error at server.create", "server-type is empty or invalid");
                    return;
                }

                // server-type
                if (this.IsoImageDataGrid.SelectedItem != null)
                {
                    createIsoImage = (this.IsoImageDataGrid.SelectedItem as IsoImage);
                }
                else
                {
                    hasError = true;
                    await this.ShowMessageAsync("error at server.create", "iso-image is empty or invalid");
                    return;
                }

                // optional fields
                if(this.StartAfterCreateToggleSwitch.IsChecked.HasValue)
                {
                    startAfterCreate = this.StartAfterCreateToggleSwitch.IsChecked.Value;
                }

                if(!hasError)
                {
                    ServerActionResponse action = await newServer.SaveAsync(createIsoImage, startAfterCreate);
                    if(action.Error != null)
                    {
                        // error
                        await this.ShowMessageAsync("error at server.create api exception", action.Error.Message);
                    } else
                    {
                        // success
                        string password = (action.AdditionalActionContent as string);
                        await this.ShowMessageAsync("success at server.create", string.Format("server created with id {0} and password '{1}'.", newServer.Id, password));
                    }
                }
                
            }
            catch (Exception err)
            {
                await this.ShowMessageAsync("error at server.create", err.Message);
            }
        }
    }
}