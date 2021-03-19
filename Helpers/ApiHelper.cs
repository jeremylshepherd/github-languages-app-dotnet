using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GithubLanguagesApp.Helpers
{
    public static class ApiHelper
    {
        private static string _githubApiKey = Startup.StaticConfiguration["Github:APIKey"];

        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.github.com/users/")
            };
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("dotNet", "3.1"));
            ApiClient.DefaultRequestHeaders.Add("token", $"token {_githubApiKey}");
        }
    }
}
