using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PizzeriaDOM.src.classes;
using PizzeriaDOM.src.functions;

namespace PizzeriaDOM.Pages
{
    public partial class Statistics : UserControl
    {
        private ListSortDirection sortDirection = ListSortDirection.Ascending;

        public Statistics()
        {
            InitializeComponent();

            Update();
        }

        public void Update()
        {
            List<Order> ordersList = new List<Order>();
            ordersList = IOFile.ReadFromFile<Order>("Orders");

            float totalordersPrice = 0;

            int numberOfCustomers = 0;

            //list of string used to check if the customer have already been counted
            List<string> CustomersNumber = new List<string>();

            //calculate the total price of all orders + the number of customers
            foreach (Order order in ordersList)
            {
                totalordersPrice += (float)order.PriceOrder;

                if (!CustomersNumber.Contains(order.Customer.TelephoneNumber))
                {
                    CustomersNumber.Add(order.Customer.TelephoneNumber);
                    numberOfCustomers++;
                }
            }

            int numberOfOrders = ordersList.Count;

            AverageOrder.Text = (totalordersPrice / numberOfOrders) + " € per order";

            AverageAccount.Text = (totalordersPrice / numberOfCustomers) + " € per customer";

            OrderList.ItemsSource = ordersList;
        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;

            if (headerClicked != null)
            {
                string header = headerClicked.Column.Header as string;
                ToggleSortDirection();
                Sort(header, sortDirection);
            }
        }

        private void ToggleSortDirection()
        {
            if (sortDirection == ListSortDirection.Ascending)
            {
                sortDirection = ListSortDirection.Descending;
            }
            else
            {
                sortDirection = ListSortDirection.Ascending;
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(OrderList.ItemsSource);

            dataView.SortDescriptions.Clear();

            if (!string.IsNullOrEmpty(sortBy))
            {
                SortDescription sd = new SortDescription(sortBy, direction);
                dataView.SortDescriptions.Add(sd);
                dataView.Refresh();
            }
        }

    }
}
