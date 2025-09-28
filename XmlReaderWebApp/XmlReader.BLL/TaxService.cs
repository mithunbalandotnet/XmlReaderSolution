using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XmlReader.BLL.Contract;
using XmlReader.Domain;

namespace XmlReader.BLL
{
    public class TaxService : ITaxService
    {
        public Product CalculateTax(string xmlData, short taxRate)
        {
            Product result = new Product();
            string pattern = "<(\\w+)>";
            System.Text.RegularExpressions.MatchCollection regexMatches = System.Text.RegularExpressions.Regex.Matches(xmlData, pattern);
            if(regexMatches.Count == 0)
            {
                throw new Exception("Invalid XML format");
            }
            string firstTag = regexMatches.First().Value;
            string lastTag = "</" + firstTag.Trim('<');
            if(xmlData.IndexOf(lastTag) == -1 || xmlData.IndexOf(lastTag) < xmlData.IndexOf(firstTag))
            {
                throw new Exception("Invalid XML format");
            }
            string xmlOriginal = xmlData.Substring(xmlData.IndexOf(firstTag), xmlData.IndexOf(lastTag) + lastTag.Length - xmlData.IndexOf(firstTag));
            try
            {
                XDocument xDocument = XDocument.Parse(xmlOriginal);
                if (xDocument.Descendants().Any(x => x.Name == "total"))
                {
                    result.Total = int.Parse(xDocument.Descendants("total").First().Value, NumberStyles.AllowThousands);
                    result.TotalExcludingTax = Math.Round(CalculateProductCost(result.Total, taxRate));
                    result.SalesTax = Math.Round(result.Total - result.TotalExcludingTax, 2);
                    result.CostCentre = xDocument.Descendants("cost_centre").Any() ? xDocument.Descendants("cost_centre").First().Value : "UNKNOWN";
                }
                else
                {
                    throw new Exception("Total tag is missing in XML");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private decimal CalculateProductCost(int total, short taxRate)
        {
            return total / (1 + (taxRate * 0.0100M));
        }
    }
}
