namespace Persistence.Repositories
{
    using Persistence.Entities;
    using System.Linq;

    class UsersRepository : Repository<UserEntity>, IUsersRepository
    {
        internal UsersRepository(PhotoAlbumDbContext context) : base(context) { }

        public UserEntity Get(string username) => Set.SingleOrDefault(u => u.Username == username);

        public UserEntity Add(string username, byte[] passwordHash, string fullName, string email)
        {
            return Set.Add(new UserEntity
            {
                Username = username,
                PasswordHash = passwordHash,
                FullName = fullName,
                Email = email
            });
        }
    }
}
