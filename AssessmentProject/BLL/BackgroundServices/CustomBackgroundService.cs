using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AssessmentProject.BLL.BackgroundServices
{
    public class CustomBackgroundService : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CustomBackgroundService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        { 
            while (!stoppingToken.IsCancellationRequested)
            {
                var uri = "https://freegeoip.app/json/";
                var request = new HttpRequestMessage(HttpMethod.Get, uri);
                var client = _httpClientFactory.CreateClient();
                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                var JsonResponse = JsonConvert.DeserializeObject(content);
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
        }
    }
}
