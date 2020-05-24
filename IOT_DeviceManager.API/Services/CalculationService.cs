using System;
using System.Collections.Generic;
using System.Linq;
using IOT_DeviceManager.API.Entity.Interfaces;

namespace IOT_DeviceManager.API.Services
{
    public class CalculationService
    {
        public double CalculateAverage(IEnumerable<IDeviceMeasurement> measurements, string propertyName)
        {
            propertyName = propertyName.ToLower();
            return measurements
                .Where(x => x.Values.ToDictionary(k => k.Key.ToLower(), k => k.Value).ContainsKey(propertyName))
                .Select(x =>
                {
                    try
                    {
                        return (double?)Convert.ToDouble(x.Values.ToDictionary(k => k.Key.ToLower(), k => k.Value)[propertyName]);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                })
                .Where(x => x != null)
                .Cast<double>()
                .Average();
        }

        public TimeSpan CalculateTotalOnTime(IEnumerable<IDeviceMeasurement> measurements)
        {
            // Check for at least 2 measurements
            if (measurements.Count() < 2)
            {
                throw new ArgumentException("At least 2 measurements are required to calculate the on time.");
            }

            double secondsTurnedOn = 0;
            for (int i = 0; i < measurements.Count() - 1; i++)
            {
                // Check if thermostat was turned on during timespan
                if (measurements.ToArray()[i].Status.OnStatus && measurements.ToArray()[i + 1].Status.OnStatus)
                {
                    TimeSpan diff = measurements.ToArray()[i + 1].TimeStamp - measurements.ToArray()[i].TimeStamp;
                    secondsTurnedOn += diff.TotalSeconds;
                }
            }
            return TimeSpan.FromSeconds(secondsTurnedOn);
        }
    }
}
