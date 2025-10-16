using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pow_project.Server.Models;

using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace pow_project.Server
{
    public class MyDBContext : IdentityDbContext<User>
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) {}

        // Fallback por si no se configuró por DI en Program.cs
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // ¡Usá un connection string completo!
                // OJO: completá la contraseña real si aplica.
                var cs = "Server=localhost;Database=movies;User Id=root;Password=;";
                optionsBuilder.UseMySql(cs, ServerVersion.AutoDetect(cs));
            }
        }

        public DbSet<Movie> Movies { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<MovieList> MovieLists { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
    }
}
