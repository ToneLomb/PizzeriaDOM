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
        public TakeOrder()
        {
            InitializeComponent();
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
            
            
        }
    }
}
