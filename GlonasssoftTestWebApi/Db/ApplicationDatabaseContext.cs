using GlonasssoftTestWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlonasssoftTestWebApi.Db
{
    public class ApplicationDatabaseContext:DbContext
    {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Report> Reports { get; set; }

        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
        {
            
        }

        public ApplicationDatabaseContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
