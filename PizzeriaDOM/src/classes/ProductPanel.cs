using System.ComponentModel;

namespace PizzeriaDOM.src.classes
{
    public class ProductPanel : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string sizeGroup { get; set; }
        public string productName { get; set; }
        public string selectedProduct { get; set; }
        public string size { get; set; }

        public int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        //Déf du booléen
        private bool isBoissonSelected;

        //Définition du Getter/Setter
        public bool IsBoissonSelected
        {
            get { return isBoissonSelected; }
            set
            {
                //Si l'ancienne valeur est effectivement différente de la nouvelle
                if (isBoissonSelected != value)
                {
                    //Alors on la met à jour et on appelle la méthode OnPropertyChanged en lui indiquant le nom de la propriété qui a changé
                    isBoissonSelected = value;
                    OnPropertyChanged(nameof(IsBoissonSelected));
                }
            }
        }

        //Même qu'au dessus
        private bool isPizzaSelected;
        public bool IsPizzaSelected
        {
            get { return isPizzaSelected; }
            set
            {
                if (isPizzaSelected != value)
                {
                    isPizzaSelected = value;
                    OnPropertyChanged(nameof(IsPizzaSelected));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //Permet de Bind avec le XMAL
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}