using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; } 
        public DateTime EnrollmentDate { get; set; } 
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


    }
}
