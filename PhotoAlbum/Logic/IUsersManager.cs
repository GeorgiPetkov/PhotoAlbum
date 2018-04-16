namespace Logic
{
    using System;

    public interface IUsersManager
    {
        User Login(string username, string password);

        User Create(string username, string password, string fullName, string email);

        User Edit(Guid id, string password, string fullName, string email);
    }
}
