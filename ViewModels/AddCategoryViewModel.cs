using ShopingListApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopingListApp.ViewModels
{
    public class AddCategoryViewModel : INotifyPropertyChanged
    {
        private string _categoryName;
        public string CategoryName
        {
            get => _categoryName;
            set
            {
                if(_categoryName != value)
                {
                    _categoryName = value;
                    OnPropertyChanged(nameof(CategoryName));
                }
            }
        }

        public ObservableCollection<CategoryViewModel> Categories { get; }

        public ICommand AddCategoryCommand { get; }
        public ICommand CancelCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public AddCategoryViewModel()
        {
            Categories = ShoppingListViewModel.Instance.Categories;
            AddCategoryCommand = new Command(AddCategory);
            CancelCommand = new Command(Cancel);

        }

        private async void AddCategory()
        {
            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", "Podaj nazwę kategorii", "OK");
                return;
            }

            
            var newCategory = new CategoryViewModel
            {
                Name = CategoryName,
            };
            ShoppingListViewModel.Instance.Categories.Add(newCategory);


            if (!ShoppingListViewModel.Instance.Categories.Contains(newCategory))
            {
                ShoppingListViewModel.Instance.Categories.Add(newCategory);
            }

            ShoppingListViewModel.Instance.SaveToXml();
        
            

            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void Cancel()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void OnPropertyChanged(string propretyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propretyName));
        }
    }
}
