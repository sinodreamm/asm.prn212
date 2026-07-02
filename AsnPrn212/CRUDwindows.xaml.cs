using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AsnPrn212
{
    /// <summary>
    /// Interaction logic for CRUDwindows.xaml
    /// </summary>
    public partial class CRUDwindows : Window
    {
        private readonly UserDAO userDAO;
        private readonly User? _currentUser;

        public CRUDwindows()
        {
            InitializeComponent();
            try { this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/favicon.ico", UriKind.RelativeOrAbsolute)); } catch { }
        }

        public CRUDwindows(UserDAO dao, User? user = null)
        {
            InitializeComponent();
            userDAO = dao;
            _currentUser = user;
            if (user != null)
            {
                btnAdd.Visibility = Visibility.Collapsed;
                this.DataContext = user;
            }
            else { btnUpdate.Visibility = Visibility.Collapsed; }
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string role = comboRole.Text;
            string fullname = txtFullname.Text;
            
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(fullname))
            { 
                MessageBox.Show("Please Check Correct Input", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); 
            }
            else if (role == "Guest")
            {
                if (userDAO.CheckUserExists(username,phone)) { MessageBox.Show("User still exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); }
                else {
                    User userNew = new User
                    {

                        Role = role,
                        FullName = "Guest User",
                    };
                    userDAO.AddUser(userNew);
                    MessageBox.Show("Add Successfully !", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }

            }
            else
            {
                if (userDAO.CheckUserExists(username, phone)) { MessageBox.Show("User still exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); }
                else
                {
                    User userNew = new User
                    {
                        Username = username,
                        PasswordHash = password,
                        Email = email,
                        Phone = phone,
                        Role = role,
                        FullName = fullname
                    };
                    userDAO.AddUser(userNew);
                    MessageBox.Show("Add Successfully !", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string role = comboRole.Text;
            string fullname = txtFullname.Text;
            
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(fullname))
            { 
                MessageBox.Show("Please Check Correct Input", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); 
            }
           
            else if (_currentUser != null)
            {
                _currentUser.Username = username;
                _currentUser.PasswordHash = password;
                _currentUser.Email = email;
                _currentUser.Phone = phone;
                _currentUser.Role = role;
                _currentUser.FullName = fullname;

                userDAO.UpdateUser(_currentUser);
                MessageBox.Show("Update Successfully !", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
        }
    }
}
