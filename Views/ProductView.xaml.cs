using ShopingListApp.Models;
using ShopingListApp.ViewModels;
using System.Linq;

namespace ShopingListApp.Views
{
    public partial class ProductView : ContentView
    {
        public ProductView()
        {
            InitializeComponent();
        }

        // Metoda zmniejszaj¹ca iloœæ produktu
        private void OnDecreaseQuantityClicked(object sender, EventArgs e)
        {
            if (BindingContext is Product product)
            {
                if (product.Quantity > 0)
                {
                    product.Quantity--;  // Zmniejszamy iloœæ
                    ShoppingListViewModel.Instance.SaveToXml();  // Zapisujemy dane do pliku
                }
            }
        }

        // Metoda zwiêkszaj¹ca iloœæ produktu
        private void OnIncreaseQuantityClicked(object sender, EventArgs e)
        {
            if (BindingContext is Product product)
            {
                product.Quantity++;  // Zwiêkszamy iloœæ
                ShoppingListViewModel.Instance.SaveToXml();  // Zapisujemy dane do pliku
            }
        }

        // Metoda usuwaj¹ca produkt
        private void OnRemoveProductClicked(object sender, EventArgs e)
        {
            if (BindingContext is Product product)
            {
                var category = ShoppingListViewModel.Instance.Categories
                    .FirstOrDefault(c => c.Products.Contains(product));  // Znajdujemy kategoriê, do której nale¿y produkt

                if (category != null)
                {
                    category.Products.Remove(product);  // Usuwamy produkt z kategorii
                    ShoppingListViewModel.Instance.SaveToXml();  // Zapisujemy dane do pliku
                }
            }
        }
    }
}
