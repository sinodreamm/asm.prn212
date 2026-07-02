using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AsnPrn212
{
    public partial class CRUDorder : Window
    {
        private readonly OrderDAO orderDAO;
        private readonly UserDAO userDAO;
        private readonly Order? _currentOrder;

        public CRUDorder()
        {
            InitializeComponent();
            orderDAO = new OrderDAO();
            userDAO = new UserDAO();
            try { this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/favicon.ico", UriKind.RelativeOrAbsolute)); } catch { }

            LoadUsers();
            
            // Add mode defaults
            btnAdd.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Collapsed;
            dpOrderDate.SelectedDate = DateTime.Now;
        }

        public CRUDorder(OrderDAO oDao, UserDAO uDao, Order order)
        {
            InitializeComponent();
            orderDAO = oDao;
            userDAO = uDao;
            _currentOrder = order;
            try { this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/favicon.ico", UriKind.RelativeOrAbsolute)); } catch { }

            LoadUsers();
            
            if (_currentOrder != null)
            {
                btnAdd.Visibility = Visibility.Collapsed;
                btnUpdate.Visibility = Visibility.Visible;
                
                this.DataContext = _currentOrder;

                var customer = userDAO.getAllUser().FirstOrDefault(u => u.Id == _currentOrder.CustomerId);
                if (customer != null && customer.Role == "Guest")
                {
                    rbGuest.IsChecked = true;
                }
                else
                {
                    rbRegistered.IsChecked = true;
                    // Make sure the customer is loaded into the ComboBox even if not searched
                    if (customer != null)
                    {
                        txtCustomerSearch.Text = customer.Username;
                        cboCustomer.ItemsSource = new List<User> { customer };
                        cboCustomer.SelectedValue = customer.Id;
                    }
                }

                cboSaler.SelectedValue = _currentOrder.SalerId;
                cboDelivery.SelectedValue = _currentOrder.DeliveryWorkerId;
            }
        }

        private void LoadUsers()
        {
            var allUsers = userDAO.getAllUser();
            // Don't load all customers to avoid scrolling forever. We force them to search.
            cboCustomer.ItemsSource = new List<User>(); 
            cboSaler.ItemsSource = allUsers.Where(u => u.Role == "Saler").ToList();
            cboDelivery.ItemsSource = allUsers.Where(u => u.Role == "Delivery").ToList();
        }

        private void btnSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtCustomerSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                cboCustomer.ItemsSource = userDAO.getAllUser().Where(u => u.Role == "Customer").ToList();
            }
            else
            {
                // Use their custom search method
                var results = userDAO.getUserbyName(keyword);
                cboCustomer.ItemsSource = results.Where(u => u.Role == "Customer").ToList();
            }

            if (cboCustomer.Items.Count > 0)
            {
                cboCustomer.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No customer found!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CustomerType_Changed(object sender, RoutedEventArgs e)
        {
            if (cboCustomer != null && panelSearchCustomer != null && panelCustomerResult != null)
            {
                if (rbGuest.IsChecked == true)
                {
                    panelSearchCustomer.Visibility = Visibility.Collapsed;
                    panelCustomerResult.Visibility = Visibility.Collapsed;
                    cboCustomer.SelectedIndex = -1;
                }
                else
                {
                    panelSearchCustomer.Visibility = Visibility.Visible;
                    panelCustomerResult.Visibility = Visibility.Visible;
                }
            }
        }

        //private int GetOrCreateGuestId()
        //{
        //    var allUsers = userDAO.getAllUser();
        //    var guest = allUsers.FirstOrDefault(u => u.Role == "Guest");
        //    if (guest != null) return guest.Id;

        //    User newGuest = new User
        //    {
        //        Username = "guest_" + Guid.NewGuid().ToString().Substring(0, 4),
        //        PasswordHash = "guest",
        //        FullName = "Khách Vãng Lai",
        //        Role = "Guest",
        //        Email = "guest@example.com",
        //        Phone = "0000000000"
        //    };
        //    userDAO.AddUser(newGuest);
        //    return userDAO.getAllUser().First(u => u.Role == "Guest").Id;
        //}

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int customerId;
                if (rbGuest.IsChecked == true)
                {
                    customerId = GetOrCreateGuestId();
                }
                else
                {
                    if (cboCustomer.SelectedValue == null)
                    {
                        MessageBox.Show("Please search and select a registered customer.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    customerId = (int)cboCustomer.SelectedValue;
                }

                Order newOrder = new Order
                {
                    CustomerId = customerId,
                    SalerId = cboSaler.SelectedValue != null ? (int)cboSaler.SelectedValue : null,
                    DeliveryWorkerId = cboDelivery.SelectedValue != null ? (int)cboDelivery.SelectedValue : null,
                    OrderDate = dpOrderDate.SelectedDate ?? DateTime.Now,
                    TotalAmount = decimal.TryParse(txtTotalAmount.Text, out decimal amt) ? amt : 0,
                    PaymentMethod = cboPaymentMethod.Text,
                    Status = cboStatus.Text
                };

                orderDAO.AddOrder(newOrder);
                MessageBox.Show("Add Order Successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentOrder != null)
                {
                    int customerId;
                    if (rbGuest.IsChecked == true)
                    {
                        customerId = GetOrCreateGuestId();
                    }
                    else
                    {
                        if (cboCustomer.SelectedValue == null)
                        {
                            MessageBox.Show("Please search and select a registered customer.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        customerId = (int)cboCustomer.SelectedValue;
                    }

                    _currentOrder.CustomerId = customerId;
                    _currentOrder.SalerId = cboSaler.SelectedValue != null ? (int)cboSaler.SelectedValue : null;
                    _currentOrder.DeliveryWorkerId = cboDelivery.SelectedValue != null ? (int)cboDelivery.SelectedValue : null;
                    _currentOrder.OrderDate = dpOrderDate.SelectedDate ?? DateTime.Now;
                    _currentOrder.TotalAmount = decimal.TryParse(txtTotalAmount.Text, out decimal amt) ? amt : 0;
                    _currentOrder.PaymentMethod = cboPaymentMethod.Text;
                    _currentOrder.Status = cboStatus.Text;

                    orderDAO.UpdateOrder(_currentOrder);
                    MessageBox.Show("Update Order Successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
