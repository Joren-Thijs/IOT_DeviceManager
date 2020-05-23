using System;
using System.Collections.Generic;
using System.Linq;
using IOT_DeviceManager.API.Entity.Interfaces;
using IOT_DeviceManager.API.Entity.ThermostatDevice;
using IOT_DeviceManager.API.Extensions;

namespace IOT_DeviceManager.API.Services
{
    public class CalculationService
    {
        public double CalculateAverage(IEnumerable<IDeviceMeasurement> measurements, string propertyName)
        {
            double average = 0;
            
            foreach (var measurement in measurements)
            {
                var property = measurement.GetType().GetProperty(propertyName.FirstCharToUpper());
                if (property == null)
                {
                    throw new ArgumentException($"{ propertyName.FirstCharToUpper() } does not exist on {measurement.GetType()}");
                }

                var propertyValue = property.GetValue(measurement) 
                                    ?? throw new NullReferenceException(($"{ propertyName.FirstCharToUpper() } has no value"));
                average += (double)propertyValue;
            }
            return average;
        }

        public double CalculateAverageTemp(IEnumerable<ThermostatDeviceMeasurement> measurements)
        {
            double averageTemp = 0.0f;
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
                    TimeSpan diff = measurements.ToArray()[i + 1].TimeStamp - measurements.ToArray()[i].TimeStamp;
                    secondsTurnedOn += diff.TotalSeconds;
                }
            }
            return TimeSpan.FromSeconds(secondsTurnedOn);
        }
    }
}
