using System;
using System.ComponentModel;

namespace ShopingListApp.Models
{
    public class Product : INotifyPropertyChanged
    {
        private bool _isPurchased;
        private double _quantity;

        public string Name { get; set; }

        // Właściwość Quantity, która wywołuje OnPropertyChanged, gdy wartość się zmienia
        public double Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)  // Jeśli wartość się zmienia
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));  // Powiadamiamy o zmianie
                }
            }
        }

        public string Unit { get; set; } // Jednostka miary: "szt", "l", "kg"
        public string Category { get; set; }

        public bool IsPurchased
        {
            get => _isPurchased;
            set
            {
                if (_isPurchased != value)  // Jeśli wartość się zmienia
                {
                    _isPurchased = value;
                    OnPropertyChanged(nameof(IsPurchased));  // Powiadamiamy o zmianie
                }
            }
        }

        // Zdarzenie PropertyChanged, które wywołuje powiadomienie o zmianach
        public event PropertyChangedEventHandler PropertyChanged;

        // Metoda wywołująca powiadomienie o zmianie właściwości
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
