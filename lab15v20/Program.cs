using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

Console.OutputEncoding = System.Text.Encoding.UTF8;

DateTime today = DateTime.Today;
int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);

var orders = new List<Order>
{
    new(1,  new DateTime(today.Year, today.Month, Math.Min(1, daysInMonth)), 101),
    new(2,  new DateTime(today.Year, today.Month, Math.Min(3, daysInMonth)), 102),
    new(3,  new DateTime(today.Year, today.Month, Math.Min(5, daysInMonth)), 103),
    new(4,  new DateTime(today.Year, today.Month, Math.Min(7, daysInMonth)), 104),
    new(5,  new DateTime(today.Year, today.Month, Math.Min(9, daysInMonth)), 105),
    new(6,  new DateTime(today.Year, today.Month, Math.Min(11, daysInMonth)), 106),
    new(7,  new DateTime(today.Year, today.Month, Math.Min(13, daysInMonth)), 107),
    new(8,  new DateTime(today.Year, today.Month, Math.Min(15, daysInMonth)), 108),
    new(9,  new DateTime(today.Year, today.Month, Math.Min(17, daysInMonth)), 109),
    new(10, new DateTime(today.Year, today.Month, Math.Min(19, daysInMonth)), 110),
    new(11, today.AddMonths(-1), 111),
    new(12, today.AddMonths(-1).AddDays(-5), 112)
};

var orderDetails = new List<OrderDetail>
{
    new(1, 1, 101, 3, 250),
    new(2, 1, 102, 2, 150),
    new(3, 2, 103, 5, 100),
    new(4, 2, 101, 1, 250),
    new(5, 3, 104, 4, 80),
    new(6, 3, 105, 2, 120),
    new(7, 4, 106, 7, 50),
    new(8, 5, 101, 6, 250),
    new(9, 5, 107, 3, 90),
    new(10, 6, 108, 2, 300),
    new(11, 6, 103, 4, 100),
    new(12, 7, 109, 8, 40),
    new(13, 8, 110, 1, 500),
    new(14, 8, 101, 2, 250),
    new(15, 9, 102, 5, 150),
    new(16, 10, 104, 3, 80),
    new(17, 11, 101, 10, 250),
    new(18, 11, 105, 2, 120),
    new(19, 12, 106, 4, 50),
    new(20, 12, 107, 1, 90)
};

Console.WriteLine("ЛАБОРАТОРНА РОБОТА №15");
Console.WriteLine("LINQ: фільтрація, Join, агрегація та продуктивність");
Console.WriteLine("Варіант 20");
Console.WriteLine($"Кількість замовлень: {orders.Count}");
Console.WriteLine($"Кількість деталей замовлень: {orderDetails.Count}");

Console.WriteLine("\nЗАВДАННЯ 1. Замовлення поточного місяця з товаром ProductId = 101");

// Query Syntax
var task1Query = Measure("Query Syntax", () =>
    (from order in orders
     where order.OrderDate.Year == today.Year
        && order.OrderDate.Month == today.Month
        && orderDetails.Any(detail =>
            detail.OrderId == order.Id && detail.ProductId == 101)
     select order).ToList());

foreach (var order in task1Query)
    Console.WriteLine($"Замовлення №{order.Id}, дата: {order.OrderDate:dd.MM.yyyy}");

// Method Syntax
var task1Method = Measure("Method Syntax", () =>
    orders.Where(order =>
        order.OrderDate.Year == today.Year &&
        order.OrderDate.Month == today.Month &&
        orderDetails.Any(detail =>
            detail.OrderId == order.Id && detail.ProductId == 101))
    .ToList());

foreach (var order in task1Method)
    Console.WriteLine($"Замовлення №{order.Id}, дата: {order.OrderDate:dd.MM.yyyy}");

Console.WriteLine("\nЗАВДАННЯ 2. Об'єднання замовлень з їхніми деталями");

// Query Syntax
var task2Query = Measure("Query Syntax", () =>
    (from order in orders
     join detail in orderDetails on order.Id equals detail.OrderId
     select new OrderDetailResult(order.Id, detail.ProductId, detail.Quantity))
    .ToList());

foreach (var item in task2Query)
    Console.WriteLine($"OrderId: {item.OrderId}, ProductId: {item.ProductId}, Quantity: {item.Quantity}");

// Method Syntax
var task2Method = Measure("Method Syntax", () =>
    orders.Join(orderDetails,
        order => order.Id,
        detail => detail.OrderId,
        (order, detail) =>
            new OrderDetailResult(order.Id, detail.ProductId, detail.Quantity))
    .ToList());

foreach (var item in task2Method)
    Console.WriteLine($"OrderId: {item.OrderId}, ProductId: {item.ProductId}, Quantity: {item.Quantity}");

Console.WriteLine("\nЗАВДАННЯ 3. Загальна кількість одиниць кожного товару");

// Query Syntax
var task3Query = Measure("Query Syntax", () =>
    (from detail in orderDetails
     group detail by detail.ProductId into productGroup
     select new ProductSummary(
         productGroup.Key,
         productGroup.Sum(item => item.Quantity)))
    .OrderBy(item => item.ProductId)
    .ToList());

foreach (var item in task3Query)
    Console.WriteLine($"ProductId: {item.ProductId}, загальна кількість: {item.TotalQuantity}");

// Method Syntax
var task3Method = Measure("Method Syntax", () =>
    orderDetails
        .GroupBy(detail => detail.ProductId)
        .Select(group => new ProductSummary(
            group.Key,
            group.Sum(item => item.Quantity)))
        .OrderBy(item => item.ProductId)
        .ToList());

foreach (var item in task3Method)
    Console.WriteLine($"ProductId: {item.ProductId}, загальна кількість: {item.TotalQuantity}");

Console.WriteLine("\nЗАВДАННЯ 4. Замовлення з найбільшою загальною кількістю товарів");

// Query Syntax
var task4Query = Measure("Query Syntax", () =>
    (from order in orders
     let total = (from detail in orderDetails
                  where detail.OrderId == order.Id
                  select detail.Quantity).Sum()
     orderby total descending
     select new OrderSummary(order.Id, total))
    .First());

Console.WriteLine($"Замовлення №{task4Query.OrderId}, загальна кількість: {task4Query.TotalQuantity}");

// Method Syntax
var task4Method = Measure("Method Syntax", () =>
    orders
        .Select(order => new OrderSummary(
            order.Id,
            orderDetails
                .Where(detail => detail.OrderId == order.Id)
                .Sum(detail => detail.Quantity)))
        .OrderByDescending(item => item.TotalQuantity)
        .First());

Console.WriteLine($"Замовлення №{task4Method.OrderId}, загальна кількість: {task4Method.TotalQuantity}");

Console.WriteLine("\nВИСНОВОК");
Console.WriteLine("У роботі використано Where, Any, Join, GroupBy, Sum, OrderByDescending та First.");
Console.WriteLine("Для кожного завдання реалізовано Query Syntax і Method Syntax.");
Console.WriteLine("Час вимірювався за допомогою Stopwatch. Для невеликих тестових колекцій різниця");
Console.WriteLine("між синтаксисами може бути незначною. На час впливають розмір колекцій,");
Console.WriteLine("кількість операцій, вкладені пошуки та необхідність сортування.");

static T Measure<T>(string syntax, Func<T> query)
{
    var stopwatch = Stopwatch.StartNew();
    T result = query();
    stopwatch.Stop();

    Console.WriteLine($"{syntax}: {stopwatch.Elapsed.TotalMilliseconds:F4} мс");
    return result;
}

record Order(int Id, DateTime OrderDate, int CustomerId);
record OrderDetail(int Id, int OrderId, int ProductId, int Quantity, decimal UnitPrice);
record OrderDetailResult(int OrderId, int ProductId, int Quantity);
record ProductSummary(int ProductId, int TotalQuantity);
record OrderSummary(int OrderId, int TotalQuantity);
