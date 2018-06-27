using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests_net_standard.ApiClients
{
    [TestClass]
    public class ServerClientTest
    {
        [TestMethod]
        public async void TestMethod1()
        {
            CloudApiNet.ApiClients.ServerClient serverClient = new CloudApiNet.ApiClients.ServerClient(ApiConfig.API_TOKEN);
            List<CloudApiNet.Objects.ServerObjects.Server> serverList = await serverClient.GetAllAsync();
        }
    }
}
