using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IOT_Thermostat.API.Services
{
    public interface IDeviceClient
    {
        Task StartDeviceClientAsync(CancellationToken stoppingToken);
    }
}
