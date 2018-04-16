namespace Presentation.Converters
{
    using Logic;

    class UserToGreetingConverter : ValueConverter<User, string>
    {
        protected override string Convert(User user) => $"Hello, {user.FullName}!";
    }
}
