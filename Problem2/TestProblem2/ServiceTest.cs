using Moq;
using Problem2.domain;
using Problem2.domain.discount;
using Problem2.domain.dto;
using Problem2.repository;
using Problem2.service;

namespace TestProblem2;

public class ServiceTest
{
    [Fact]
    public void CalculateOrderFinalPrice_ShouldReturnInitialAmount_WhenNotEligibleForDiscount()
    {
        var mockOrderRepository = new Mock<IRepository<long, Order>>();
        mockOrderRepository.Setup(x => x.FindOneById(1)).Returns(() =>
        {
            var order = new Order(new Customer("Michael Alex"));

            order.Id = 1;
            order.AddItem(new Item("Marker", 20, 3));
            order.AddItem(new Item("Mouse", 2, 50));

            return order;
        });

        var orderService = new OrderService(mockOrderRepository.Object, new ThresholdDiscountPolicy(500, 10));

        decimal expectedFinalPrice = 160m;

        decimal finalPrice = orderService.CalculateOrderFinalPrice(mockOrderRepository.Object.FindOneById(1));
        
        Assert.Equal(expectedFinalPrice, finalPrice);
    }

    [Fact]
    public void CalculateOrderFinalPrice_ShouldReturnAmountUpdated_WhenEligibleForDiscount()
    {
        var mockOrderRepository = new Mock<IRepository<long, Order>>();
        mockOrderRepository.Setup(x => x.FindOneById(1)).Returns(() =>
        {
            var order = new Order(new Customer("Michael Alex"));

            order.Id = 1;
            order.AddItem(new Item("Marker", 20, 3));
            order.AddItem(new Item("Laptop", 2, 2500));

            return order;
        });

        var orderService = new OrderService(mockOrderRepository.Object, new ThresholdDiscountPolicy(500, 10));

        decimal expectedFinalPrice = 4554m;

        decimal finalPrice = orderService.CalculateOrderFinalPrice(mockOrderRepository.Object.FindOneById(1));
        
        Assert.Equal(expectedFinalPrice, finalPrice);
    }

    [Fact]
    public void GetTopSpendingCustomer_ShouldReturnNameOfTopSpendingCustomer_WhenOnlyOneTopSpendingCustomer()
    {
        var mockOrderRepository = new Mock<IRepository<long, Order>>();
        mockOrderRepository.Setup(x => x.FindAll()).Returns(() =>
        {
            var customer1 = new Customer("Michael Alex");
            var customer2 = new Customer("Stefan George");
            var orders = new List<Order>()
            {
                new Order(customer1),
                new Order(customer2),
                new Order(customer1)
            };

            orders[0].Id = 1;
            orders[1].Id = 2;
            orders[2].Id = 3;

            orders[0].AddItem(new Item("Marker", 20, 3));
            orders[0].AddItem(new Item("Laptop", 2, 2500));

            orders[1].AddItem(new Item("Headphones", 2, 250));
            orders[1].AddItem(new Item("Gaming PC", 1, 5000));

            orders[2].AddItem(new Item("Keyboard", 2, 500));

            return orders;
        });
        
        var orderService = new OrderService(mockOrderRepository.Object, new ThresholdDiscountPolicy(500, 10));

        string expectedName = "Michael Alex";

        string? name = orderService.GetTopSpendingCustomer();

        Assert.NotNull(name);
        Assert.Equal(expectedName, name);
    }

    [Fact]
    public void
        GetTopSpendingCustomer_ShouldReturnSmallestNameOfTopSpendingCustomers_WhenMoreThanOneTopSpendingCustomers()
    {
        var mockOrderRepository = new Mock<IRepository<long, Order>>();
        mockOrderRepository.Setup(x => x.FindAll()).Returns(() =>
        {
            var customer1 = new Customer("Michael Alex");
            customer1.Id = 1;
            
            var customer2 = new Customer("Stefan George");
            customer2.Id = 2;
            
            var customer3 = new Customer("Alice Maria");
            customer3.Id = 3;
            
            var orders = new List<Order>()
            {
                new Order(customer1),
                new Order(customer2),
                new Order(customer1),
                new Order(customer3)
            };

            orders[0].Id = 1;
            orders[1].Id = 2;
            orders[2].Id = 3;
            orders[3].Id = 4;

            orders[0].AddItem(new Item("Marker", 20, 3));
            orders[0].AddItem(new Item("Laptop", 2, 2500));

            orders[1].AddItem(new Item("Headphones", 2, 250));
            orders[1].AddItem(new Item("Gaming PC", 1, 5000));

            orders[2].AddItem(new Item("Keyboard", 2, 500));
            
            orders[3].AddItem(new Item("Marker", 20, 3));
            orders[3].AddItem(new Item("Laptop", 2, 2500));
            orders[3].AddItem(new Item("Keyboard", 2, 500));

            return orders;
        });
        
        var orderService = new OrderService(mockOrderRepository.Object, new ThresholdDiscountPolicy(500, 10));

        string expectedName = "Alice Maria";

        string? name = orderService.GetTopSpendingCustomer();

        Assert.NotNull(name);
        Assert.Equal(expectedName, name);
    }

    [Fact]
    public void GetTopSpendingCustomer_ShouldReturnNull_WhenNoOrders()
    {
        var mockOrderRepository = new Mock<IRepository<long, Order>>();
        mockOrderRepository.Setup(x => x.FindAll()).Returns(() => new List<Order>());
        
        var orderService = new OrderService(mockOrderRepository.Object, new ThresholdDiscountPolicy(500, 10));

        string? name = orderService.GetTopSpendingCustomer();

        Assert.Null(name);
    }

    [Fact]
    public void GetMostPopularProducts_ReturnsTopThreeProducts_WhenMoreThanThreeExist()
    {
        var mockOrderRepository = new Mock<IRepository<long, Order>>();
        mockOrderRepository.Setup(x => x.FindAll()).Returns(() =>
        {
            var customer1 = new Customer("Michael Alex");
            customer1.Id = 1;
            
            var customer2 = new Customer("Stefan George");
            customer2.Id = 2;
            
            var customer3 = new Customer("Alice Maria");
            customer3.Id = 3;
            
            var orders = new List<Order>()
            {
                new Order(customer1),
                new Order(customer2),
                new Order(customer1),
                new Order(customer3)
            };

            orders[0].Id = 1;
            orders[1].Id = 2;
            orders[2].Id = 3;
            orders[3].Id = 4;

            orders[0].AddItem(new Item("Marker", 20, 3));
            orders[0].AddItem(new Item("Laptop", 2, 2500));

            orders[1].AddItem(new Item("Headphones", 2, 250));
            orders[1].AddItem(new Item("Gaming PC", 1, 5000));

            orders[2].AddItem(new Item("Keyboard", 2, 500));
            
            orders[3].AddItem(new Item("Marker", 20, 3));
            orders[3].AddItem(new Item("Laptop", 2, 2500));
            orders[3].AddItem(new Item("Keyboard", 2, 500));

            return orders;
        });
        
        var orderService = new OrderService(mockOrderRepository.Object, new ThresholdDiscountPolicy(500, 10));

        List<PopularProduct> popularProducts = orderService.GetMostPopularProducts().ToList();
        
        Assert.Equal(3, popularProducts.Count());
        
        Assert.Equal("Marker", popularProducts[0].ProductName);
        Assert.Equal(40, popularProducts[0].QuantitySold);
        
        Assert.Equal("Keyboard", popularProducts[1].ProductName);
        Assert.Equal(4, popularProducts[1].QuantitySold);
        
        Assert.Equal("Laptop", popularProducts[2].ProductName);
        Assert.Equal(4, popularProducts[2].QuantitySold);
    }

    [Fact]
    public void GetMostPopularProducts_ReturnsAllProducts_WhenFewerThanThreeExist()
    {
        var mockOrderRepository = new Mock<IRepository<long, Order>>();
        mockOrderRepository.Setup(x => x.FindAll()).Returns(() =>
        {
            var customer1 = new Customer("Michael Alex");
            customer1.Id = 1;
            var orders = new List<Order>()
            {
                new Order(customer1)
            };

            orders[0].Id = 1;
            
            orders[0].AddItem(new Item("Marker", 20, 3));
            orders[0].AddItem(new Item("Laptop", 2, 2500));

            return orders;
        });
        
        var orderService = new OrderService(mockOrderRepository.Object, new ThresholdDiscountPolicy(500, 10));

        List<PopularProduct> popularProducts = orderService.GetMostPopularProducts().ToList();
        
        Assert.Equal(2, popularProducts.Count());
        
        Assert.Equal("Marker", popularProducts[0].ProductName);
        Assert.Equal(20, popularProducts[0].QuantitySold);
        
        Assert.Equal("Laptop", popularProducts[1].ProductName);
        Assert.Equal(2, popularProducts[1].QuantitySold);
    }
}