using GlonasssoftTestWebApi.Db;
using GlonasssoftTestWebApi.Entities;

namespace GlonasssoftTestWebApi.Services
{
    public class UsersService
    {

        private readonly ApplicationDatabaseContext db;

        public UsersService(ApplicationDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<Guid> CreateUserAsync()
        {
            var usr = new User();
            usr.Id = Guid.NewGuid();

            await db.Users.AddAsync(usr);
            await db.SaveChangesAsync();

            return usr.Id;
        }
    }
}
