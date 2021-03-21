namespace TodoList.UI
{
    using System;
    using Xamarin.Forms;

    public class ItemList : PageBase
    {
        private ItemListModel Model
        {
            get
            {
                return this.BindingContext as ItemListModel;
            }
        }
        public ItemList(ItemListModel model) : base(model)
        {
            this.Title = "My tasks";
            this.BindingContext = model;
            
            var itemList = new ListView
            {
                HasUnevenRows = true,
                IsPullToRefreshEnabled = false,
                SelectionMode = ListViewSelectionMode.None,
                RowHeight = AppStyles.ListRowHeight,
                SeparatorVisibility = SeparatorVisibility.Default,
                ItemTemplate = new DataTemplate(typeof(ItemListCell)),
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Transparent
            };
            itemList.SetBinding<ItemListModel>(ListView.ItemsSourceProperty, m => m.Items);
            itemList.SetBinding<ItemListModel>(ListView.IsRefreshingProperty, m => m.IsDataLoading);
            itemList.SetBinding<ItemListModel>(ListView.RefreshCommandProperty, m => m.RefreshCommand);
            itemList.SetBinding<ItemListModel>(ListView.HeightRequestProperty, m => m.ListHeightRequest);
            itemList.ItemTapped += ItemTapped;

            var emptyState = new Label()
            {
                Text = "You've got nothing to do! Add a task 👆",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(0, 20, 0, 0)
            };
            emptyState.SetBinding<ItemListModel>(Label.IsVisibleProperty, m => m.ListIsEmpty);

            this.Content = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Children = { emptyState, itemList }
            };
            
            ToolbarItems.Add(new ToolbarItem("Add", null,() =>
            {
                NavigationPage.SetBackButtonTitle(this, "Cancel");
                Navigation.PushAsync(new ItemDetails(new ItemModel(new TodoItem(0, "", "", DateTime.Now, State.Open))));
            }));
        }
        
        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ItemDetails(e.Item as ItemModel));
        }  
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            this.Model.ReloadData();
        }
    }
}