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
        }
        public static UserDAO userDAO = new UserDAO();
        private void managerOrder_Click(object sender, RoutedEventArgs e)
        {
        }

        private void managerAccount_Click(object sender, RoutedEventArgs e)
        {

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
        }
    }
}