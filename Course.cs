using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public int Credits { get; set; }
        public string? Description { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
