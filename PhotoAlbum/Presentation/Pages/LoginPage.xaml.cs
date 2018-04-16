namespace Presentation.Pages
{
    using Logic;
    using Prism.Commands;
    using System.Windows.Input;
    using static Logic.Services;

    partial class LoginPage : Page
    {
        public string Username { private get; set; }
        string Password => PasswordBox.Password;
        public ICommand LoginCommand { get; }

        internal LoginPage()
        {
            LoginCommand = new DelegateCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrEmpty(Password))
                {
                    ErrorMessage = "Both username and password should be provided.";
                    return;
                }

                User user = UsersManager.Login(Username.Trim(), Password);
                if (user == null)
                {
                    ErrorMessage = "Invalid user/password combination.";
                    return;
                }

                NavigateToPage(new UserPage(user));
            });
        }
    }
}
