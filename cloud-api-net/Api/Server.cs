using lkcode.hetznercloudapi.Core;
using lkcode.hetznercloudapi.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class Server
    {
        #region # static properties #

        private static int _currentPage { get; set; }
        public static int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
            }
        }

        private static int _maxPages { get; set; }
        public static int MaxPages
        {
            get
            {
                return _maxPages;
            }
            set
            {
                _maxPages = value;
            }
        }

        #endregion

        #region # public properties #

        /// <summary>
        /// ID of server.
        /// </summary>
        public long Id { get; set; } = 0;

        /// <summary>
        /// Name of the server (must be unique per project and a valid hostname as per RFC 1123).
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Status of the server.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Point in time when the server was created (in ISO-8601 format) as a System.DateTimeOffset.
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Public network information.
        /// </summary>
        public Network Network { get; set; }

        #endregion

        #region # static methods #

        /// <summary>
        /// Returns all server in a list.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Server>> GetAsync(int page = 1)
        {
            if ((_maxPages > 0 && (page <= 0 || page > _maxPages)))
            {
                throw new InvalidPageException("invalid page number (" + page + "). valid values between 1 and " + _maxPages + "");
            }

            List<Server> serverList = new List<Server>();

            string url = string.Format("/servers");
            if (page > 1)
            {
                url += "?page=" + page.ToString();
            }

            string responseContent = await ApiCore.SendRequest(url);
            Objects.Server.Get.Response response = JsonConvert.DeserializeObject<Objects.Server.Get.Response>(responseContent);

            // load meta
            CurrentPage = response.meta.pagination.page;
            float pagesDValue = ((float)response.meta.pagination.total_entries / (float)response.meta.pagination.per_page);
            MaxPages = (int)Math.Ceiling(pagesDValue);

            foreach (Objects.Server.Universal.Server responseServer in response.servers)
            {
                Server server = GetServerFromResponseData(responseServer);

                serverList.Add(server);
            }

            return serverList;
        }

        /// <summary>
        /// Returns all server filtered by the given filter-value.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<List<Server>> GetAsync(string filter, int page = 1)
        {
            if ((_maxPages > 0 && (page <= 0 || page > _maxPages)))
            {
                throw new InvalidPageException("invalid page number (" + page + "). valid values between 1 and " + _maxPages + "");
            }

            if (string.IsNullOrEmpty(filter) || string.IsNullOrWhiteSpace(filter))
            {
                return await GetAsync();
            }

            List<Server> serverList = new List<Server>();

            string url = string.Format("/servers?name={0}", filter);
            if (page > 1)
            {
                url += "&page=" + page.ToString();
            }

            string responseContent = await ApiCore.SendRequest(url);
            Objects.Server.Get.Response response = JsonConvert.DeserializeObject<Objects.Server.Get.Response>(responseContent);

            // load meta
            CurrentPage = response.meta.pagination.page;
            float pagesDValue = ((float)response.meta.pagination.total_entries / (float)response.meta.pagination.per_page);
            MaxPages = (int)Math.Ceiling(pagesDValue);

            foreach (Objects.Server.Universal.Server responseServer in response.servers)
            {
                Server server = GetServerFromResponseData(responseServer);

                serverList.Add(server);
            }

            return serverList;
        }

        /// <summary>
        /// Returns a server by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Server> GetAsync(long id)
        {
            string responseContent = await ApiCore.SendRequest(string.Format("/servers/{0}", id.ToString()));
            Objects.Server.GetOne.Response response = JsonConvert.DeserializeObject<Objects.Server.GetOne.Response>(responseContent);

            Server server = GetServerFromResponseData(response.server);

            return server;
        }

        #endregion

        #region # public methods #

        /// <summary>
        /// Shuts down a server gracefully by sending an ACPI shutdown request. The server operating system must support ACPI and react to the request, otherwise the server will not shut down.
        /// </summary>
        /// <returns></returns>
        public async Task<ServerActionResponse> Shutdown()
        {
            string responseContent = await ApiCore.SendPostRequest(string.Format("/servers/{0}/actions/shutdown", this.Id));
            Objects.Server.PostShutdown.Response response = JsonConvert.DeserializeObject<Objects.Server.PostShutdown.Response>(responseContent);

            ServerActionResponse actionResponse = GetServerActionFromResponseData(response.action);

            return actionResponse;
        }

        /// <summary>
        /// Starts a server by turning its power on.
        /// </summary>
        /// <returns></returns>
        public async Task<ServerActionResponse> PowerOn()
        {
            string responseContent = await ApiCore.SendPostRequest(string.Format("/servers/{0}/actions/poweron", this.Id));
            Objects.Server.PostPoweron.Response response = JsonConvert.DeserializeObject<Objects.Server.PostPoweron.Response>(responseContent);

            ServerActionResponse actionResponse = GetServerActionFromResponseData(response.action);

            return actionResponse;
        }

        /// <summary>
        /// Cuts power to the server. This forcefully stops it without giving the server operating system time to gracefully stop. May lead to data loss, equivalent to pulling the power cord. Power off should only be used when shutdown does not work.
        /// </summary>
        /// <returns></returns>
        public async Task<ServerActionResponse> PowerOff()
        {
            string responseContent = await ApiCore.SendPostRequest(string.Format("/servers/{0}/actions/poweroff", this.Id));
            Objects.Server.PostPoweroff.Response response = JsonConvert.DeserializeObject<Objects.Server.PostPoweroff.Response>(responseContent);

            ServerActionResponse actionResponse = GetServerActionFromResponseData(response.action);

            return actionResponse;
        }

        /// <summary>
        /// Reboots a server gracefully by sending an ACPI request. The server operating system must support ACPI and react to the request, otherwise the server will not reboot.
        /// </summary>
        /// <returns></returns>
        public async Task<ServerActionResponse> Reboot()
        {
            string responseContent = await ApiCore.SendPostRequest(string.Format("/servers/{0}/actions/reboot", this.Id));
            Objects.Server.PostReboot.Response response = JsonConvert.DeserializeObject<Objects.Server.PostReboot.Response>(responseContent);

            ServerActionResponse actionResponse = GetServerActionFromResponseData(response.action);

            return actionResponse;
        }

        /// <summary>
        /// Cuts power to a server and starts it again. This forcefully stops it without giving the server operating system time to gracefully stop. This may lead to data loss, it’s equivalent to pulling the power cord and plugging it in again. Reset should only be used when reboot does not work.
        /// </summary>
        /// <returns></returns>
        public async Task<ServerActionResponse> Reset()
        {
            string responseContent = await ApiCore.SendPostRequest(string.Format("/servers/{0}/actions/reset", this.Id));
            Objects.Server.PostReset.Response response = JsonConvert.DeserializeObject<Objects.Server.PostReset.Response>(responseContent);

            ServerActionResponse actionResponse = GetServerActionFromResponseData(response.action);

            return actionResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ServerActionResponse> ResetPassword()
        {
            string responseContent = await ApiCore.SendPostRequest(string.Format("/servers/{0}/actions/reset_password", this.Id));
            Objects.Server.ResetPassword.Response response = JsonConvert.DeserializeObject<Objects.Server.ResetPassword.Response>(responseContent);

            ServerActionResponse actionResponse = GetServerActionFromResponseData(response.action);
            actionResponse.AdditionalActionContent = response.root_password;

            return actionResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<ServerActionResponse> CreateImage(string description, string type)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(description.Trim()) &&
                !string.IsNullOrWhiteSpace(description.Trim()))
            {
                arguments.Add("description", description);
            }

            if (!string.IsNullOrEmpty(type.Trim()) &&
                !string.IsNullOrWhiteSpace(type.Trim()))
            {
                arguments.Add("type", type);
            }
            
            string responseContent = await ApiCore.SendPostRequest(string.Format("/servers/{0}/actions/create_image", this.Id), arguments);
            JObject responseObject = JObject.Parse(responseContent);

            if(responseObject["error"] != null)
            {
                // error
                Objects.Server.Universal.ErrorResponse error = JsonConvert.DeserializeObject<Objects.Server.Universal.ErrorResponse>(responseContent);
                ServerActionResponse response = new ServerActionResponse();
                response.Error = GetErrorFromResponseData(error);

                return response;
            } else
            {
                // success
                Objects.Server.PostCreateImage.Response response = JsonConvert.DeserializeObject<Objects.Server.PostCreateImage.Response>(responseContent);

                ServerActionResponse actionResponse = GetServerActionFromResponseData(response.action);
                actionResponse.AdditionalActionContent = GetServerImageFromResponseData(response.image);

                return actionResponse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<ServerActionResponse> RebuildImage(string image)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(image.Trim()) &&
                !string.IsNullOrWhiteSpace(image.Trim()))
            {
                arguments.Add("image", image);
            }

            string responseContent = await ApiCore.SendPostRequest(string.Format("/servers/{0}/actions/rebuild", this.Id), arguments);
            JObject responseObject = JObject.Parse(responseContent);

            if (responseObject["error"] != null)
            {
                // error
                Objects.Server.Universal.ErrorResponse error = JsonConvert.DeserializeObject<Objects.Server.Universal.ErrorResponse>(responseContent);
                ServerActionResponse response = new ServerActionResponse();
                response.Error = GetErrorFromResponseData(error);

                return response;
            }
            else
            {
                // success
                Objects.Server.PostRebuild.Response response = JsonConvert.DeserializeObject<Objects.Server.PostRebuild.Response>(responseContent);

                ServerActionResponse actionResponse = GetServerActionFromResponseData(response.action);
                //actionResponse.AdditionalActionContent = GetServerImageFromResponseData(response.image);

                return actionResponse;
            }
        }

        /// <summary>
        /// Deletes a server. This immediately removes the server from your account, and it is no longer accessible.
        /// </summary>
        /// <returns></returns>
        public async Task<ServerActionResponse> Delete()
        {
            string responseContent = await ApiCore.SendDeleteRequest(string.Format("/servers/{0}", this.Id));
            Objects.Server.Delete.Response response = JsonConvert.DeserializeObject<Objects.Server.Delete.Response>(responseContent);

            ServerActionResponse actionResponse = GetServerActionFromResponseData(response.action);

            return actionResponse;
        }

        /// <summary>
        /// Changes the name of a server.
        /// Please note that server names must be unique per project and valid hostnames as per RFC 1123 (i.e.may only contain letters, digits, periods, and dashes).
        /// </summary>
        /// <returns></returns>
        public async Task ChangeName(string name)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(name.Trim()) &&
                !string.IsNullOrWhiteSpace(name.Trim()))
            {
                arguments.Add("name", name);
            }

            string responseContent = await ApiCore.SendPutRequest(string.Format("/servers/{0}", this.Id), arguments);
            Objects.Server.PutChangeName.Response response = JsonConvert.DeserializeObject<Objects.Server.PutChangeName.Response>(responseContent);

            Server server = GetServerFromResponseData(response.server);

            this.Name = server.Name;
        }

        #endregion

        #region # private methods for processing #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseServer"></param>
        /// <returns></returns>
        private static Server GetServerFromResponseData(Objects.Server.Universal.Server responseData)
        {
            Server server = new Server();

            server.Id = responseData.id;
            server.Name = responseData.name;
            server.Status = responseData.status;
            server.Created = responseData.created;
            server.Network = new Network()
            {
                Ipv4 = new AddressIpv4()
                {
                    Ip = responseData.public_net.ipv4.ip,
                    Blocked = responseData.public_net.ipv4.blocked
                },
                Ipv6 = new AddressIpv6()
                {
                    Ip = responseData.public_net.ipv6.ip,
                    Blocked = responseData.public_net.ipv6.blocked
                },
                FloatingIpIds = responseData.public_net.floating_ips
            };

            return server;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private static ServerActionResponse GetServerActionFromResponseData(Objects.Server.Universal.ServerAction responseData)
        {
            ServerActionResponse serverAction = new ServerActionResponse();

            serverAction.Id = responseData.id;
            serverAction.Command = responseData.command;
            serverAction.Progress = responseData.progress;
            serverAction.Started = responseData.started;
            serverAction.Status = responseData.status;

            return serverAction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private static ServerImage GetServerImageFromResponseData(Objects.Server.PostCreateImage.Image responseData)
        {
            ServerImage serverImage = new ServerImage();

            serverImage.Id = responseData.id;
            serverImage.Type = responseData.type;
            serverImage.Name = responseData.name;
            serverImage.Description = responseData.description;

            return serverImage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorResponse"></param>
        /// <returns></returns>
        private static Error GetErrorFromResponseData(Objects.Server.Universal.ErrorResponse errorResponse)
        {
            Error error = new Error();

            error.Message = errorResponse.error.message;
            error.Code = errorResponse.error.code;

            return error;
        }

        #endregion
    }
}