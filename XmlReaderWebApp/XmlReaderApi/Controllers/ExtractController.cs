using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using XmlReader.BLL.Contract;
using XmlReader.Domain;
using XmlReaderApi.Model;

namespace XmlReaderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtractController(ITaxService taxService, IConfiguration configuration) : ControllerBase
    {
        private ITaxService _taxService = taxService;
        private IConfiguration _configuration = configuration;

        [HttpPost]
        public Product Post(XmlData model)
        {
            model.PostData = HttpUtility.UrlDecode(model.PostData);
            var taxRate = Convert.ToInt16(_configuration.GetSection("Constants.SalesTax").Value ??= "10");
            return _taxService.CalculateTax(model.PostData, taxRate);
        }
    }
}
