using System;
using Innovation.Entities;
using Innovation.OcrQuoteBuy.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovation.OcrQuoteBuy.Tests.Controllers
{
    [TestClass]
    public class PromoControllerTest
    {
        [TestMethod]
        public void Test_GetById_returns_correct_promotion()
        {
            var promoController = new PromoController();
            Promotion promo = null;
            int count = 0;
            do
            {
                promo = promoController.Get("CC2D2644-DB27-477C-99AD-A34E6FD3F139");
                if (++count > 100000)
                    break;
            } while (promo == null);
            Assert.IsNotNull(promo);
            Assert.AreEqual(Guid.Parse("0B0B8BEF-27D7-4C4D-8236-C2C7EE81371E"), promo.Id);
        }
    }
}
