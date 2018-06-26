using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CloudApiNet.Core
{
    public class ApiCore
    {
        /// <summary>
        /// 
        /// </summary>
        private string _apiToken { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string ApiToken
        {
            get
            {
                return this._apiToken;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _clientUserAgent { get; set; } = "CloudApi .NET Library";
        /// <summary>
        /// 
        /// </summary>
        public string ClientUserAgent
        {
            get
            {
                return this._clientUserAgent;
            }
            set
            {
                this._clientUserAgent = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private HttpClient _client { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        public HttpClient Client
        {
            get
            {
                return this._client;
            }
            set
            {
                this._client = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly string ApiServer = "https://api.hetzner.cloud/v1";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiToken"></param>
        public ApiCore(string apiToken)
        {
            this._apiToken = apiToken;
            this._client = new HttpClient();
        }

        public async Task<string> SendRequest(string action)
        {
            this.Client.DefaultRequestHeaders.Accept.Clear();
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            this.Client.DefaultRequestHeaders.Add("User-Agent", this.ClientUserAgent);
            this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.ApiToken);

            string response = await this.Client.GetStringAsync(this.ApiServer + action);

            return response;
        }
    }
}
