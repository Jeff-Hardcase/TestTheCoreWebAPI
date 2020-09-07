using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace TestTheCoreWebAPI.Models.Repos
{
    public enum HttpVerb
    {
        Get,
        Post,
        Put,
        Delete
    };

    public static class WebServiceRepo
    {
        private static HttpClient httpClient = new HttpClient();

        static WebServiceRepo()
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static string GetJSON(string wsURL)
        {
            var _uri = new Uri(wsURL);
            var jsonData = httpClient.GetAsync(_uri).Result;

            return jsonData.Content.ReadAsStringAsync().Result;
        }

        public static string GetJSONDispose(string wsURL)
        {
            var result = string.Empty;
            var _uri = new Uri(wsURL);

            using (var response = httpClient.GetAsync(_uri))
            {
                var responseMessage = response.Result;

                responseMessage.EnsureSuccessStatusCode();

                result = responseMessage.Content.ReadAsStringAsync().Result;
            }

            return result;
        }

        public static T CallService<T>(string wsURL, HttpVerb httpVerb = HttpVerb.Get, object data = null, NameValueCollection headerData = null)
        {
            T result = default;
            StringContent _content = null;

            var _uri = new Uri(wsURL);
            var _method = VerbMethod(httpVerb);

            if (data != null)
                _content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage() { RequestUri = _uri, Method = _method, Content = _content };

            if (headerData != null)
            {
                for (int i = 0; i < headerData.Count; i++)
                {
                    request.Headers.Add(headerData.GetKey(i), headerData.Get(i));
                }
            }
            
            using (var response = httpClient.SendAsync(request))
            {
                var responseMessage = response.Result;
                responseMessage.EnsureSuccessStatusCode();
                var resultData = responseMessage.Content.ReadAsStringAsync().Result;

                result = JsonConvert.DeserializeObject<T>(resultData);
            }

            return result;
        }

        private static HttpMethod VerbMethod(HttpVerb httpVerb)
        {
            var result = HttpMethod.Get;

            switch (httpVerb)
            {
                case HttpVerb.Get:
                    result = HttpMethod.Get;
                    break;
                case HttpVerb.Post:
                    result = HttpMethod.Post;
                    break;
                case HttpVerb.Put:
                    result = HttpMethod.Put;
                    break;
                case HttpVerb.Delete:
                    result = HttpMethod.Delete;
                    break;
            }

            return result;
        }
    }
}
