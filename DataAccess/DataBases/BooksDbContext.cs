using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DataBase;

namespace DataAccess.DataBases
{
    public class BooksDbContext : DbContext
    {
        public DbSet<DbBook> Books { get; set; }
        public DbSet<DbAuthorBook> Authors { get; set; }

        public BooksDbContext(DbContextOptions<BooksDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = String.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connection = "Server=localhost\\sqlexpress;Database=test;Trusted_Connection=True;";
            }
            else if (
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connection = "Server=localhost;Database=t0;User=sa;Password=password123;Trusted_Connection=False;";
            }
            
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbAuthorBook>().HasIndex(p => new {p.BookId, p.Author}).IsUnique();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}