using ShopingListApp.ViewModels;

namespace ShopingListApp.Views;

public partial class AddProductPage : ContentPage
{
    public AddProductPage(ShoppingListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = new AddProductViewModel(viewModel);
    }
}
