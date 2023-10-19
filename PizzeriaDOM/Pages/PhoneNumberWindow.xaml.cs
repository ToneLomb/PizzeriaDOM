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
    /// Logique d'interaction pour PhoneNumberWindow.xaml
    /// </summary>
    public partial class PhoneNumberWindow : Window
    {
        public string PhoneNumber { get; private set; }
        public PhoneNumberWindow()
        {
            InitializeComponent();
        }

        private bool isNumber;

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                isNumber = true;
                //Permet de check si toute la chaîne de caractère contient uniquement des digit
                foreach(char c in TextPhoneNumber.Text)
                {
                    if (!Char.IsDigit(c))
                    {
                        isNumber = false;
                    }
                }
                if (isNumber)
                {
                    PhoneNumber = TextPhoneNumber.Text;
                    this.Close();
                }
                else
                {
                    Error.Content = "Only digit are allowed !";
                }
                
            }
        }
    }
}
