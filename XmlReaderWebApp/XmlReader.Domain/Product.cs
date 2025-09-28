namespace XmlReader.Domain
{
    public class Product
    {
        public int Total { get; set; }
        public decimal SalesTax { get; set; }
        public decimal TotalExcludingTax { get; set; }
        public string CostCentre { get; set; } = string.Empty;
    }
}
