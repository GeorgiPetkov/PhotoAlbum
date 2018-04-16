namespace Presentation.Pages
{
    using Prism.Commands;
    using System.Windows.Input;

    partial class LoginRegisterPage : Page
    {
        public ICommand OpenLoginPageCommand { get; }
        public ICommand OpenRegisterPageCommand { get; }

        internal LoginRegisterPage()
        {
            OpenLoginPageCommand = new DelegateCommand(() => NavigateToPage(new LoginPage()));
            OpenRegisterPageCommand = new DelegateCommand(() => NavigateToPage(new RegisterPage()));
        }
    }
}
