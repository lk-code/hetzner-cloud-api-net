using CloudApiNet.Core;
using CloudApiNet.Objects.NetworkObjects;
using CloudApiNet.Objects.ServerObjects.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudApiNet.Api
{
    public class Server
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Network Network { get; set; }

        #region # static methods #

        public static async Task<List<Server>> GetAsync()
        {
            List<Server> serverList = new List<Server>();
            
            string responseContent = await ApiCore.SendRequest("/servers");
            GetAllResponse response = GetAllResponse.FromJson(responseContent);

            foreach (ResponseServer responseServer in response.Servers)
            {
                Server server = new Server();

                server.Id = responseServer.Id;
                server.Name = responseServer.Name;
                server.Status = responseServer.Status;
                server.Created = responseServer.Created;
                server.Network = new Network()
                {
                    Ipv4 = new AddressIpv4()
                    {
                        Ip = responseServer.PublicNet.Ipv4.Ip,
                        Blocked = responseServer.PublicNet.Ipv4.Blocked
                    },
                    Ipv6 = new AddressIpv6()
                    {
                        Ip = responseServer.PublicNet.Ipv6.Ip,
                        Blocked = responseServer.PublicNet.Ipv6.Blocked
                    }
                };

                serverList.Add(server);
            }

            return serverList;
        }

        #endregion

        #region # class methods #

        public async void Shutdown()
        {
            string responseContent = await ApiCore.SendRequest(string.Format("/servers/{0}/actions/shutdown", this.Id));
            GetAllResponse response = GetAllResponse.FromJson(responseContent);
        }

        #endregion
    }
}