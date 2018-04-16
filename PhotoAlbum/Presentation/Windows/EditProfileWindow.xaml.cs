namespace Presentation.Windows
{
    using Logic;
    using Prism.Commands;
    using System;
    using System.Windows.Input;
    using static Logic.Services;

    partial class EditProfileWindow : Window
    {
        internal event EventHandler<ProfileEditedEventArgs> ProfileEdited;

        string Password => PasswordBox.Password;
        public string FullName { get; set; }
        public string Email { get; set; }
        public ICommand EditProfileCommand { get; }

        internal EditProfileWindow(User user)
        {
            FullName = user.FullName;
            Email = user.Email;
            EditProfileCommand = new DelegateCommand(() =>
            {
                if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Email))
                {
                    ErrorMessage = "All fields are required.";
                    return;
                }

                ProfileEdited?.Invoke(this, new ProfileEditedEventArgs
                {
                    User = UsersManager.Edit(user.Id, Password, FullName.Trim(), Email.Trim())
                });
                Close();
            });
        }
    }

    class ProfileEditedEventArgs
    {
        internal User User { get; set; }
    }
}
