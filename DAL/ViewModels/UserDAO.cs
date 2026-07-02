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
        private readonly FruitShopDbContext _context;

        public UserDAO()
        {
            _context = new FruitShopDbContext();

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
        public void AddUser(User user)
        {
            try
            {
                _context.Add(user);
                _context.SaveChanges();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
        }
        public void UpdateUser(User user)
        {
            try
            {
                _context.Update(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DeleteUser(User user)
        {
            try
            {
                _context.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
       public List<User> getUserbyName(string keyword)
        {
            List<User> list = new List<User>();
            try
            {
                list = _context.Users.Where(u => u.Username.Contains(keyword) || u.Phone.Contains(keyword) || u.FullName.Contains(keyword)).ToList();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return list;
        }
        public bool CheckUserExists(string username , string phone)
        {
            try
            {
                return _context.Users.Any(u => u.Username == username || u.Phone == phone);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
