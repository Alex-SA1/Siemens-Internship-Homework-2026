using Problem2.domain;
using Problem2.domain.discount;
using Problem2.domain.dto;
using Problem2.repository;

namespace Problem2.service;

public class OrderService
{
    private readonly IRepository<long, Order> _orderRepository;
    private readonly IDiscountPolicy _discountPolicy;
    private int _orderId = 1;

    public OrderService(IRepository<long, Order> orderRepository, IDiscountPolicy discountPolicy)
    {
        _orderRepository = orderRepository;
        _discountPolicy = discountPolicy;
    }

    /// <summary>
    /// Calculates the final price of an order after aplying the discount (if the order is eligible for discount)
    /// </summary>
    /// <param name="order">an Order object</param>
    /// <returns>the final price of the order</returns>
    public decimal CalculateOrderFinalPrice(Order order)
    {
        return _discountPolicy.Apply(order.TotalValue());
    }

    /// <summary>
    /// Determines the customer who spent the most on orders
    /// If there are more customers with the same maximum amount of money spent, then the one with the smallest
    /// lexicographically name will be returned
    /// </summary>
    /// <returns>the name of the customer who spent the most money or null if no order is registered</returns>
    public string? GetTopSpendingCustomer()
    {
        IEnumerable<Order> orders = _orderRepository.FindAll();
        if (!orders.Any())
            return null;

        var customerName = orders
            .GroupBy(order => order.Customer.Id)
            .Select(group => new
            {
                CustomerId = group.Key,
                CustomerName = group.First().Customer.Name, // all orders in the group will have the same customer
                TotalSpent = group.Sum(order => CalculateOrderFinalPrice(order))
            })
            .OrderByDescending(group => group.TotalSpent)
            .ThenBy(group => group.CustomerName)
            .First()
            .CustomerName;

        return customerName;
    }

    /// <summary>
    /// Determines the top three most popular products 
    /// If two or more products have the same quantity sold, they will be sorted lexicographically by their name
    /// </summary>
    /// <returns>a list containing the name and the total quantity sold for every product from the top three</returns>
    public IEnumerable<PopularProduct> GetMostPopularProducts()
    {
        IEnumerable<Order> orders = _orderRepository.FindAll();

        var products = orders
            .SelectMany(order => order.Items)
            .GroupBy(item => item.ProductName)
            .Select(group => new
            {
                ProductName = group.Key,
                QuantitySold = group.Sum(item => item.Quantity)
            })
            .Select(group => new PopularProduct(group.ProductName, group.QuantitySold))
            .OrderByDescending(product => product.QuantitySold)
            .ThenBy(product => product.ProductName)
            .Take(3);

        return products;
    }
    
    /// <summary>
    /// Sends an order to the repository to be stored and assigns an unique identifier to it
    /// </summary>
    /// <param name="order">the order that has to be saved</param>
    public void SaveOrder(Order order)
    {
        order.Id = _orderId;
        _orderRepository.Save(order);
        _orderId++;
    }
}