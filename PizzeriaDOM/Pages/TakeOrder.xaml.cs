using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PizzeriaDOM.src.classes;
using PizzeriaDOM.src.functions;

namespace PizzeriaDOM.Pages
{
    /// <summary>
    /// Logique d'interaction pour TakeOrder.xaml
    /// </summary>
    public partial class TakeOrder : UserControl
    {
        //La liste de panels à afficher
        public ObservableCollection<ProductPanel> Products { get; set; } = new ObservableCollection<ProductPanel>();
        private int productIdCounter = 1;

        public TakeOrder()
        {
            InitializeComponent();
            Products = new ObservableCollection<ProductPanel>();
            DataContext = this;
        }

        private Customer customer = null;

        public Customer Customer
        {
            get => customer;
            set => customer = value;
        }

        private void takeCall_Click(object sender, RoutedEventArgs e)
        {
            PhoneNumberWindow phoneNumberWindow = new PhoneNumberWindow();
            phoneNumberWindow.ShowDialog(); 
            string phoneNumber = phoneNumberWindow.PhoneNumber;
            phoneNumberWindow.Close();

            this.customer = checkFunctions.customerExist(phoneNumber);
            Trace.WriteLine(customer.FirstName);
        }

        private void createProduct_Click(object sender, RoutedEventArgs e)
        {
            //On crée une nouvelle instance du panel
            ProductPanel product = new ProductPanel
            {
                ID = productIdCounter
            };

            // Ajout à la collection
            Products.Add(product);
            productIdCounter++;
        }
    }
}
