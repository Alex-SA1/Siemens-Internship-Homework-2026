namespace Problem2.domain.dto;

public class PopularProduct
{
    public string ProductName { get; set; }
    public int QuantitySold { get; set; }

    public PopularProduct(string productName, int quantitySold)
    {
        ProductName = productName;
        QuantitySold = quantitySold;
    }

    public override string ToString()
    {
        return $"Product name: {ProductName} | Total Quantity Sold: {QuantitySold}";
    }
}