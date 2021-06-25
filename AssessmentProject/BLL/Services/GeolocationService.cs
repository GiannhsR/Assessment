using AssessmentProject.DAL.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AssessmentProject.BLL.ViewModels;
using System.Text.RegularExpressions;

namespace AssessmentProject.BLL.Services
{
    public class GeolocationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string urlPrefix = "https://freegeoip.app";
        public GeolocationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GeolocationViewModel> GetGeolocationInfo()
        {
            var uri = string.Concat(urlPrefix, "/json/");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var geolocation = JsonConvert.DeserializeObject<Geolocation>(content);
            GeolocationViewModel geolocationViewModel = new GeolocationViewModel(geolocation);
            return geolocationViewModel;
        }

        public async Task<GeolocationViewModel> GetGeolocationInfo(string ip)
        {
            var uri = ValidateIP(ip);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var geolocation = JsonConvert.DeserializeObject<Geolocation>(content);
            GeolocationViewModel geolocationViewModel = new GeolocationViewModel(geolocation);
            return geolocationViewModel;
        }

        private string ValidateIP(string ip)
        {
            var uri = "";
            string pattern = @"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$";
            Match m = Regex.Match(ip, pattern);
            if (m.Success)
            {
                uri = string.Concat(urlPrefix, "/json/", ip);
            }
            else
            {
                uri = string.Concat(urlPrefix, "/json/");
            }
            return uri;
        }
    }
}
