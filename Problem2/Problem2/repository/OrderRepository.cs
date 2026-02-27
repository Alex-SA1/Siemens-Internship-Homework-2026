using Problem2.domain;

namespace Problem2.repository;

public class OrderRepository : IRepository<long, Order>
{
    private Dictionary<long, Order> orders;

    public OrderRepository() => orders = new Dictionary<long, Order>();

    public Order Save(Order order)
    {
        orders[order.Id] = order;
        return order;
    }

    public IEnumerable<Order> FindAll() => orders.Values;

    public Order? FindOneById(long id)
    {
        if (!orders.ContainsKey(id))
            return null;

        return orders[id];
    }
}