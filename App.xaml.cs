namespace ShopingListApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            
            Resources.Add(new StyleSheet
            {
                Source = new Uri("Resources/Styles/styles.css", UriKind.Relative)
            });

            MainPage = new AppShell();
        }
    }
}
