using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;
using XmlReaderApi.Model;

namespace XmlReader.Test.IntegrationTest
{
    [TestClass]
    public class ExtractIntegrationTest
    {
        private static HttpClient _client;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var factory = new WebApplicationFactory<XmlReaderApi.Program>(); // Program.cs of MyApp
            _client = factory.CreateClient();
        }

        [TestMethod]
        public async Task Post_ValidXml_ReturnsProduct()
        {
            // Arrange
            var xmlEncoded = HttpUtility.UrlEncode("\"<product><total>110</total><cost_centre>XYZ</cost_centre></product>\"");
            var payload = new { PostData = xmlEncoded };
            var response = await _client.PostAsJsonAsync("/api/extract", payload);

            // Assert
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            StringAssert.Contains(body, "110");
            StringAssert.Contains(body, "XYZ");
        }

        [TestMethod]
        public async Task Post_InvalidXml_ReturnsError()
        {
            var xml = "\"invalid xml\"";
            var payload = new { PostData = xml };
            var response = await _client.PostAsJsonAsync("/api/extract", payload);

            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
