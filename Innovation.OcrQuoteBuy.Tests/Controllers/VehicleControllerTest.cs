using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Innovation.OcrQuoteBuy.Controllers;

namespace Innovation.OcrQuoteBuy.Tests.Controllers
{
    /// <summary>
    /// Summary description for VehicleControllerTest
    /// </summary>
    [TestClass]
    public class VehicleControllerTest
    {
        public VehicleControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod()]
        [DeploymentItem("Datasource.xml")]
        public void Test_Get_Returns_n_Cars()
        {
            var controller = new VehicleController();
            var actual = controller.Get();
            Assert.AreEqual(5, actual.Count());
        }

        [TestMethod()]
        [DeploymentItem("Datasource.xml")]
        public void Test_Get_Returns_Car_with_given_Id()
        {
            var controller = new VehicleController();
            var actual = controller.Get("CC2D2644-DB27-477C-99AD-A34E6FD3F139");
            Assert.AreEqual("Silver", actual.Colour);
            Assert.AreEqual("Audi", actual.Make);
            Assert.AreEqual("A4", actual.Model);
            Assert.AreEqual("MEOWDI", actual.Registration);
            Assert.AreEqual("1996", actual.Year);


        }
    }
}
