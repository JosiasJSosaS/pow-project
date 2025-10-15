using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pow_project.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pow_project.Server
{
    public class MyDBContext : IdentityDbContext<User>
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("User Id=root;Host=localhost;Database=movies;");
        }   

        public DbSet<Models.Movie> Movies { get; set; }
        public DbSet<Models.Genre> Genres { get; set; }
        public DbSet<Models.MovieList> MovieLists { get; set; }
        public DbSet<Models.Comment> Comments { get; set; }
    }
}
