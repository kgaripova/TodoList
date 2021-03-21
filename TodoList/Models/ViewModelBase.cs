namespace TodoList
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Xamarin.Forms;

    public class ViewModelBase : INotifyPropertyChanged
    {
        private bool isDataLoading;
        public INavigation Navigation;

        public bool IsDataLoading 
        { 
            get
            {
                return this.isDataLoading;
            }
            protected set
            {
                this.isDataLoading = value;
                OnPropertyChanged();
            } 
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}