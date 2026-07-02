using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DAL.Models;
using DAL.ViewModels;

namespace AsnPrn212
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/favicon.ico", UriKind.Absolute));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nguyên nhân không hiện Icon: " + ex.Message, "Lỗi Icon");
            }
        }
        private static UserDAO userDAO = new UserDAO();
        private static OrderDAO orDAO = new OrderDAO() ;

        private void managerOrder_Click(object sender, RoutedEventArgs e)
        {
        }

        private void managerAccount_Click(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender, e);
        }

        private void reportStatic_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgAccount.ItemsSource = userDAO.getAllUser();
            dgOrder.ItemsSource = orDAO.getorder();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender, e);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender, e);
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
           
            CRUDwindows crud = new CRUDwindows(userDAO,null);
            crud.ShowDialog();
            Window_Loaded(null, null);
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            var chossenUser = dgAccount.SelectedItem as User;
            if (chossenUser != null)
            {
                CRUDwindows crud = new CRUDwindows(userDAO, chossenUser);
                crud.ShowDialog();
                Window_Loaded(null, null);
            }
            else
            {
                MessageBox.Show("Please select a user to update.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var chossenUser = dgAccount.SelectedItem as User;
            if (chossenUser != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete user {chossenUser.Username}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    userDAO.DeleteUser(chossenUser);
                    MessageBox.Show("Delete Successfully!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    Window_Loaded(null, null);
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            CRUDorder cRUDorder = new CRUDorder();
            cRUDorder.ShowDialog();
        }
    }
}