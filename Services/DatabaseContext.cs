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
        private readonly SQLiteAsyncConnection _database;

        // TODO: add try/ catch blocks to handle database interaction exceptions
        public DatabaseContext(string dbPath)
        {

            // Create the database, if it doesn't already exist
            _database = new SQLiteAsyncConnection(dbPath);

            // Enable foreign key constraints
            _database.ExecuteAsync("PRAGMA foreign_keys = ON;");

            // Create the tables
            _database.CreateTableAsync<Student>().Wait();
            _database.CreateTableAsync<Teacher>().Wait();
            _database.CreateTableAsync<Course>().Wait();
            _database.CreateTableAsync<Assignment>().Wait();
            _database.CreateTableAsync<Submission>().Wait();
             
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
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        // Save a student
        public Task<int> SaveStudentAsync(Student student)
        {
            if (student.Id != 0)
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
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        // Save a teacher
        public Task<int> SaveTeacherAsync(Teacher teacher)
        {
            if (teacher.Id != 0)
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
        public Task<List<Course>> GetCoursesAsync()
        {
            return _database.Table<Course>().ToListAsync();
        }
        // Get a specific course
        public Task<Course> GetCourseAsync(int id)
        {
            return _database.Table<Course>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }
        // Save a course
        public Task<int> SaveCourseAsync(Course course)
        {
            if (course.Id != 0)
            {
                return _database.UpdateAsync(course);
            }
            else
            {
                return _database.InsertAsync(course);
            }
        }
        // Delete a course
        public Task<int> DeleteCourseAsync(Course course)
        {
            return _database.DeleteAsync(course);
        }
        // ASSIGNMENT CRUD OPERATIONS

        // Get all assignments
        public Task<List<Assignment>> GetAssignmentsAsync()
        {
            return _database.Table<Assignment>().ToListAsync();
        }
        // Get a specific assignment
        public Task<Assignment> GetAssignmentAsync(int id)
        {
            return _database.Table<Assignment>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }
        // Save an assignment
        public Task<int> SaveAssignmentAsync(Assignment assignment)
        {
            if (assignment.Id != 0)
            {
                return _database.UpdateAsync(assignment);
            }
            else
            {
                return _database.InsertAsync(assignment);
            }
        }
        // Delete an assignment
        public Task<int> DeleteAssignmentAsync(Assignment assignment)
        {
            return _database.DeleteAsync(assignment);
        }
        // SUBMISSIONS CRUD OPERATIONS

        // Get all submissions
        public Task<List<Submission>> GetSubmissionsAsync()
        {
            return _database.Table<Submission>().ToListAsync();
        }
        // Get a specific submission
        public Task<Submission> GetSubmissionAsync(int id)
        {
            return _database.Table<Submission>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }
        // Save a submission
        public Task<int> SaveSubmissionAsync(Submission submission)
        {
            if (submission.Id != 0)
            {
                return _database.UpdateAsync(submission);
            }
            else
            {
                return _database.InsertAsync(submission);
            }
        }
        // Delete a submission
        public Task<int> DeleteSubmissionAsync(Submission submission)
        {
            return _database.DeleteAsync(submission);
        }

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
                    new("John", "Smith", Genders.Male, "js24042"),
                    new("Jane", "Doe", Genders.Female, "jd24043"),
                    new("Robert", "Graham", Genders.Male, "rg21012"),
                    new("Ann", "Gale", Genders.Female, "ag23030"),
                    new("Gaston", "Ganu", Genders.Other, "gg24031"),
                };

                foreach (var student in testStudents)
                {
                    await SaveStudentAsync(student);
                }

                // Refresh the students list
                students = await GetStudentsAsync();
            }

            // If there are no teachers in database, insert test data
            var teachers = await GetTeachersAsync();
            if (teachers.Count == 0)
            {
                // Insert teacher test data
                var testTeachers = new List<Teacher>
                {
                    new("Alice", "Johnson", Genders.Female, new DateTime(2020, 1, 15)),
                    new("Bob", "Williams", Genders.Male, new DateTime(2019, 3, 22)),
                    new("Catherine", "Brown", Genders.Female, new DateTime(2018, 5, 10)),
                    new("David", "Jones", Genders.Male, new DateTime(2021, 7, 30)),
                    new("Eva", "Miller", Genders.Female, new DateTime(2022, 9, 5)),
                };

                foreach (var teacher in testTeachers)
                {
                    await SaveTeacherAsync(teacher);
                }

                // Refresh the teachers list
                teachers = await GetTeachersAsync();
            }

            // If there are no courses in database, insert test data
            var courses = await GetCoursesAsync();
            if (courses.Count == 0)
            {
                // Insert course test data
                var testCourses = new List<Course>
                {
                    new("Mathematics", teachers[0]),
                    new("English", teachers[1]),
                    new("Science",teachers[2]),
                    new("History",teachers[3]),
                    new("Art",teachers[4])
                };
                foreach (var course in testCourses)
                {
                    await SaveCourseAsync(course);
                }

                // Refresh the courses list
                courses = await GetCoursesAsync();
            }
            // If there are no assignments in database, insert test data
            var assignments = await GetAssignmentsAsync();
            if (assignments.Count == 0)
            {
                // Insert assignment test data
                var testAssignments = new List<Assignment>
                {
                    new(new DateTime(2022, 1, 15), courses[0], "Mathematics Assignment 1"),
                    new(new DateTime(2022, 2, 15), courses[1], "English Assignment 1"),
                    new(new DateTime(2022, 3, 15), courses[2], "Science Assignment 1"),
                    new(new DateTime(2022, 4, 15), courses[3], "History Assignment 1"),
                    new(new DateTime(2022, 5, 15), courses[4], "Art Assignment 1"),
                    new(new DateTime(2023, 1, 15), courses[0], "Mathematics Assignment 2"),
                    new(new DateTime(2023, 2, 15), courses[1], "English Assignment 2"),
                    new(new DateTime(2023, 3, 15), courses[2], "Science Assignment 2"),
                    new(new DateTime(2023, 4, 15), courses[3], "History Assignment 2"),
                    new(new DateTime(2023, 5, 15), courses[4], "Art Assignment 2"),
                };
                foreach (var assignment in testAssignments)
                {
                    await SaveAssignmentAsync(assignment);
                }

                // Refresh the assignments list
                assignments = await GetAssignmentsAsync();
            }
            // If there are no submissions in database, insert test data
            var submissions = await GetSubmissionsAsync();
            if (submissions.Count == 0)
            {
                // Insert submission test data
                var testSubmissions = new List<Submission>
                {
                    new(57, new DateTime(2022, 1, 15), assignments[0], students[0]),
                    new(78, new DateTime(2022, 2, 15), assignments[1], students[1]),
                    new(89, new DateTime(2022, 3, 15), assignments[2], students[2]),
                    new(90, new DateTime(2022, 4, 15), assignments[3], students[3]),
                    new(100, new DateTime(2022, 5, 15), assignments[4], students[4]),
                    new(67, new DateTime(2023, 1, 15), assignments[5], students[0]),
                    new(88, new DateTime(2023, 2, 15), assignments[6], students[1]),
                    new(90, new DateTime(2023, 3, 15), assignments[7], students[2]),
                    new(95, new DateTime(2023, 4, 15), assignments[8], students[3]),
                    new(100, new DateTime(2023, 5, 15), assignments[9], students[4]),

                };
                foreach (var submission in testSubmissions)
                {
                    await SaveSubmissionAsync(submission);
                }
            }
        }
        // Clear all data
        public async Task ClearDataAsync()
        {
            // TODO: Unhandled exception after dropping tables

            try
            {
                // Drop the child tables
                 await _database.DeleteAllAsync<Submission>();
                 await _database.DeleteAllAsync<Assignment>();
                 await _database.DeleteAllAsync<Course>();

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
                await _database.CreateTableAsync<Course>();
                await _database.CreateTableAsync<Assignment>();
                await _database.CreateTableAsync<Submission>();
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
            var courseCount = await _database.Table<Course>().CountAsync();
            sb.AppendLine($"Courses: {courseCount}");

            // Get the number of assignments
             var assignmentCount = await _database.Table<Assignment>().CountAsync();
            sb.AppendLine($"Assignments: {assignmentCount}");

            // Get the number of submissions
             var submissionCount = await _database.Table<Submission>().CountAsync();
            sb.AppendLine($"Submissions: {submissionCount}");

            return sb.ToString();
        }
    }
}
