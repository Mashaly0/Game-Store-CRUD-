using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameDevices> GameDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
            .HasData(new Category[]
        {
         new Category { Id = 1, Name = "Sports" },
        new Category { Id = 2, Name = "Action" },
        new Category { Id = 3, Name = "Adventure" },
        new Category { Id = 4, Name = "Racing" },
        new Category { Id = 5, Name = "Fighting" },
        new Category { Id = 6, Name = "Film" },
     });

            modelBuilder.Entity<Devices>()
                .HasData(new Devices[]
                {
        new Devices { Id = 1, Name = "PlayStation", Icon = "bi bi-playstation" },
        new Devices { Id = 2, Name = "Xbox", Icon = "bi bi-xbox" },
        new Devices { Id = 3, Name = "Nintendo Switch", Icon = "bi bi-nintendo-switch" },
        new Devices { Id = 4, Name = "PC", Icon = "bi bi-pc-display" }
                });


            modelBuilder.Entity<GameDevices>()
                .HasKey(e => new { e.GameId, e.DevicesId });

            base.OnModelCreating(modelBuilder);
        }




    }

}