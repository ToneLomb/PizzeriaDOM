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
            //On récupère le numéro de téléphone du client
            PhoneNumberWindow phoneNumberWindow = new PhoneNumberWindow();
            phoneNumberWindow.ShowDialog();
            string phoneNumber = phoneNumberWindow.PhoneNumber;
            phoneNumberWindow.Close();

            //On récupère le customer s'il est enregistré dans notre BDD
            this.customer = checkFunctions.customerExist(phoneNumber);

            //Sinon on appelle la fenêtre de formulaire pour l'enregistrer
            if (customer == null)
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
                ID = productIdCounter,
                sizeGroup = "SizeGroup" + productIdCounter,
                productName = "ProductName" + productIdCounter

            };


            // Ajout à la collection
            Products.Add(product);
            //Set du groupName unique des radioBouttons
            productIdCounter++;
        }

        //Les deux méthodes ci dessous permettent de mettre le placeholder "Search ..."
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox search = (TextBox)sender;
            if (search.Text == "Search ...")
            {
                search.Text = string.Empty;
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox search = (TextBox)sender;
            if (search.Text == string.Empty)
            {
                search.Text = "Search ...";
            }
        }

        //Si pizza ou boisson checked, alors on met à jour le booléen de l'élément correspondant
        private void Boisson_Checked(object sender, RoutedEventArgs e)
        {
            ProductPanel product = (ProductPanel)(sender as RadioButton).DataContext;
            product.IsBoissonSelected = true;
        }

        private void Pizza_Checked(object sender, RoutedEventArgs e)
        {
            ProductPanel product = (ProductPanel)(sender as RadioButton).DataContext;
            product.IsPizzaSelected = true;
        }

        private void Boisson_UnChecked(object sender, RoutedEventArgs e)
        {
            ProductPanel product = (ProductPanel)(sender as RadioButton).DataContext;
            product.IsBoissonSelected = false;
        }

        private void Pizza_UnChecked(object sender, RoutedEventArgs e)
        {
            ProductPanel product = (ProductPanel)(sender as RadioButton).DataContext;
            product.IsPizzaSelected = false;
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            //On récupère la TextBox sur laquelle on a cliqué
            TextBox search = (TextBox)sender;

            //On récupère l'élément parent pour pouvoir accéder à la listBox et les suggestions de Produits
            StackPanel parentStackPanel = (StackPanel)search.Parent;
            ListBox suggestionList = (ListBox)parentStackPanel.FindName("suggestionList");
            Popup suggestionPopup = (Popup)parentStackPanel.FindName("suggestionPopup");

            //On récupère également le ProductPanel associé et en fonction du choix du client on charge la BDD associée aux produits
            ProductPanel productPanel = (ProductPanel)parentStackPanel.DataContext;
            string databaseToLoad = productPanel.IsPizzaSelected ? "Pizza" : "Drinks";
 
            List<Order.Product> suggestions = IOFile.ReadFromFile<Order.Product>(databaseToLoad);

            //On récupère les noms des Produits
            List<string> productsName = new List<string>();
            foreach(Order.Product product in suggestions)
            {
                productsName.Add(product.Name);
            }

            //Filtre en fonction de ce que l'utilisateur tape
            List<string> filteredSuggestions = productsName
            .Where(product => product.ToLower().Contains(search.Text.ToLower()))
            .ToList();

            //Mettre à jour la ListBox avec les suggestions filtrées
            suggestionList.ItemsSource = filteredSuggestions;
            suggestionPopup.IsOpen = productsName.Count > 0;
        }

        private void suggestionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //On récupère le StackPanel qui est le grand parent de la list box
            ListBox suggestionList = (ListBox)sender;
            Popup popup = (Popup)suggestionList.Parent;
            StackPanel parentStackPanel = (StackPanel)popup.Parent;

            //On récupère le produit associé 
            ProductPanel product = (ProductPanel)parentStackPanel.DataContext;

            //On enregistre la selection du client dans le produit
            if (suggestionList.SelectedItem != null)
            {
                string selectedSuggestion = suggestionList.SelectedItem.ToString();
                product.selectedProduct = selectedSuggestion;

            }

            TextBox search = (TextBox)parentStackPanel.FindName("SearchBox");
            search.Text = product.selectedProduct;
        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            Button decrease = (Button)sender;
            StackPanel parentStackPanel = (StackPanel)decrease.Parent;
            StackPanel grandParentStackPanel = (StackPanel)parentStackPanel.Parent;

            TextBlock quantity = (TextBlock)grandParentStackPanel.FindName("Quantity");
            int value = int.Parse(quantity.Text);
            Trace.WriteLine(value);
            if (value > 1) {
                value--;
            }
            quantity.Text = value.ToString();

        }

        private void Increment_Click(object sender, RoutedEventArgs e)
        {
            Button increase = (Button)sender;
            StackPanel parentStackPanel = (StackPanel)increase.Parent;
            StackPanel grandParentStackPanel = (StackPanel)parentStackPanel.Parent;

            TextBlock quantity = (TextBlock)grandParentStackPanel.FindName("Quantity");
            int value = int.Parse(quantity.Text);
            value++;
            quantity.Text = value.ToString();
        }
    }
}