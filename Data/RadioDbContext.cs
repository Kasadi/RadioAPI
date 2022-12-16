using Microsoft.EntityFrameworkCore;
using RadioAPI.Model;


namespace RadioAPI.Data
{
    public class RadioDbContext : DbContext
    {
        public RadioDbContext(DbContextOptions<RadioDbContext> options) : base(options)
        {
        }




        public DbSet<Station>? Station { get; set; }
        public DbSet<Show>? Show { get; set; }
        public DbSet<User>? User { get; set; }
        public DbSet<AnonymUser>? AnonymUser { get; set; }

    }
}
