namespace Persistence.Repositories
{
    using Persistence.Entities;

    public interface IUsersRepository : IRepository<UserEntity>
    {
        UserEntity Get(string username);

        UserEntity Add(string username, byte[] passwordHash, string fullName, string email);
    }
}
