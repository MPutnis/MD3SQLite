using System;
using System.Collections.Generic;
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
        public Task<List<Student>> GetStudentsAsync()
        {
            return _databaseContext.GetStudentsAsync();
        }

        // Get a student by ID
        public Task<Student> GetStudentAsync(int id)
        {
            return _databaseContext.GetStudentAsync(id);
        }

        // Save a student
        public Task<int> SaveStudentAsync(Student student)
        {
            return _databaseContext.SaveStudentAsync(student);
        }

        // Delete a student
        public Task<int> DeleteStudentAsync(Student student)
        {
            return _databaseContext.DeleteStudentAsync(student);
        }
    }
}
