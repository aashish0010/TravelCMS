using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Function
{
    public static class FunctionData
    {
        public static string HttpClient(string json, string url, Dictionary<string, string> dict = null)
        {
            using (var http = new HttpClient())
            {
                var req = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                    Content = new StringContent(json,Encoding.UTF8,"application/json")
                };
                foreach(KeyValuePair <string,string> entry in dict)
                {
                    http.DefaultRequestHeaders.Add(entry.Key,entry.Value);
                }
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (var http_response = http.SendAsync(req))
                {
                    string responseStr = http_response.Result.Content.ReadAsStringAsync().Result;
                    return responseStr;
                }
            }
        }
    }
}
