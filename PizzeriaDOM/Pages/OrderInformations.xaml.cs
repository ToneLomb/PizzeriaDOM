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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PizzeriaDOM.src.classes;
using PizzeriaDOM.src.functions;

namespace PizzeriaDOM.Pages
{
    /// <summary>
    /// Logique d'interaction pour OrderInformations.xaml
    /// </summary>
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
            orderList = IOFile.ReadFromFile<Order>("Order");

            foreach (Order order in orderList)
            {
                ComboID.Items.Add(new ComboBoxItem() { Content = order.ID });
            }
        }

        private void ComboID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateOrderList();

            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            //convert selectItem to an int
            int selectedID = Convert.ToInt32(selectedItem.Content);

            //get the order with the selected ID
            Order selectedOrder = orderList.Find(order => order.ID == selectedID);

            //CustomerNumberTextBlock.Text = selectedOrder.CustomerTelephoneNumber;

            TotalPriceTextBlock.Text = selectedOrder.PriceOrder.ToString();

            StatusTextBlock.Text = selectedOrder.State;
            //add changing color depending on the status ?

            DateTextblock.Text = selectedOrder.DateOrder.ToString();

            List<Order.Product> productList = selectedOrder.ProductList;

            //display the list of product in the order
            //TODO
        }
    }
}
