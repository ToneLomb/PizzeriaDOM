using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using PizzeriaDOM.src.classes;
using PizzeriaDOM.src.functions;

namespace PizzeriaDOM.Pages
{
    public partial class NewEmployeeWindow : Window
    {
        string selectedText = "";

        public NewEmployeeWindow()
        {
            InitializeComponent();
        }

        //Add a new employee with the corresponding type
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            List<Object> list = new List<Object>();
            string selectedType = this.selectedText.Replace(" ", "");

            //create a new employee depending on the selected type
            if (this.selectedText.Equals("Clerk"))
            {
                Clerk new_employee = new Clerk();
                new_employee.ID = checkFunctions.getEmployeeLastID(selectedType) + 1;
                new_employee.OrderManaged = 0;

                list.Add(new_employee);
            }
            else if (this.selectedText.Equals("Delivery Man"))
            {
                int ID = checkFunctions.getEmployeeLastID(selectedType) + 1;
                DeliveryMan new_employee = new DeliveryMan(0,ID,true);

                list.Add(new_employee);
            }

            //write the new employee in the corresponding file
            IOFile.WriteInFile(list, selectedType);

            Trace.WriteLine("NewEmployeeWindow.xaml.cs : Ajout d'un nouveau " + this.selectedText);

            this.Close();
        }

        //handle selection change in the comboBox
        private void TypeEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            if (selectedItem != null)
            {
                this.selectedText = selectedItem.Content.ToString();
            }
        }
    }
}
