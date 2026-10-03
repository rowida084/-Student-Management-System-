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
    internal class StudentConfig: IEntityTypeConfiguration<Student>
    {
        private List<Student> LoadStudentData()
        {
            return new List<Student>
    {
        new Student
        {
            StudentId = 1,
            FullName = "Ahmed Ali",
            Email = "ahmed.ali@example.com",
            DateOfBirth = new DateTime(2003, 5, 10),
            EnrollmentDate = new DateTime(2025, 9, 1)
        },
        new Student
        {
            StudentId = 2,
            FullName = "Sara Mohamed",
            Email = "sara.mohamed@example.com",
            DateOfBirth = new DateTime(2004, 2, 15),
            EnrollmentDate = new DateTime(2025, 9, 1)
        },
        new Student
        {
            StudentId = 3,
            FullName = "Omar Hassan",
            Email = "omar.hassan@example.com",
            DateOfBirth = new DateTime(2003, 11, 20),
            EnrollmentDate = new DateTime(2025, 9, 2)
        }
    };
        }

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //Id Constrains
            builder.HasKey(x => x.StudentId);
            builder.Property(x => x.StudentId).ValueGeneratedOnAdd().IsRequired();

            //FullName Constrains
            builder.Property(x=>x.FullName).HasMaxLength(150).IsRequired();

            //Email Constrains
            builder.HasIndex(x=>x.Email).IsUnique();
            builder.Property(x => x.Email).HasMaxLength(150).IsRequired();

            //EnrollmentDate Constrain
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_Student_EnrollmentDate" ,
                "EnrollmentDate<=GetDate()"
                ));

            //Relationships 
            builder.HasMany(x=>x.Enrollments)
                .WithOne(x=>x.Student).HasForeignKey(x=>x.StudentId);

            builder.ToTable("Students");

            //Loading Data
            builder.HasData(LoadStudentData());
        }
    }
}
