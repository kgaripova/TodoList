namespace TodoList.UI
{
    using Xamarin.Forms;

    public class PageBase : ContentPage
    {
        public PageBase(ViewModelBase model)
        {
            model.Navigation = this.Navigation;
        }
    }
}