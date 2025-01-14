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

       
        public bool HasProducts => Products?.Count > 0;

        
        public Command ToggleCategoryCommand { get; }

        public CategoryViewModel()
        {
           
            ToggleCategoryCommand = new Command(ToggleCategory);
        }

      
        private void ToggleCategory()
        {
            IsExpanded = !IsExpanded;
        }

      
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
