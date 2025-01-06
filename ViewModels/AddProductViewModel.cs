using ShopingListApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopingListApp.ViewModels
{
    public class AddProductViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public double Quantity { get; set; } = 1;
        public string SelectedUnit { get; set; }
        public Category SelectedCategory { get; set; }

        public ObservableCollection<string> Units { get; } = new() { "szt", "l", "kg" };
        public ObservableCollection<Category> Categories { get; }

        private ShoppingListViewModel _shoppingListViewModel;

        public Command AddProductCommand { get; }

        public AddProductViewModel(ShoppingListViewModel shoppingListViewModel)
        {
            _shoppingListViewModel = shoppingListViewModel;

            Categories = new ObservableCollection<Category>(
                _shoppingListViewModel.Categories.Select(c => new Category
                {
                    Name = c.Name,
                    Products = new ObservableCollection<Product>(c.Products)
                })
            );

            AddProductCommand = new Command(AddProduct);
        }

        private async void AddProduct()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(SelectedUnit) || SelectedCategory == null)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", "Wypełnij wszystkie pola!", "OK");
                return;
            }

            var product = new Product
            {
                Name = Name,
                Quantity = Quantity,
                Unit = SelectedUnit,
                Category = SelectedCategory.Name,
                IsPurchased = false
            };

            _shoppingListViewModel.AddProduct(product);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
