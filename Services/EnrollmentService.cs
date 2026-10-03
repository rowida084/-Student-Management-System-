using ConsoleApp5.Data;
using ConsoleApp5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5.Service
{
    public class EnrollmentService
    {
        private readonly AppDbContext _context;

        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> GetStudentsByCourseAsync(int courseId)
        {
            return await _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.Student)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<(Course Course, int? Grade)>> GetCoursesByStudentAsync(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => new ValueTuple<Course, int?>(e.Course, e.Grade))
                .ToListAsync();
        }

        public async Task<double?> GetAverageGradePerCourseAsync(int courseId)
        {
            return await _context.Enrollments
                .Where(e => e.CourseId == courseId && e.Grade.HasValue)
                .AverageAsync(e => (double?)e.Grade);
        }
    }
}
