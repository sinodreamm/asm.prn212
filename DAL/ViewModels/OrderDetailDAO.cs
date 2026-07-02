using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    internal class OrderDetailDAO
    {
        private readonly FruitShopDbContext _context;
        public OrderDetailDAO()
        {
            _context = new FruitShopDbContext();
        }
        public void addOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
