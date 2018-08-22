using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests_net_standard.Api
{
    [TestClass]
    public class ServerTest
    {
        [TestMethod]
        public async void TestGet()
        {
            CloudApiNet.Core.ApiCore.ApiToken = ApiConfig.API_TOKEN;
            List<CloudApiNet.Api.Server> serverList = await CloudApiNet.Api.Server.GetAsync();
        }
    }
}
