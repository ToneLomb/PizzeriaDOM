using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using PizzeriaDOM.src.classes;
using PizzeriaDOM.src.functions;

namespace PizzeriaDOM.Pages
{
    public partial class OrderInformations : UserControl
    {
        List<Order> orderList = new List<Order>();

        public OrderInformations()
        {
            InitializeComponent();

            updateOrderList();
        }

        public void updateOrderList()
        {
            orderList = IOFile.ReadFromFile<Order>("Orders");

            foreach (Order order in orderList)
            {
                ComboID.Items.Add(new ComboBoxItem() { Content = order.ID });
            }
        }

        private void ComboID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            //convert selectItem to an int
            int selectedID = Convert.ToInt32(selectedItem.Content);

            //get the order with the selected ID
            Order selectedOrder = orderList.Find(order => order.ID == selectedID);

            //Customer number
            CustomerNumberTextBlock.Text = selectedOrder.Customer.TelephoneNumber.ToString();

            //Total price
            TotalPriceTextBlock.Text = selectedOrder.PriceOrder.ToString() + " €";

            //Status
            StatusTextBlock.Text = selectedOrder.State;

            //Date
            DateTextblock.Text = selectedOrder.DateOrder.ToString();

            //product list
            //create 2 lists, one for pizza and one for drinks
            List<Order.Product> availablePizzas = IOFile.ReadFromFile<Order.Product>("Pizza");
            List<Order.Product> availableDrinks = IOFile.ReadFromFile<Order.Product>("Drinks");

            List<string> AvailablePizzasName = new List<string>();
            List<string> AvailableDrinksName = new List<string>();

            foreach (Order.Product pizza in availablePizzas)
            {
                AvailablePizzasName.Add(pizza.Name);
            }
            foreach (Order.Product drink in availableDrinks)
            {
                AvailableDrinksName.Add(drink.Name);
            }

            List<object> pizzaList = new List<object>();
            List<Order.Product> drinkList = new List<Order.Product>();

            //if pizzaList.Name == a string in AvailablePizzasName, add it to pizzaList
            foreach (Order.Product product in selectedOrder.ProductList)
            {
                if (AvailablePizzasName.Contains(product.Name))
                {
                    pizzaList.Add(product);
                }
                else
                {
                    drinkList.Add(product);
                }
            }

            //display pizza list in PizzaListView
            PizzaListView.ItemsSource = pizzaList;
            DrinkListView.ItemsSource = drinkList;
        }
    }
}
