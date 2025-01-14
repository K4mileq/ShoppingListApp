using ShopingListApp.Models;
using ShopingListApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ShopingListApp.ViewModels
{
    public class ShoppingListViewModel : INotifyPropertyChanged
    {
        public static ShoppingListViewModel Instance { get; private set; }
        public ObservableCollection<CategoryViewModel> Categories { get; set; } = new();

        public Command AddProductCommand { get; }
        public Command AddCategoryCommand { get; }

        public ShoppingListViewModel()
        {
            Instance = this;
            AddProductCommand = new Command(OpenAddProductPage);
            AddCategoryCommand = new Command(OpenAddCategoryPage);

            // Inicjalizacja kategorii
            Categories.Add(new CategoryViewModel
            {
                Name = "Nabiał",
                Products = new ObservableCollection<Product>
                {
                    new Product { Name = "Mleko", Quantity = 1, Unit = "l", Category = "Nabiał", IsPurchased = false }
                }
            });
            Categories.Add(new CategoryViewModel
            {
                Name = "Warzywa",
                Products = new ObservableCollection<Product>
                {
                    new Product { Name = "Ziemniaki", Quantity = 5, Unit = "kg", Category = "Warzywa", IsPurchased = false }
                }
            });
            Categories.Add(new CategoryViewModel
            {
                Name = "Owoce",
                Products = new ObservableCollection<Product>
                {
                    new Product { Name = "Jabłka", Quantity = 2, Unit = "kg", Category = "Owoce", IsPurchased = false }
                }
            });
            Categories.Add(new CategoryViewModel
            {
                Name = "Elektronika",
                Products = new ObservableCollection<Product>
                {
                    new Product { Name = "Baterie", Quantity = 2, Unit = "szt", Category = "Elektronika", IsPurchased = false }
                }
            });
            Categories.Add(new CategoryViewModel
            {
                Name = "Inne",
                Products = new ObservableCollection<Product>
                {
                    new Product { Name = "Parasol", Quantity = 1, Unit = "szt", Category = "Inne", IsPurchased = false }
                }
            });

            // Załaduj dane z pliku XML
            LoadFromXml();

        }

        private async void OpenAddProductPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage(this));
        }

        private async void OpenAddCategoryPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddCategoryPage());
        }


        public void AddProduct(Product product)
        {
            var category = Categories.FirstOrDefault(c => c.Name == product.Category);
            if (category == null)
            {
                category = new CategoryViewModel { Name = product.Category };
                Categories.Add(category);
            }
            category.Products.Add(product);
            SaveToXml();

        }

        public void AddCategory(string categoryName)
        {
            var category = Categories.FirstOrDefault(c => c.Name == categoryName);
            if (category == null)
            {
                category = new CategoryViewModel 
                { 
                    Name = categoryName,
                    Products = new ObservableCollection<Product>()
                };
                Categories.Add(category);
                SaveToXml();
            }
        }

        public void LoadFromXml()
        {
            if (!File.Exists(GetFilePath())) return;

            var serializer = new XmlSerializer(typeof(List<Category>));
            using var reader = new StreamReader(GetFilePath());
            var categories = (List<Category>)serializer.Deserialize(reader);
            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(new CategoryViewModel
                {
                    Name = category.Name,
                    Products = new ObservableCollection<Product>(
                        category.Products.OrderBy(p => p.IsPurchased)
                    )
                });
            }
        }

        public void SaveToXml()
        {
            var serializer = new XmlSerializer(typeof(List<Category>));
            using var writer = new StreamWriter(GetFilePath());

            var categoriesToSave = Categories.Select(c => new Category
            {
                Name = c.Name,
                Products = new ObservableCollection<Product>(
                    c.Products.Select(p => new Product
                    {
                        Name = p.Name,
                        Quantity = p.Quantity,
                        Unit = p.Unit,
                        Category = p.Category,
                        IsPurchased = p.IsPurchased
                    }))
            }).ToList();

            serializer.Serialize(writer, categoriesToSave);
        }


        private string GetFilePath() =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "shopping_list.xml");

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

