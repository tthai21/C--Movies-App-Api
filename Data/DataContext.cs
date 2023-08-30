using Microsoft.EntityFrameworkCore;

namespace C__Movies_App_Api.Data
{
    public class DataContext : DbContext
    {
        // public DataContext(DbContextOptions<DataContext> options) : base(options)
        // {

        // }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server= .\\SQLEXPRESS;Database=Users;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

    }

}