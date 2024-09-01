using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop
{
    public class OrderService
    {
        private readonly ApplicationContext _context;

        public OrderService(ApplicationContext context)
        {
            _context = context;
        }

        public void AddOrder(DateTime orderDate, List<int> productIds)
        {
            var products = _context.Products.Where(p => productIds.Contains(p.Id)).ToList();
            var order = new Order
            {
                OrderDate = orderDate,
                Products = products
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void RemoveOrder(int orderId)
        {
            var order = _context.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                _context.Products.RemoveRange(order.Products);
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Order with Id {orderId} not found.");
            }
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.Products).ToList();
        }

        public Order? GetOrderById(int orderId)
        {
            return _context.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == orderId);
        }
    }
}
