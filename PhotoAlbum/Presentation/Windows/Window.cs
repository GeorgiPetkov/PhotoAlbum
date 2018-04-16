namespace Presentation.Windows
{
    using Prism.Commands;
    using System.ComponentModel;
    using System.Windows.Input;
    using static System.Windows.ResizeMode;
    using static System.Windows.SizeToContent;

    public abstract class Window : System.Windows.Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CloseCommand { get; }
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

        protected Window()
        {
            GetType().GetMethod("InitializeComponent").Invoke(this, new object[] { });
            DataContext = this;
            ResizeMode = NoResize;
            SizeToContent = WidthAndHeight;
            CloseCommand = new DelegateCommand(Close);
        }

        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
