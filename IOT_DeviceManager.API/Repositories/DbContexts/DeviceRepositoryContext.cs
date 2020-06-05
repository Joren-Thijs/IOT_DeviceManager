using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Entity.Device;
using Microsoft.EntityFrameworkCore;

namespace IOT_DeviceManager.API.Repositories.DbContexts
{
    public class DeviceRepositoryContext : DbContext
    {
        public DeviceRepositoryContext(DbContextOptions<DeviceRepositoryContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceMeasurement> Measurements { get; set; }
    }
}
