using AssessmentProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentProject.BLL.ViewModels
{
    public class GeolocationViewModel
    {
        public string ip { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string time_zone { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public GeolocationViewModel() { }
        public GeolocationViewModel(Geolocation geolocation)
        {
            this.ip = geolocation.ip;
            this.country_code = geolocation.country_code;
            this.country_name = geolocation.country_name;
            this.time_zone = geolocation.time_zone;
            this.latitude = geolocation.latitude;
            this.longitude = geolocation.longitude;
        }
    }
}
