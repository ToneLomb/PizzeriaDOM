using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class CustomerPage : UserControl
    {
        private ListSortDirection sortDirection = ListSortDirection.Ascending;
        public CustomerPage()
        {
            InitializeComponent();

            updateCustomerList();
        }
        
        public void updateCustomerList()
        {
            //read each file
            List<Customer> customerList = IOFile.ReadFromFile<Customer>("Customer");

            //use the list as the source of the ListView
            CustomerListView.ItemsSource = customerList;
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
            ICollectionView dataView = CollectionViewSource.GetDefaultView(CustomerListView.ItemsSource);

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
