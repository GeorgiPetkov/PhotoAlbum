namespace Presentation.Pages
{
    using Logic;
    using Prism.Commands;
    using System.Windows.Input;
    using static Logic.Services;

    partial class RegisterPage : Page
    {
        public string Username { private get; set; }
        string Password => PasswordBox.Password;
        public string FullName { private get; set; }
        public string Email { private get; set; }
        public ICommand RegisterCommand { get; }

        internal RegisterPage()
        {
            RegisterCommand = new DelegateCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrEmpty(Password) ||
                string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Email))
                {
                    ErrorMessage = "All fields are required.";
                    return;
                }

                User user = UsersManager.Create(Username.Trim(), Password, FullName.Trim(), Email.Trim());

                NavigateToPage(new UserPage(user));
            });
        }
    }
}
