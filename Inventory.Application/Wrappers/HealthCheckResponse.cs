using System;
using System.Collections.Generic;

namespace Inventory.Application.Wrappers
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }
        public IEnumerable<HealthCheck> Checks {get;set;}
        public TimeSpan Duration { get; set; }
    }
}
