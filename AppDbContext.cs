using ConsoleApp5.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleApp5.Data
{
    public class AppDbContext : DbContext
    {

       public virtual DbSet<Student> Students { get; set; } 
        public virtual DbSet<Course> Courses { get; set; } 
        public virtual DbSet<Enrollment> Enrollments { get; set; } 
        public virtual DbSet<Instructor> Instructors { get; set; } //bonus

        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //var config = new ConfigurationBuilder()
            //.AddJsonFile("appsettings.json")
            //.Build();
            var connectionString =
                    "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";


            optionsBuilder.UseSqlServer(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
