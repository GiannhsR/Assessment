using AssessmentProject.BLL.BackgroundServices;
using AssessmentProject.BLL.Services;
using AssessmentProject.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssessmentProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeolocationController : ControllerBase
    {
        public GeolocationService _geolocationService;
        public GeolocationController(GeolocationService geolocationService)
        {
            _geolocationService = geolocationService;
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
        public async Task<IActionResult> PostListOfIPsAsync(InputModel input)
        {
            await Task.Run(async () =>
             {
                 foreach (string givenIp in input.list)
                 {
                     await _geolocationService.GetGeolocationInfo(givenIp);
                 }
             });

            return Ok();
        }
    }
    public class InputModel
    {
        public List<string> list { get; set; }
    }
}
