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
using Newtonsoft.Json;
using PizzeriaDOM.src.classes;
using PizzeriaDOM.src.functions;
using RabbitMQ.Client;

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
            //On récupère le numéro de téléphone du client
            PhoneNumberWindow phoneNumberWindow = new PhoneNumberWindow();
            phoneNumberWindow.ShowDialog(); 
            string phoneNumber = phoneNumberWindow.PhoneNumber;
            phoneNumberWindow.Close();

            //On récupère le customer s'il est enregistré dans notre BDD
            this.customer = checkFunctions.customerExist(phoneNumber);

            //Sinon on appelle la fenêtre de formulaire pour l'enregistrer
            if(customer == null)
            {
                RegisterCustomerWindow registerCustomerWindow = new RegisterCustomerWindow();
                registerCustomerWindow.ShowDialog();
                this.customer = registerCustomerWindow.NewCustomer;

            }

            //Affichage du nom du client
            CustomerName.Content = "Customer : " + customer.Surname + " " + customer.FirstName;
            
            
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

        private void sendCommand_Click(object sender, RoutedEventArgs e)
        {
            //Récupérer les infos et pas les mettre en brut
            List<Order.Product> products = new List<Order.Product>();
            Order.Product product = new Order.Product("M", "Boisson", 10);
            Order order = new Order(1, "0000000000", 10, "Preparation", DateTime.Now, products);
            //
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            string serializedObject = JsonConvert.SerializeObject(order);

            var body = Encoding.UTF8.GetBytes(serializedObject);

            //channel.ExchangeDeclare("broadcast", type: ExchangeType.Fanout);
            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "kitchen",
                                 basicProperties: null,
                                 body: body);
            Trace.WriteLine("Message envoyé");

        }
    }
}
