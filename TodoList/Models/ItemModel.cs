namespace TodoList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Extensions;
    using Xamarin.Forms;

    public class ItemModel : ViewModelBase
    {
        private int id;
        private string name;
        private string description;
        private State state;
        public ImageSource stateImage;
        public string stateCaption;
        
        private IDataService dataService;
        public static ImageSource Icon_open = ImageSource.FromFile("open.png");
        public static ImageSource Icon_progress = ImageSource.FromFile("progress.png");
        public static ImageSource Icon_done = ImageSource.FromFile("done.png");

        public static List<StateModel> StateModels =
            Enum.GetValues(typeof(State)).Cast<State>().Select(s => new StateModel(s)).ToList();
        
        public ItemModel(TodoItem item) : base()
        {
            this.dataService = DependencyService.Get<IDataService>();
            this.InitModel(item);
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                OnPropertyChanged();
            }
        }
        public string Description 
        { 
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
                OnPropertyChanged();
            } 
        }
        public bool IsExistingItem 
        { 
            get
            {
                return this.Id != 0;
            }
        }
        public DateTime CreatedDate { get; set; }

        public State State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
                switch (this.State)
                {
                    case State.Open:
                        this.StateImage = Icon_open;
                        break;
                    case State.InProgress:
                        this.StateImage = Icon_progress;
                        break;
                    case State.Done:
                        this.StateImage = Icon_done;
                        break;
                    default:
                        break;
                }

                this.StateCaption = this.state.Caption();
                OnPropertyChanged();
            }
        }

        public string CreatedLabel
        {
            get
            {
                var date = this.CreatedDate.Date == DateTime.Today.Date ? "Today" : this.CreatedDate.ToString("dd MMM");
                return string.Format("{0}, {1}", date, this.CreatedDate.ToString("HH:mm"));
            }
        }

        public ImageSource StateImage
        {
            get
            {
                return this.stateImage;
            }
            set
            {
                this.stateImage = value;
                OnPropertyChanged();
            }
        }
        public string StateCaption
        {
            get
            {
                return this.stateCaption;
            }
            set
            {
                this.stateCaption = value;
                OnPropertyChanged();
            }
        }

        public async Task Save()
        {
            TodoItem itemToSave = new TodoItem(this.Id, this.Name, this.Description, this.CreatedDate, this.State);
            int id = await DependencyService.Get<IDataService>().SaveItem(itemToSave);
            this.Id = id;
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new Command((s) =>  this.Delete(), (s) => true);    
            }
        }

        public async Task Delete()
        {
            var action = await App.Current.MainPage.DisplayActionSheet(
                "Are you sure?",
                null, null,
                "Yes!",
                "No");

            if (action == "No")
            {
                return;
            }
            
            await DependencyService.Get<IDataService>().DeleteItem(this.Id);
            this.Navigation.PopAsync();
        }
        
        private void InitModel(TodoItem item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.Description = item.Description;
            this.CreatedDate = item.CreatedDate;
            this.State = item.State;
        }
    }
}