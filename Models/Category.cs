using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShopingListApp.Models
{
    public class Category : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public ObservableCollection<string> Units { get; set; } = new();
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Category()
        {
            Products.CollectionChanged += (s, e) => OnPropertyChanged(nameof(HasProducts));
        }
    }
}
