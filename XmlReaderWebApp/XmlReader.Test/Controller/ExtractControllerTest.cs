using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using XmlReader.BLL.Contract;
using XmlReader.Domain;
using XmlReaderApi.Controllers;
using XmlReaderApi.Model;

namespace XmlReader.Test.Controller
{
    [TestClass]
    public class ExtractControllerTest
    {
        private Mock<ITaxService> _mockTaxService;
        private IConfiguration _configuration;
        private ExtractController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockTaxService = new Mock<ITaxService>();

            // fake config: Constants.SalesTax = 15
            var inMemorySettings = new Dictionary<string, string>
            {
                { "Constants.SalesTax", "15" }
            };
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _controller = new ExtractController(_mockTaxService.Object, _configuration);
        }

        [TestMethod]
        public void Post_ValidXml_ReturnsProduct()
        {
            // Arrange
            var xml = HttpUtility.UrlEncode("<product><total>200</total><cost_centre>ABC</cost_centre></product>");
            var expectedProduct = new Product { Total = 200, CostCentre = "ABC" };

            _mockTaxService.Setup(s => s.CalculateTax(HttpUtility.UrlDecode(xml), 15))
                           .Returns(expectedProduct);

            var model = new XmlData { PostData = xml };

            // Act
            var result = _controller.Post(model);

            // Assert
            Assert.AreEqual(200, result.Total);
            Assert.AreEqual("ABC", result.CostCentre);
        }

        [TestMethod]
        public void Post_InvalidXml_ThrowsException()
        {
            var model = new XmlData { PostData = "invalid" };
            _mockTaxService.Setup(s => s.CalculateTax("invalid", 15))
                           .Throws(new System.Exception("Invalid XML"));

            Assert.ThrowsException<System.Exception>(() => _controller.Post(model));
        }
    }
}
