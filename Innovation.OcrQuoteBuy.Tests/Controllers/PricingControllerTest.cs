using System;
using Innovation.OcrQuoteBuy.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovation.OcrQuoteBuy.Tests.Controllers
{
    [TestClass]
    public class PricingControllerTest
    {
        [TestMethod]
        [DeploymentItem("Datasource.xml")]
        public void Test_GetById_Returns_correct_pricing_values()
        {
            var target = new PricingController();
            var actual = target.Get("35931061-8227-4B86-80C6-572AD9376911");
            Assert.AreEqual(100, actual.Option1);
            Assert.AreEqual(150, actual.Option2);
            Assert.AreEqual(250, actual.Option3);
        }
    }
}
