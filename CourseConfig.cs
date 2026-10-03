using ConsoleApp5.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5.Data.Configurations
{
    internal class CourseConfig : IEntityTypeConfiguration<Course>
    {
        private List<Course> LoadCourseData()
        {
            return new List<Course>
         {
        new Course
        {
            CourseId = 1,
            Title = "C# Programming",
            Credits = 3,
            Description = "Introduction to C# programming"
        },
        new Course
        {
            CourseId = 2,
            Title = "Database Systems",
            Credits = 3,
            Description = "Introduction to relational databases"
        },
        new Course
        {
            CourseId = 3,
            Title = "Entity Framework Core",
            Credits = 3,
            Description = "Working with EF Core and databases"
        }
    };
        }

        public void Configure(EntityTypeBuilder<Course>builder)
        {
            //Id 
            builder.HasKey(x=>x.CourseId);
            builder.Property(x => x.CourseId).IsRequired().ValueGeneratedOnAdd();

            //Title
            builder.Property(x => x.Title).IsRequired();

            //Credits
            builder.Property(x=>x.Credits).IsRequired();

            //Description
            builder.Property(x => x.Description).HasMaxLength(250);

            //Relationships
            builder.HasMany(x => x.Instructors)
                .WithMany(x => x.Courses);

            builder.ToTable("Courses");

            //Loading Data
            builder.HasData(LoadCourseData());
        }
    }
}
