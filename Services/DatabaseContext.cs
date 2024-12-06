using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD3SQLite.Models;
using System.Diagnostics;

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
             _database.CreateTableAsync<Teacher>().Wait();
        }
        // task to create table, used when clearing data
        public Task CreateTableAsync<T>() where T : new()
        {
            return _database.CreateTableAsync<T>();
        }
        // STUDENT CRUD OPERATIONS

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

        // TEACHER CRUD OPERATIONS

        // Get all teachers
        public Task<List<Teacher>> GetTeachersAsync()
        {
            return _database.Table<Teacher>().ToListAsync();
        }

        // Get a specific teacher
        public Task<Teacher> GetTeacherAsync(int id)
        {
            return _database.Table<Teacher>()
                .Where(i => i.id == id)
                .FirstOrDefaultAsync();
        }

        // Save a teacher
        public Task<int> SaveTeacherAsync(Teacher teacher)
        {
            if (teacher.id != 0)
            {
                return _database.UpdateAsync(teacher);
            }
            else
            {
                return _database.InsertAsync(teacher);
            }
        }

        // Delete a teacher
        public Task<int> DeleteTeacherAsync(Teacher teacher)
        {
            return _database.DeleteAsync(teacher);
        }

        // COURSE CRUD OPERATIONS

        // Get all courses

        // Get a specific course

        // Save a course

        // Delete a course

        // ASSIGNMENT CRUD OPERATIONS

        // Get all assignments

        // Get a specific assignment

        // Save an assignment

        // Delete an assignment

        // SUBMISSION CRUD OPERATIONS

        // Get all submissions

        // Get a specific submission

        // Save a submission

        // Delete a submission


        // Seed test data
        public async Task SeedDataAsync()
        {
            // If there are no students in database, insert test data
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

            // If there are no teachers in database, insert test data
            var teachers = await GetTeachersAsync();
            if (teachers.Count == 0)
            {
                // Insert teacher test data
                var testTeachers = new List<Teacher>
                {
                    new Teacher("Alice", "Johnson", Genders.Female, new DateTime(2020, 1, 15)),
                    new Teacher("Bob", "Williams", Genders.Male, new DateTime(2019, 3, 22)),
                    new Teacher("Catherine", "Brown", Genders.Female, new DateTime(2018, 5, 10)),
                    new Teacher("David", "Jones", Genders.Male, new DateTime(2021, 7, 30)),
                    new Teacher("Eva", "Miller", Genders.Female, new DateTime(2022, 9, 5)),
                };

                foreach (var teacher in testTeachers)
                {
                    await SaveTeacherAsync(teacher);
                }
            }

            // If there are no courses in database, insert test data

            // If there are no assignments in database, insert test data

            // If there are no submissions in database, insert test data

        }
        // Clear all data
        public async Task ClearDataAsync()
        {
            // TODO: Unhandled exception after dropping tables

            try
            {
                // Drop the child tables
                // await _database.DeleteAllAsync<Submission>();
                // await _database.DeleteAllAsync<Assignment>();
                // await _database.DeleteAllAsync<Course>();

                // Drop the parent tables
                await _database.DropTableAsync<Student>();
                await _database.DeleteAllAsync<Teacher>();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Debug.WriteLine($"Error dropping table: {ex.Message}");
            }

            // Recreate the tables 
            try
            {
                // Recreate the parent tables
                await _database.CreateTableAsync<Student>();
                await _database.CreateTableAsync<Teacher>();

                // Recreate the child tables
                // await _database.CreateTableAsync<Course>();
                // await _database.CreateTableAsync<Assignment>();
                // await _database.CreateTableAsync<Submission>();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Debug.WriteLine($"Error recreating table after deletion: {ex.Message}");
            }
           
        }

        // Get database statistics
        public async Task<string> GetDatabaseStatisticsAsync()
        {
            var sb = new StringBuilder();

            // Get the number of students
            var studentCount = await _database.Table<Student>().CountAsync();
            sb.AppendLine($"Students: {studentCount}");

            // Get the number of teachers
            var teacherCount = await _database.Table<Teacher>().CountAsync();
            sb.AppendLine($"Teachers: {teacherCount}");

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
