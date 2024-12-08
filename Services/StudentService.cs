using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MD3SQLite.Models;

namespace MD3SQLite.Services
{
    public class StudentService(DatabaseContext databaseContext)
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        // Get all students
        public async Task<List<Student>> GetStudentsAsync()
        {
            try
            {
                return await _databaseContext.GetStudentsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching students: {ex.Message}");
                throw;
            }
        }

        // Get a student by ID
        public async Task<Student> GetStudentAsync(int id)
        {
            try
            {
                return await _databaseContext.GetStudentAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching student with {id}: {ex.Message}");
                throw;
            }
        }

        // Save a student
        public async Task<int> SaveStudentAsync(Student student)
        {
            try
            {
                return await _databaseContext.SaveStudentAsync(student);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving student: {ex.Message}");
                throw;
            }
        }

        // Delete a student
        public async Task<int> DeleteStudentAsync(Student student)
        {
            try
            {
                return await _databaseContext.DeleteStudentAsync(student);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting student: {ex.Message}");
                throw;
            }
        }
    }
}
