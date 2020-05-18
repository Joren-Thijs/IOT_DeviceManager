using IOT_Thermostat.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IOT_Thermostat.API.Services
{
    public class CalculationService
    {
        public float CalculateAverageTemp(IEnumerable<DeviceMeasurement> measurements)
        {
            float averageTemp = 0.0f;
            // Sum the temperatures
            foreach (var measurement in measurements)
            {
                averageTemp += measurement.Temperature;
            }
            // Devide by the amount
            averageTemp /= measurements.Count();

            return averageTemp;
        }

        public TimeSpan CalculateTotalOnTime(IEnumerable<DeviceMeasurement> measurements)
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
                if (measurements.ToArray()[i].Status.On && measurements.ToArray()[i + 1].Status.On)
                {
                    TimeSpan diff = measurements.ToArray()[i+1].TimeStamp - measurements.ToArray()[i].TimeStamp;
                    secondsTurnedOn += diff.TotalSeconds;
                }
            }
            return TimeSpan.FromSeconds(secondsTurnedOn);
        }
    }
}
