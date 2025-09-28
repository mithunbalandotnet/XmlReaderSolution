using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlReader.Domain;

namespace XmlReader.BLL.Contract
{
    public interface ITaxService
    {
        Product CalculateTax(string XmlData, short taxRate);
    }
}
