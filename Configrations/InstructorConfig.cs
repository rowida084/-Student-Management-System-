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
    internal class InstructorConfig : IEntityTypeConfiguration<Instructor>
    {
      private List<Instructor> LoadInstructorData()
        {
            return new List<Instructor>
    {
        new Instructor
        {
            InstructorId = 1,
            FullName = "Dr. Ahmed Hassan"
        },
        new Instructor
        {
            InstructorId = 2,
            FullName = "Dr. Mona Ali"
        },
        new Instructor
        {
            InstructorId = 3,
            FullName = "Dr. Karim Mohamed"
        }
    };
        }

     public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            //ID
            builder.HasKey(x => x.InstructorId);
            builder.Property(x => x.InstructorId).IsRequired().ValueGeneratedOnAdd();

            //FullName
            builder.Property(x => x.FullName).IsRequired();

            //Relationships 
            builder.HasMany(x => x.Courses)
                .WithMany(x => x.Instructors);

            builder.ToTable("Instructors");


            //Loading Data
            builder.HasData(LoadInstructorData());
        }
    }
}
