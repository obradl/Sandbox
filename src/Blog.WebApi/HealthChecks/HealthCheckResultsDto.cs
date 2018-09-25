using System.Collections.Generic;

namespace Blog.WebApi.HealthChecks
{
    public class HealthCheckResultsDto
    {
        public string OverallStatus { get; set; }
        public List<HealthCheckDto> HealthChecks { get; set; }
    }
    public class HealthCheckDto
    {
        public string Name { get; set; }
        public string Status { get; set; }
    }

}