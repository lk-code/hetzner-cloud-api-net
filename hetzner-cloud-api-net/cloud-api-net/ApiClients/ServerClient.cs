using CloudApiNet.Core;
using CloudApiNet.Objects.NetworkObjects;
using CloudApiNet.Objects.ServerObjects;
using CloudApiNet.Objects.ServerObjects.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudApiNet.ApiClients
{
    public class ServerClient : ApiCore
    {
        public ServerClient(string apiToken) : base(apiToken)
        {

        }

        public async Task<List<Server>> GetAllAsync()
        {
            List<Server> serverList = new List<Server>();

            try
            {
                string responseContent = await this.SendRequest("/servers");
                GetAllResponse response = GetAllResponse.FromJson(responseContent);

                foreach(ResponseServer responseServer in response.Servers)
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
            }
            catch (Exception err)
            {
            }

            return serverList;
        }
    }
}