using DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{   
    public class UserDAO
    {
        private FruitShopDbContext _context;

        public UserDAO()
        {
            _context = new FruitShopDbContext();
            _context.Database.EnsureCreated();
        }

        public UserDAO(FruitShopDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public List<User> getAllUser()
            
        {   
            List<User> list = new List<User>();
            try
            {
               list = _context.Users.ToList();

            }catch(SqlException e)
            {
                Console.WriteLine(e);
            }
            return list;
        }
    }
}
