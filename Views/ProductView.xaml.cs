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

        
        private void OnDecreaseQuantityClicked(object sender, EventArgs e)
        {
            if (BindingContext is Product product)
            {
                if (product.Quantity > 0)
                {
                    product.Quantity--;
                    ShoppingListViewModel.Instance.SaveToXml();
                }
            }
        }

        
        private void OnIncreaseQuantityClicked(object sender, EventArgs e)
        {
            if (BindingContext is Product product)
            {
                product.Quantity++;  
                ShoppingListViewModel.Instance.SaveToXml(); 
            }
        }

        
        private void OnRemoveProductClicked(object sender, EventArgs e)
        {
            if (BindingContext is Product product)
            {
                var category = ShoppingListViewModel.Instance.Categories
                    .FirstOrDefault(c => c.Products.Contains(product));  

                if (category != null)
                {
                    category.Products.Remove(product);  
                    ShoppingListViewModel.Instance.SaveToXml();  
                }
            }
        }
    }
}
