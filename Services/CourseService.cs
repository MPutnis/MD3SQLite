using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<List<Course>> GetCoursesAsync()
        {
            try
            {
                return await _databaseContext.GetCoursesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching courses: {ex.Message}");
                throw;
            }
            
        }

        // Get a course by ID
        public async Task<Course> GetCourseAsync(int id)
        {
            try
            {
                return await _databaseContext.GetCourseAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching course with {id}: {ex.Message}");
                throw;
            }
        }

        // Save a course
        public async Task<int> SaveCourseAsync(Course course)
        {
            try
            {
                return await _databaseContext.SaveCourseAsync(course);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving course: {ex.Message}");
                throw;
            }
        }

        // Delete a course
        public async Task<int> DeleteCourseAsync(Course course)
        {
            try
            {
                var assignments = await _databaseContext.GetAssignmentsByCourseIdAsync(course.Id);
                if (assignments.Count > 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Cannot delete course with assignments", "OK");
                    throw new InvalidOperationException("Cannot delete course with assignments");
                }
                return await _databaseContext.DeleteCourseAsync(course);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting course: {ex.Message}");
                throw;
            }
            
        }
    }
}
