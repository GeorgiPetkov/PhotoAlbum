namespace Logic
{
    using Persistence.Entities;
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using static Persistence.UnitOfWork.UnitOfWorkProvider;
    using static System.Text.Encoding;

    class UsersManager : IUsersManager
    {
        public User Login(string username, string password)
        {
            return UsingUnitOfWork(unitOfWork =>
            {
                UserEntity user = unitOfWork.Users.Get(username);
                if (user == null || !ComputeHash(password).SequenceEqual(user.PasswordHash))
                {
                    return null;
                }

                return new User(user);
            });
        }

        public User Create(string username, string password, string fullName, string email)
        {
            return UsingUnitOfWork(unitOfWork =>
            {
                UserEntity user = unitOfWork.Users.Add(username, ComputeHash(password), fullName, email);
                unitOfWork.Categories.Add(username, user, null);
                unitOfWork.Complete();

                return new User(user);
            });
        }

        public User Edit(Guid id, string password, string fullName, string email)
        {
            return UsingUnitOfWork(unitOfWork =>
            {
                UserEntity user = unitOfWork.Users.Get(id);
                user.PasswordHash = ComputeHash(password);
                user.FullName = fullName;
                user.Email = email;
                unitOfWork.Complete();

                return new User(user);
            });
        }

        static byte[] ComputeHash(string input) => SHA256.Create().ComputeHash(UTF8.GetBytes(input));
    }
}
