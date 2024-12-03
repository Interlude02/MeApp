namespace MeApp
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();
            //var navigationPage= new NavigationPage(new MainPage());
           // MainPage = mainPage; this works
            MainPage = new NavigationPage(mainPage);
           // MainPage = mainPage;
        }
    }
}
// public App(MainPage mainPage)
//{
  //  InitializeComponent();
  
  //  MainPage = mainPage;
//}
