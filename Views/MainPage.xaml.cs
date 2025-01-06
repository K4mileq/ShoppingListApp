using ShopingListApp.ViewModels;

namespace ShopingListApp.Views;

public partial class MainPage : ContentPage
{
    private ShoppingListViewModel _viewModel;

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new ShoppingListViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.SaveToXml();
    }
}
