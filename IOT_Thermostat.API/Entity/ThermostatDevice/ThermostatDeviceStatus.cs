using IOT_Thermostat.API.Entity.Interfaces;

namespace IOT_Thermostat.API.Entity.ThermostatDevice
{
    public class ThermostatDeviceStatus : IDeviceStatus
    {
        public ThermostatDeviceStatus() { }
        public ThermostatDeviceStatus(bool onStatus)
        {
            OnStatus = onStatus;
        }

        public bool OnStatus { get; set; }
    }
}
