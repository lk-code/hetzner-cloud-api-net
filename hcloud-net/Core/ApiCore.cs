using lkcode.hetznercloudapi.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Core
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
        private static string _clientUserAgent { get; set; } = "HetznerCloudApi .NET Library";
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
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static async Task<string> SendPostRequest(string action, Dictionary<string, object> arguments = null)
        {
            checkApiToken();

            StringContent argumentsContent = null;

            if (arguments != null &&
                arguments.Count > 0)
            {
                string argumentsJsonContent = JsonConvert.SerializeObject(arguments);
                argumentsContent = new StringContent(argumentsJsonContent, Encoding.UTF8, "application/json");
            }

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ApiCore.ClientUserAgent);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiCore.ApiToken);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ApiCore.ApiServer + action);
            request.Content = argumentsContent;

            HttpResponseMessage httpResponse = await client.SendAsync(request);
            string response = await httpResponse.Content.ReadAsStringAsync();

            checkResponseContent(response);

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> SendDeleteRequest(string action, Dictionary<string, string> arguments = null)
        {
            checkApiToken();

            StringContent argumentsContent = null;

            if (arguments != null &&
                arguments.Count > 0)
            {
                string argumentsJsonContent = JsonConvert.SerializeObject(arguments);
                argumentsContent = new StringContent(argumentsJsonContent, Encoding.UTF8, "application/json");
            }

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ApiCore.ClientUserAgent);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiCore.ApiToken);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, ApiCore.ApiServer + action);
            request.Content = argumentsContent;

            HttpResponseMessage httpResponse = await client.SendAsync(request);
            string response = await httpResponse.Content.ReadAsStringAsync();

            checkResponseContent(response);

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<string> SendPutRequest(string action, Dictionary<string, string> arguments = null)
        {
            checkApiToken();

            StringContent argumentsContent = null;

            if (arguments != null &&
                arguments.Count > 0)
            {
                string argumentsJsonContent = JsonConvert.SerializeObject(arguments);
                argumentsContent = new StringContent(argumentsJsonContent, Encoding.UTF8, "application/json");
            }

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ApiCore.ClientUserAgent);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiCore.ApiToken);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, ApiCore.ApiServer + action);
            request.Content = argumentsContent;

            HttpResponseMessage httpResponse = await client.SendAsync(request);
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
