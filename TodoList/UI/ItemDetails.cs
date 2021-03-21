namespace TodoList.UI
{
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class ItemDetails : PageBase
    {
        private ItemModel ItemModel
        {
            get
            {
                return this.BindingContext as ItemModel;
            }
        }

        public ItemDetails(ItemModel itemModel) : base(itemModel)
        {
            this.BindingContext = itemModel;
            this.Title = itemModel.Id > 0 ? itemModel.Name : "Add a task";
            
            var name = new Entry()
            {
                Placeholder = "Name",
                HorizontalOptions = LayoutOptions.Fill
            };
            name.SetBinding<ItemModel>(Entry.TextProperty, m => m.Name, BindingMode.TwoWay);

            var description = new Editor()
            {
                Placeholder = "Description",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 200
            };
            description.SetBinding<ItemModel>(Entry.TextProperty, m => m.Description, BindingMode.TwoWay);

            var created = new Label()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.End,
                TextColor = AppStyles.Color_Grey,
                Padding = new Thickness(0, 0, 0, 5),
                FontSize = 16,
            };
            created.SetBinding<ItemModel>(Label.TextProperty, m => m.CreatedLabel);

            var stateIcon = new Image()
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };
            stateIcon.SetBinding<ItemModel>(Image.SourceProperty, m => m.StateImage);
            
            var picker = new Picker()
            {
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 150,
                ItemsSource = ItemModel.StateModels.Select(m => m.Caption).ToList()
            };
            picker.SetBinding<ItemModel>(Picker.SelectedIndexProperty, m => m.State, 
                BindingMode.TwoWay, new IntEnumConverter());
            
            var stateRow = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                Children = { created, stateIcon, picker }
            };

            var emptyBox = new BoxView()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.StartAndExpand
            };
            var deleteButton = new Button()
            {
                Text = "Delete",
                WidthRequest = 100,
                BorderColor = Color.Red,
                BorderWidth = 1,
                TextColor = Color.Red,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Margin = 30
            };
            deleteButton.SetBinding<ItemModel>(Button.IsVisibleProperty, s => s.IsExistingItem );
            deleteButton.SetBinding<ItemModel>(Button.CommandProperty, s => s.DeleteCommand );
            
            var layout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Children = { name, description, stateRow, emptyBox, deleteButton },
                Padding = 10,
                Spacing = 10
            };

            this.Content = layout;
            
            ToolbarItems.Add(new ToolbarItem("Save", null,() =>
            {
                this.Save();
            }));
        }

        private async Task Save()
        {
            await this.ItemModel.Save();
            await Navigation.PopAsync();
        }
    }
}