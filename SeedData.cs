using ConsoleApp5.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5.Data
{
    public static class SeedData
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Students.Any()) return;

            var students = new List<Student>
        {
            new()
            {
                FullName = "Nour Hassan",
                Email = "nour@demo.com",
                DateOfBirth = new DateTime(2002, 7, 14),
                EnrollmentDate = DateTime.Today
            },
            new()
            {
                FullName = "Youssef Samir",
                Email = "youssef@demo.com",
                DateOfBirth = new DateTime(2001, 11, 3),
                EnrollmentDate = DateTime.Today
            },
            new()
            {
                FullName = "Mariam Adel",
                Email = "mariam@demo.com",
                DateOfBirth = new DateTime(2003, 2, 25),
                EnrollmentDate = DateTime.Today
            }
        };

            var courses = new List<Course>
        {
            new() { Title = "C# Fundamentals", Credits = 3 },
            new() { Title = "SQL Server", Credits = 4 },
            new() { Title = "Entity Framework Core", Credits = 3 }
        };

            context.Students.AddRange(students);
            context.Courses.AddRange(courses);
            context.SaveChanges();

            context.Enrollments.AddRange(
                new Enrollment
                {
                    StudentId = students[0].StudentId,
                    CourseId = courses[0].CourseId,
                    EnrollmentDate = DateTime.Today,
                    Grade = 82
                },
                new Enrollment
                {
                    StudentId = students[0].StudentId,
                    CourseId = courses[2].CourseId,
                    EnrollmentDate = DateTime.Today
                },
                new Enrollment
                {
                    StudentId = students[1].StudentId,
                    CourseId = courses[1].CourseId,
                    EnrollmentDate = DateTime.Today,
                    Grade = 91
                },
                new Enrollment
                {
                    StudentId = students[2].StudentId,
                    CourseId = courses[0].CourseId,
                    EnrollmentDate = DateTime.Today,
                    Grade = 76
                }
            );

            context.SaveChanges();
        }
    }
}
