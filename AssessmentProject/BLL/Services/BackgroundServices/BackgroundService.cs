using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Storage;

namespace AssessmentProject.BLL.Services.BackgroundServices
{
    public class BackgroundService
    {
        private readonly IBackgroundJobClient _backgroundJobs;
        public GeolocationService _geolocationService;
        public BackgroundService(IBackgroundJobClient backgroundJobs,GeolocationService geolocationService)
        {
            _backgroundJobs = backgroundJobs;
            _geolocationService = geolocationService;
        }

        public int EnqueueJob(List<string> inputList)
        {
            // var jobId = _backgroundJobs.Schedule(() => _geolocationService.GetGeolocationInfoForMultipleIP(input.list),TimeSpan.FromSeconds(60));
            var jobId = _backgroundJobs.Enqueue(() => _geolocationService.GetGeolocationInfoForMultipleIP(inputList));

            return Int32.Parse(jobId);
        }

        public string GetJobState(int jobId)
        {
            IStorageConnection connection = JobStorage.Current.GetConnection();
            JobData jobData = connection.GetJobData(jobId.ToString());
            string stateName = jobData.State;

            return stateName;
        }
    }
}
