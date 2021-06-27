using AssessmentProject.BLL.BackgroundServices;
using AssessmentProject.BLL.Services;
using AssessmentProject.BLL.ViewModels;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssessmentProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeolocationController : ControllerBase
    {
        public GeolocationService _geolocationService;
        private readonly IBackgroundJobClient _backgroundJobs;

        public GeolocationController(GeolocationService geolocationService,IBackgroundJobClient backgroundJobs)
        {
            _geolocationService = geolocationService;
            _backgroundJobs = backgroundJobs;
        }

        [HttpGet("Info/")]
        public async Task<ActionResult<GeolocationViewModel>> GetGeolocationAsync()
        {
            return await _geolocationService.GetGeolocationInfo();
        }

        [HttpGet("Info/{parameter}")]
        public async Task<ActionResult<GeolocationViewModel>> GetGeolocationAsync(string parameter)
        {
            return await _geolocationService.GetGeolocationInfo(parameter);
        }

        [HttpPost("IP")]
        public  ActionResult PostListOfIPsAsync(InputModel input)
        {
            var jobId = _backgroundJobs.Schedule(() => _geolocationService.GetGeolocationInfoForMultipleIP(input.list),TimeSpan.FromSeconds(60));

            var returnUrl = @"api/Geolocation/Progress/" + jobId.ToString();

            return Ok(returnUrl);
        }

        [HttpGet("Progress/{jobId:int}")]
        public string GetProgress(int jobId)
        {
            IStorageConnection connection = JobStorage.Current.GetConnection();
            JobData jobData = connection.GetJobData(jobId.ToString());

            string stateName = jobData.State;

            return stateName;
        }
    }
    public class InputModel
    {
        public List<string> list { get; set; }
    }
}
