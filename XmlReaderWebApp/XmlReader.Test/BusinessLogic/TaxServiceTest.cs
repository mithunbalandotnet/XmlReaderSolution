using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlReader.BLL;

namespace XmlReader.Test.BusinessLogic
{
    [TestClass]
    public class TaxServiceTest
    {
        private TaxService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new TaxService();
        }

        [TestMethod]
        public void CalculateTax_ValidXml_ShouldReturnCorrectValues()
        { // Arrange
            string xml = "<product><total>110</total><cost_centre>ABC</cost_centre></product>";
            short taxRate = 10;
            //Act
            var result = _service.CalculateTax(xml, taxRate);
            //Assert
            Assert.AreEqual(110, result.Total);
            Assert.AreEqual("ABC", result.CostCentre);
            Assert.AreEqual(100, result.TotalExcludingTax); // 110 / 1.1 = 100
            Assert.AreEqual(10, result.SalesTax);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CalculateTax_InvalidXml_NoTags_ShouldThrow()
        {
            string xml = "plain text without tags";
            _service.CalculateTax(xml, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CalculateTax_InvalidXml_BrokenClosingTag_ShouldThrow()
        {
            string xml = "<product><total>100</total>"; // no closing </product>
            _service.CalculateTax(xml, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CalculateTax_MissingTotalTag_ShouldThrow()
        {
            string xml = "<product><cost_centre>XYZ</cost_centre></product>";
            _service.CalculateTax(xml, 10);
        }

        [TestMethod]
        public void CalculateTax_NoCostCentre_ShouldDefaultToUnknown()
        {
            string xml = "<product><total>220</total></product>";
            var result = _service.CalculateTax(xml, 10);
            Assert.AreEqual("UNKNOWN", result.CostCentre);
            Assert.AreEqual(200, result.TotalExcludingTax); // 220 / 1.1 = 200
            Assert.AreEqual(20, result.SalesTax);
        }
    }
}
