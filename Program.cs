using System;
using System.Linq;
using Shop.Models;

namespace Shop
{
    class Program
    {
        static void Main()
        {
            using (var context = new ApplicationContext())
            {
                context.Database.EnsureCreated();

                var orderService = new OrderService(context);

              
                var productIds = context.Products.Select(p => p.Id).ToList();
                orderService.AddOrder(DateTime.Now, productIds);

             
                var orders = orderService.GetAllOrders();
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order {order.Id} - Date: {order.OrderDate}");
                    foreach (var product in order.Products)
                    {
                        Console.WriteLine($"  Product: {product.Name}, Price: {product.Price}");
                    }
                }

                
                orderService.RemoveOrder(1);
            }
        }
    }
}
