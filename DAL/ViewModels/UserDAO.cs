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
        public UserDAO() { }
        public List<User> getAllUser()
            
        {   
            List<User> list = new List<User>();
            using FruitShopDbContext _context = new FruitShopDbContext();
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
