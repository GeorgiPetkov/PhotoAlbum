namespace Presentation.Pages
{
    using System.ComponentModel;
    using System.Windows;

    public abstract class Page : System.Windows.Controls.Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            protected set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        protected Page()
        {
            GetType().GetMethod("InitializeComponent").Invoke(this, new object[] { });
            DataContext = this;
        }

        protected void NavigateToPage(System.Windows.Controls.Page page) => ((Window)Parent).Content = page;

        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
