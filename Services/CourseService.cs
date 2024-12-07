using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD3SQLite.Models;

namespace MD3SQLite.Services
{
    public class CourseService(DatabaseContext databaseContext)
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        // Get all courses
        public Task<List<Course>> GetCoursesAsync()
        {
            return _databaseContext.GetCoursesAsync();
        }

        // Get a course by ID
        public Task<Course> GetCourseAsync(int id)
        {
            return _databaseContext.GetCourseAsync(id);
        }

        // Save a course
        public Task<int> SaveCourseAsync(Course course)
        {
            return _databaseContext.SaveCourseAsync(course);
        }

        // Delete a course
        public Task<int> DeleteCourseAsync(Course course)
        {
            return _databaseContext.DeleteCourseAsync(course);
        }
    }
}
