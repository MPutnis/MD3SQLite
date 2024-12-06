using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD3SQLite.Models;

namespace MD3SQLite.Services
{
    public class TeacherService
    {
        private readonly DatabaseContext _databaseContext;

        public TeacherService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // Get all teachers
        public Task<List<Teacher>> GetTeachersAsync()
        {
            return _databaseContext.GetTeachersAsync();
        }

        // Get a teacher by ID
        public Task<Teacher> GetTeacherAsync(int id)
        {
            return _databaseContext.GetTeacherAsync(id);
        }

        // Save a teacher
        public Task<int> SaveTeacherAsync(Teacher teacher)
        {
            return _databaseContext.SaveTeacherAsync(teacher);
        }

        // Delete a teacher
        public Task<int> DeleteTeacherAsync(Teacher teacher)
        {
            return _databaseContext.DeleteTeacherAsync(teacher);
        }
    }
}
