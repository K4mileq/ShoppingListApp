using ShopingListApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ShopingListApp.ViewModels
{
    public class CategoryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> Units { get; set; } = new();
        public string Name { get; set; }
        public ObservableCollection<Product> Products { get; set; } = new();

        private bool _isExpanded = true;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

        // Sprawdza, czy kategoria ma produkty
        public bool HasProducts => Products?.Count > 0;

        // Komenda do prze³¹czania rozwiniêcia kategorii
        public Command ToggleCategoryCommand { get; }

        public CategoryViewModel()
        {
            // Przypisanie komendy do toggle'owania kategorii
            ToggleCategoryCommand = new Command(ToggleCategory);
        }

        // Metoda do prze³¹czania stanu rozwiniêcia/zwiniêcia
        private void ToggleCategory()
        {
            IsExpanded = !IsExpanded;
        }

        // Tylko jedno zdarzenie PropertyChanged w klasie
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
