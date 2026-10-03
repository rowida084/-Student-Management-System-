using ConsoleApp5.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5.Data.Configurations
{
    internal class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
    {
        private List<Enrollment> LoadEnrollmentData()
        {
            return new List<Enrollment>
    {
        new Enrollment
        {
            StudentId = 1,
            CourseId = 1,
            EnrollmentDate = new DateTime(2025, 9, 5),
            Grade = 85
        },
        new Enrollment
        {
            StudentId = 1,
            CourseId = 2,
            EnrollmentDate = new DateTime(2025, 9, 5),
            Grade = 90
        },
        new Enrollment
        {
            StudentId = 2,
            CourseId = 1,
            EnrollmentDate = new DateTime(2025, 9, 6),
            Grade = 78
        },
        new Enrollment
        {
            StudentId = 2,
            CourseId = 3,
            EnrollmentDate = new DateTime(2025, 9, 6),
            Grade = null
        },
        new Enrollment
        {
            StudentId = 3,
            CourseId = 2,
            EnrollmentDate = new DateTime(2025, 9, 7),
            Grade = 88
        }
    };
        }

        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            //Composite Primary Key 
            builder.HasKey(x => new { x.StudentId, x.CourseId });

            builder.Property(x => x.Grade).IsRequired(false);

            //RelationShips 
            builder.HasOne(x => x.Student).WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Course).WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Enrollments");

            //LoadingData
            builder.HasData(LoadEnrollmentData());
        }
    }
}
