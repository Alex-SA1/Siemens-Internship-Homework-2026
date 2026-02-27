using Problem2.domain.discount;

namespace Problem2.domain;

public class Order: Entity<long>
{
    public Customer Customer { get; }
    
    private readonly List<Item> _items = new();
    public IReadOnlyList<Item> Items => _items;

    public Order(Customer customer)
    {
        Customer = customer;
    }

    /// <summary>
    /// Adds an item to the order
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item) => _items.Add(item);

    /// <summary>
    /// Calculates the total value of the order without aplying a discount
    /// </summary>
    /// <returns>the total value of the order</returns>
    public decimal TotalValue() => _items.Sum(item => item.TotalPrice());
}