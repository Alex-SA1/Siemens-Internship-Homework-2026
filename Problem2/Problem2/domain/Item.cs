namespace Problem2.domain;

public class Item: Entity<long>
{
    public string ProductName { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }

    public Item(string productName, int quantity, decimal unitPrice)
    {
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    /// <summary>
    /// Calculates the total price of the item
    /// </summary>
    /// <returns>the total price</returns>
    public decimal TotalPrice() => UnitPrice * Quantity;
}