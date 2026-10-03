using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5.Entities
{
    public class Instructor
    {
        public int InstructorId { get; set; }   
        public string? FullName { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
