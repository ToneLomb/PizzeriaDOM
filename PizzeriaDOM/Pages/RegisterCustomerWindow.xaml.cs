using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using PizzeriaDOM.src.classes;
using PizzeriaDOM.src.functions;

namespace PizzeriaDOM.Pages
{
    /// <summary>
    /// Logique d'interaction pour RegisterCustomerWindow.xaml
    /// </summary>
    public partial class RegisterCustomerWindow : Window
    {
        public RegisterCustomerWindow()
        {
            InitializeComponent();
        }
        private bool isEmpty = false;
        public Customer NewCustomer { get; private set; }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> fields = new List<TextBox>
            {
                Surname,
                Firstname,
                Adress,
                Phone
            };

            foreach (TextBox textBox in fields)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    isEmpty = true;
                    break; // Si un TextBox est vide, sortez de la boucle.
                }
            }

            if (isEmpty)
            {
                Error.Content = "Fields cannot be empty !";
            }
            else
            {
                Customer customer = new Customer(Surname.Text, Firstname.Text, Adress.Text, Phone.Text, DateTime.Now);
                NewCustomer = customer;
                List<Object> newCustomer = new List<Object>();
                newCustomer.Add(customer);
                IOFile.WriteInFile(newCustomer, "Customer");
                this.Close();
            }
        }
    }
}
