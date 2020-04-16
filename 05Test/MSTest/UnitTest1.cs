using CoreWebApi4Docker.Controllers;
using Domain.Interface;
using Entity;
using Framework.DB.Infrastructure;
using Framework.DB.Utility.Helper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MSTest
{
    [TestClass]
    public class UnitTest1
    {
        WeatherForecastController controller;
        Mock<IRepository<WeatherForecast>> moqRep;
        Mock<ILogger<WeatherForecastController>> logger;
        Mock<IWeatherForecastService> service;
        public UnitTest1()
        {
            logger = new Mock<ILogger<WeatherForecastController>>();
            moqRep = new Mock<IRepository<WeatherForecast>>();
            controller = new WeatherForecastController(logger.Object, service.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var id = IdHelper.Instance.LongId;
            Assert.IsTrue(id > 0);
        }

        [TestMethod]
        public void ControllerTest()
        {
            var res = controller.Get(null);
            logger.Object.LogError("dddddd");
            var res2 = moqRep.Object.Get(1);
            Assert.IsNotNull(res);
        }
    }
}
