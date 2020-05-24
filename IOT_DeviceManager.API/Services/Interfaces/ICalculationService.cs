using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Interfaces;

namespace IOT_DeviceManager.API.Services.Interfaces
{
    public interface ICalculationService
    {
        public double CalculateAverage(IEnumerable<IDeviceMeasurement> measurements, string propertyName);

        public TimeSpan CalculateTotalOnTime(IEnumerable<IDeviceMeasurement> measurements);
    }
}
