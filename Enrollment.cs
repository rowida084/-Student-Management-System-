using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5.Entities
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }  
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public int? Grade { get; set; } = null;// Nullable to allow for courses without a grade yet
      public  Student Student { get; set; } = null!;
      public  Course Course { get; set; } = null!;
    }
}
