using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Device;

namespace IOT_DeviceManager.API.Services.Interfaces
{
    public interface ICalculationService
    {
        public double CalculateAverage(IEnumerable<DeviceMeasurement> measurements, string propertyName);

        public TimeSpan CalculateTotalOnTime(IEnumerable<DeviceMeasurement> measurements);
    }
}
