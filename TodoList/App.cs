namespace TodoList
{
    using System.Threading.Tasks;
    using UI;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public App()
        {
            var model = new ItemListModel();
            MainPage = new NavigationPage(new ItemList(model));
        }
        
        protected override async void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}