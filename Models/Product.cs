using System;
using System.ComponentModel;

namespace ShopingListApp.Models
{
    public class Product : INotifyPropertyChanged
    {
        private bool _isPurchased;
        private double _quantity;

        public string Name { get; set; }

        
        public double Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)  
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));  
                }
            }
        }

        public string Unit { get; set; } 
        public string Category { get; set; }

        public bool IsPurchased
        {
            get => _isPurchased;
            set
            {
                if (_isPurchased != value) 
                {
                    _isPurchased = value;
                    OnPropertyChanged(nameof(IsPurchased)); 
                }
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
