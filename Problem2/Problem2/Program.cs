
using Problem2.domain;
using Problem2.domain.discount;
using Problem2.domain.dto;
using Problem2.repository;
using Problem2.service;

IRepository<long, Order> orderRepository = new OrderRepository();
IDiscountPolicy discountPolicy = new ThresholdDiscountPolicy(500, 10);
var orderService = new OrderService(orderRepository, discountPolicy);

var customer1 = new Customer("John Alex");
customer1.Id = 1;

var customer2 = new Customer("Michael Stephan");
customer2.Id = 2;

var customer3 = new Customer("George Andrew");
customer3.Id = 3;

var order1 = new Order(customer1);
order1.AddItem(new Item("Keyboard", 2, 450));
order1.AddItem(new Item("Mouse", 5, 150));
orderService.SaveOrder(order1);

var order2 = new Order(customer2);
order2.AddItem(new Item("Laptop", 1, 4500));
order2.AddItem(new Item("Mouse", 3, 140));
orderService.SaveOrder(order2);

var order3 = new Order(customer3);
order3.AddItem(new Item("HDMI Cable", 5, 50));
order3.AddItem(new Item("Ethernet Cable", 2, 30));
order3.AddItem(new Item("Keyboard", 1, 460));
orderService.SaveOrder(order3);

var order4 = new Order(customer2);
order4.AddItem(new Item("HDMI Cable", 1, 50));
orderService.SaveOrder(order4);

Console.WriteLine("The final price (after discount) for order 1: " + orderService.CalculateOrderFinalPrice(order1));
Console.WriteLine("The final price (after discount) for order 4: " + orderService.CalculateOrderFinalPrice(order4));
Console.WriteLine("The name of the customer who spent the most money on all their orders: " + orderService.GetTopSpendingCustomer());
Console.WriteLine("The top 3 most popular products along with their total quantity sold");

IEnumerable<PopularProduct> popularProducts = orderService.GetMostPopularProducts();
foreach (var product in popularProducts)
{
    Console.WriteLine(product);
}
