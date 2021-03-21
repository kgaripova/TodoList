namespace TodoList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ItemListModel : ViewModelBase
    {
        private IDataService dataService;
        private List<ItemModel> items;
        private int listHeightRequest;
        private bool listIsEmpty;

        public ItemListModel() : base()
        {
            this.dataService = DependencyService.Get<IDataService>();
        }
        
        public List<ItemModel> Items 
        { 
            get
            {
                return this.items;
            }
            private set
            {
                this.items = value;
                OnPropertyChanged();
            } 
        }
        
        public int ListHeightRequest
        {
            get
            {
                return listHeightRequest;
            }
            set
            {
                this.listHeightRequest = value;
                OnPropertyChanged();
            }
        }
        
        public bool ListIsEmpty
        {
            get
            {
                return listIsEmpty;
            }
            set
            {
                this.listIsEmpty = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand RefreshCommand
        {
            get
            {
                return new Command((s) =>  this.ReloadData(), (s) => true);
            }
        }
        
        public async Task ReloadData()
        {
            this.IsDataLoading = true;
            
            try
            {
                this.Items = (await this.dataService.GetItems()).Select(s => new ItemModel(s)).ToList()
                    .OrderByDescending(i => i.CreatedDate).ToList();
                    
                this.ListHeightRequest = AppStyles.ListRowHeight * this.items.Count;
                this.ListIsEmpty = this.items.Count == 0;
            }   
            catch (Exception ex)
            {
                DependencyService.Get<ILogger>().LogException(ex);
                //TODO:Notify a user somehow
            }

            this.IsDataLoading = false;
        }
    }
}