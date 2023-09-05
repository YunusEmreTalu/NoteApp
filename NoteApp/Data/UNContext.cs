using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NoteApp.Models;

namespace NoteApp.Data
{
    public class UNContext : IdentityDbContext<User>
    {
        public UNContext(DbContextOptions<UNContext> options) : base(options)
        {
            
        }

        public DbSet<Images> Images { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Identity Framework tablolarının adlarını yapılandırma
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }

    }
}
