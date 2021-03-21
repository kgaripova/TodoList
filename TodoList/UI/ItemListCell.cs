namespace TodoList.UI
{
    using System.Net.Sockets;
    using Xamarin.Forms;

    public class ItemListCell : ViewCell
    {
        public ItemListCell()
        {
            var name = new Label()
            {
                FontSize = 16,
                TextColor = AppStyles.Color_Black,
                MaxLines = 1,
                VerticalOptions = LayoutOptions.Center,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            name.SetBinding<ItemModel>(Label.TextProperty, m => m.Name);
            
            var description = new Label()
            {
                FontSize = 14,
                TextColor = AppStyles.Color_Grey,
                Padding = new Thickness(0, 3, 0, 0),
                MaxLines = 1,
                VerticalOptions = LayoutOptions.Center,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            description.SetBinding<ItemModel>(Label.TextProperty, m => m.Description);
            
            var icon = new Image()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            icon.SetBinding<ItemModel>(Image.SourceProperty, m => m.StateImage);
            
            var info = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = { name, description },
                Spacing = 0
            };

            var stack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Spacing = 15,
                Margin = new Thickness(20, 10, 15, 10),
                Children = { icon, info }
            };
                
            this.View = stack;
        }
    }
}