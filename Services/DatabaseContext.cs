using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD3SQLite.Models;

namespace MD3SQLite.Services
{
    public class DatabaseContext
    {
        private readonly SQLiteAsyncConnection? _database;

        // TODO: add try/ catch blocks to handle database interaction exceptions
        public DatabaseContext(string dbPath)
        {

            // Create the database, if it doesn't already exist
            _database = new SQLiteAsyncConnection(dbPath);

            // Create the tables
            _database.CreateTableAsync<Student>().Wait();
            // _database.CreateTableAsync<Course>().Wait();
            // _database.CreateTableAsync<Assignment>().Wait();
            // _database.CreateTableAsync<Submission>().Wait();
            // _database.CreateTableAsync<Teacher>().Wait();
        }
        public Task CreateTableAsync<T>() where T : new()
        {
            return _database.CreateTableAsync<T>();
        }
        // Student CRUD operations

        // Get all students
        public Task<List<Student>> GetStudentsAsync()
        {
            return _database.Table<Student>().ToListAsync();
        }

        // Get a specific student
        public Task<Student> GetStudentAsync(int id)
        { 
            return _database.Table<Student>()
                .Where(i => i.id == id)
                .FirstOrDefaultAsync();
        }

        // Save a student
        public Task<int> SaveStudentAsync(Student student)
        {
            if (student.id != 0)
            {
                return _database.UpdateAsync(student);
            }
            else
            {
                return _database.InsertAsync(student);
            }
        }

        // Delete a student
        public Task<int> DeleteStudentAsync(Student student)
        {
            return _database.DeleteAsync(student);
        }


        // Seed test data
        public async Task SeedDataAsync()
        { 
            // Check if there are any students in the database
            var students = await GetStudentsAsync();
            if (students.Count == 0)
            {
                // Insert student test data
                var testStudents = new List<Student>
                {
                    new Student("John", "Smith", Genders.Male, "js24042"),
                    new Student("Jane", "Doe", Genders.Female, "jd24043"),
                    new Student("Robert", "Graham", Genders.Male, "rg21012"),
                    new Student("Ann", "Gale", Genders.Female, "ag23030"),
                    new Student("Gaston", "Ganu", Genders.Other, "gg24031"),
                };

                foreach (var student in testStudents)
                {
                    await SaveStudentAsync(student);
                }
            }
        }
        // Clear all data
        public async Task ClearDataAsync()
        {
            // TODO: Unhandled exception after dropping tables
            await _database.DropTableAsync<Student>();
            // await _database.DeleteAllAsync<Course>();
            // await _database.DeleteAllAsync<Assignment>();
            // await _database.DeleteAllAsync<Submission>();
            // await _database.DeleteAllAsync<Teacher>();
        }

        // Get database statistics
        public async Task<string> GetDatabaseStatisticsAsync()
        {
            var sb = new StringBuilder();

            // Get the number of students
            var studentCount = await _database.Table<Student>().CountAsync();
            sb.AppendLine($"Students: {studentCount}");

            // Get the number of teachers
            // var teacherCount = await _database.Table<Teacher>().CountAsync();
            // sb.AppendLine($"Teachers: {teacherCount}");

            // Get the number of courses
            // var courseCount = await _database.Table<Course>().CountAsync();
            // sb.AppendLine($"Courses: {courseCount}");

            // Get the number of assignments
            // var assignmentCount = await _database.Table<Assignment>().CountAsync();
            // sb.AppendLine($"Assignments: {assignmentCount}");

            // Get the number of submissions
            // var submissionCount = await _database.Table<Submission>().CountAsync();
            // sb.AppendLine($"Submissions: {submissionCount}");

            return sb.ToString();
        }
    }
}
