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

        #region # public request methods #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> SendRequest(string action)
        {
            checkApiToken();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ApiCore.ClientUserAgent);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiCore.ApiToken);

            string response = await client.GetStringAsync(ApiCore.ApiServer + action);

            checkResponseContent(response);

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> SendPostRequest(string action)
        {
            checkApiToken();

            StringContent stringContent = new StringContent("{ \"firstName\": \"John\" }", UnicodeEncoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ApiCore.ClientUserAgent);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiCore.ApiToken);
            
            HttpResponseMessage httpResponse = await client.PostAsync(ApiCore.ApiServer + action, stringContent);

            httpResponse.EnsureSuccessStatusCode();

            string response = await httpResponse.Content.ReadAsStringAsync();

            checkResponseContent(response);

            return response;
        }

        #endregion

        #region # private methods #

        /// <summary>
        /// 
        /// </summary>
        private static void checkApiToken()
        {
            if (string.IsNullOrEmpty(ApiCore.ApiToken) ||
                string.IsNullOrWhiteSpace(ApiCore.ApiToken))
            {
                throw new InvalidAccessTokenException("the access token is null. set it like this: CloudApiNet.Core.ApiCore.ApiToken = \"YOUR_ACCESS_TOKEN_HERE\";");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void checkResponseContent(string response)
        {
            if (string.IsNullOrEmpty(response) ||
                string.IsNullOrWhiteSpace(response))
            {
                throw new InvalidJsonResponseException("the json response from the api is empty. maybe an error?");
            }
        }

        #endregion
    }
}
