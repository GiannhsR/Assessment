using AssessmentProject.BLL.Services;
using AssessmentProject.BLL.Services.BackgroundServices;
using AssessmentProject.BLL.ViewModels;

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
        public BackgroundService _backgroundService;
        public GeolocationController(GeolocationService geolocationService,BackgroundService backgroundService)
        {
            _geolocationService = geolocationService;
            _backgroundService = backgroundService;
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
        public IActionResult PostListOfIPsAsync(InputModel input)
        {
            var jobId = _backgroundService.EnqueueJob(input.list);

            return Ok(GetProgress(jobId));
        }

        [HttpGet("Progress/{jobId:int}")]
        public ActionResult<string> GetProgress(int jobId)
        {
            var stateName = _backgroundService.GetJobState(jobId);

            if(stateName == null)
            {
                return NotFound();
            }

            return stateName;
        }
    }
    public class InputModel
    {
        public List<string> list { get; set; }
    }
}
