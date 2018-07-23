using CloudApiNet.Exceptions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CloudApiNet.Core
{
    public static class ApiCore
    {
        /// <summary>
        /// 
        /// </summary>
        private static string _apiToken { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public static string ApiToken
        {
            get
            {
                return ApiCore._apiToken;
            }
            set
            {
                ApiCore._apiToken = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static string _clientUserAgent { get; set; } = "CloudApi .NET Library";
        /// <summary>
        /// 
        /// </summary>
        public static string ClientUserAgent
        {
            get
            {
                return ApiCore._clientUserAgent;
            }
            set
            {
                ApiCore._clientUserAgent = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly string ApiServer = "https://api.hetzner.cloud/v1";

        public static async Task<string> SendRequest(string action)
        {
            if (string.IsNullOrEmpty(ApiCore.ApiToken) ||
                string.IsNullOrWhiteSpace(ApiCore.ApiToken))
            {
                throw new InvalidAccessTokenException("the access token is null. set it like this: CloudApiNet.Core.ApiCore.ApiToken = \"YOUR_ACCESS_TOKEN_HERE\";");
            }

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ApiCore.ClientUserAgent);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiCore.ApiToken);

            string response = await client.GetStringAsync(ApiCore.ApiServer + action);

            return response;
        }

        public static async Task<string> SendPostRequest(string action)
        {
            if (string.IsNullOrEmpty(ApiCore.ApiToken) ||
                string.IsNullOrWhiteSpace(ApiCore.ApiToken))
            {
                throw new InvalidAccessTokenException("the access token is null. set it like this: CloudApiNet.Core.ApiCore.ApiToken = \"YOUR_ACCESS_TOKEN_HERE\";");
            }

            StringContent stringContent = new StringContent("{ \"firstName\": \"John\" }", UnicodeEncoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ApiCore.ClientUserAgent);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiCore.ApiToken);
            
            HttpResponseMessage response = await client.PostAsync(ApiCore.ApiServer + action, stringContent);

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}
