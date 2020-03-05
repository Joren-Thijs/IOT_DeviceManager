using IOT_Thermostat.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_Thermostat.API.Services
{
    public class CalculationService
    {
        public float CalculateAverageTemp(IEnumerable<Measurement> measurements)
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

        public TimeSpan CalculateTotalOnTime(IEnumerable<Measurement> measurements)
        {
            double secondsTurnedOn = 0;
            for (int i = 0; i < measurements.Count() - 1; i++)
            {
                // Check if thermostat was turned on during timespan
                if (measurements.ToArray()[i].On && measurements.ToArray()[i + 1].On)
                {
                    TimeSpan diff = measurements.ToArray()[i].TimeStamp - measurements.ToArray()[i + 1].TimeStamp;
                    secondsTurnedOn += diff.TotalSeconds;
                }
            }
            return TimeSpan.FromSeconds(secondsTurnedOn);
        }
    }
}
