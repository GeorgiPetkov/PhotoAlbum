namespace Logic
{
    using Persistence.Entities;
    using System;

    public class User
    {
        public Guid Id { get; }
        public string Username { get; }
        public string FullName { get; }
        public string Email { get; }

        internal User(UserEntity user)
        {
            Id = user.Id;
            Username = user.Username;
            FullName = user.FullName;
            Email = user.Email;
        }
    }
}
