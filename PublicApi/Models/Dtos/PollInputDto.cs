﻿using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PublicAPI.Models.Dtos
{
    public class PollInputDto: SafeSessionDto
    {
        public string ModuleId { get; set; }
        public string CurrentChecksum { get; set; }
        public DateTime CurrentDataTimestamp { get; set; }



    }
}
