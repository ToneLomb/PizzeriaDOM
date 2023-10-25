using System;
using System.Windows;
using System.Windows.Input;

namespace PizzeriaDOM.Pages
{
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
