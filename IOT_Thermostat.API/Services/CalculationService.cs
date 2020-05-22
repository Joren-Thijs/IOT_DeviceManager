using IOT_Thermostat.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using IOT_Thermostat.API.Models.ThermostatDevice;

namespace IOT_Thermostat.API.Services
{
    public class CalculationService
    {
        public float CalculateAverageTemp(IEnumerable<ThermostatDeviceMeasurement> measurements)
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

        public TimeSpan CalculateTotalOnTime(IEnumerable<ThermostatDeviceMeasurement> measurements)
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
                    TimeSpan diff = measurements.ToArray()[i+1].TimeStamp - measurements.ToArray()[i].TimeStamp;
                    secondsTurnedOn += diff.TotalSeconds;
                }
            }
            return TimeSpan.FromSeconds(secondsTurnedOn);
        }
    }
}
