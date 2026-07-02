using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class OrderDAO
    {
        

        public OrderDAO()
        {
           
        }
        public List<Order> getorder()
        {
            List<Order> list = new List<Order>();
            try
            {   using FruitShopDbContext context = new FruitShopDbContext();
                // Lấy kèm thông tin Khách hàng, Chi tiết đơn hàng và Sản phẩm, Người bán, Người giao hàng
                list = context.Orders
                               .Include(o => o.Customer)
                               .Include(o => o.Saler)
                               .Include(o => o.DeliveryWorker)
                               .Include(o => o.OrderDetails)
                               .ThenInclude(od => od.Product)
                               .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return list;
        }
        public void AddOrder(Order order)
        {
            try
            {
                using FruitShopDbContext context = new FruitShopDbContext();
                context.Orders.Add(order);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                using FruitShopDbContext context = new FruitShopDbContext();
                context.Orders.Update(order);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteOrder(Order order)
        {
            try
            {
                using FruitShopDbContext context = new FruitShopDbContext();
                context.Orders.Remove(order);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
