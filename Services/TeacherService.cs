using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD3SQLite.Models;

namespace MD3SQLite.Services
{
    public class TeacherService(DatabaseContext databaseContext)
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        // Get all teachers
        public async Task<List<Teacher>> GetTeachersAsync()
        {
            try
            {
                return await _databaseContext.GetTeachersAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching teachers: {ex.Message}");
                throw;
            }
        }

        // Get a teacher by ID
        public async Task<Teacher> GetTeacherAsync(int id)
        {
            try
            {
                return await _databaseContext.GetTeacherAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching teacher with {id}: {ex.Message}");
                throw;
            }
        }

        // Save a teacher
        public async Task<int> SaveTeacherAsync(Teacher teacher)
        {
            try
            {
                return await _databaseContext.SaveTeacherAsync(teacher);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving teacher: {ex.Message}");
                throw;
            }
        }

        // Delete a teacher
        public async Task<int> DeleteTeacherAsync(Teacher teacher)
        {
            try
            {
                return await _databaseContext.DeleteTeacherAsync(teacher);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting teacher: {ex.Message}");
                throw;
            }
        }
    }
}
