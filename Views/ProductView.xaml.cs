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

        // Metoda zmniejszaj�ca ilo�� produktu
        private void OnDecreaseQuantityClicked(object sender, EventArgs e)
        {
            if (BindingContext is Product product)
            {
                if (product.Quantity > 0)
                {
                    product.Quantity--;  // Zmniejszamy ilo��
                    ShoppingListViewModel.Instance.SaveToXml();  // Zapisujemy dane do pliku
                }
            }
        }

        // Metoda zwi�kszaj�ca ilo�� produktu
        private void OnIncreaseQuantityClicked(object sender, EventArgs e)
        {
            if (BindingContext is Product product)
            {
                product.Quantity++;  // Zwi�kszamy ilo��
                ShoppingListViewModel.Instance.SaveToXml();  // Zapisujemy dane do pliku
            }
        }

        // Metoda usuwaj�ca produkt
        private void OnRemoveProductClicked(object sender, EventArgs e)
        {
            if (BindingContext is Product product)
            {
                var category = ShoppingListViewModel.Instance.Categories
                    .FirstOrDefault(c => c.Products.Contains(product));  // Znajdujemy kategori�, do kt�rej nale�y produkt

                if (category != null)
                {
                    category.Products.Remove(product);  // Usuwamy produkt z kategorii
                    ShoppingListViewModel.Instance.SaveToXml();  // Zapisujemy dane do pliku
                }
            }
        }
    }
}
