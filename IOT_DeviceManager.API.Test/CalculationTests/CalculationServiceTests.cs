using IOT_DeviceManager.API.Services;
using NUnit.Framework;


namespace IOT_DeviceManager.API.Test.CalculationTests
{
    [TestFixture]
    class CalculationServiceTests
    {
        [Test]
        public void CheckCalculationServiceCanBeCreated_ReturnsTrue()
        {
            CalculationService calculationService = new CalculationService();
            Assert.IsNotNull(calculationService);
        }
    }
}
