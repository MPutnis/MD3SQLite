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
            try
            {
                // Create the database, if it doesn't already exist
                _database = new SQLiteAsyncConnection(dbPath);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Debug.WriteLine($"Error creating database: {ex.Message}");
                throw;
            }
            try
            {
                // Enable foreign key constraints
                _database.ExecuteAsync("PRAGMA foreign_keys = ON;");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error enabling foreign key constraints: {ex.Message}");
                throw;
            }

            try
            {
                // Create the tables
                _database.CreateTableAsync<Student>().Wait();
                _database.CreateTableAsync<Teacher>().Wait();
                _database.CreateTableAsync<Course>().Wait();
                _database.CreateTableAsync<Assignment>().Wait();
                _database.CreateTableAsync<Submission>().Wait();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating database tables: {ex.Message}");
                throw;
            }
            
             
        }
        // task template to create table
        public Task CreateTableAsync<T>() where T : new()
        {
            return _database.CreateTableAsync<T>();
        }


        // STUDENT CRUD OPERATIONS

        // Get all students
        public async Task<List<Student>> GetStudentsAsync()
        {
            try
            {
                return await _database.Table<Student>().ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting students: {ex.Message}");
                throw;
            }
        }

        // Get a specific student
        public async Task<Student> GetStudentAsync(int id)
        {
            try
            {
                return await _database.Table<Student>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting student with ID {id}: {ex.Message}");
                throw;
            }
        }

        // Save a student
        public async Task<int> SaveStudentAsync(Student student)
        {
            try
            {
                if (student.Id != 0)
                {
                    return await _database.UpdateAsync(student);
                }
                else
                {
                    return await _database.InsertAsync(student);
                }
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
                return await _database.DeleteAsync(student);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting student: {ex.Message}");
                throw;
            }
            
        }


        // TEACHER CRUD OPERATIONS

        // Get all teachers
        public async Task<List<Teacher>> GetTeachersAsync()
        {
            try
            {
                return await _database.Table<Teacher>().ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching teachers: {ex.Message}");
                throw;
            }
        }

        // Get a specific teacher
        public async Task<Teacher> GetTeacherAsync(int id)
        {
            try
            {
                return await _database.Table<Teacher>()
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching teacher with ID {id}: {ex.Message}");
                throw;
            }
        }

        // Save a teacher
        public async Task<int> SaveTeacherAsync(Teacher teacher)
        {
            try
            {
                if (teacher.Id != 0)
                {
                    return await _database.UpdateAsync(teacher);
                }
                else
                {
                    return await _database.InsertAsync(teacher);
                }
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
                return await _database.DeleteAsync(teacher);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting teacher: {ex.Message}");
                throw;
            }
        }


        // COURSE CRUD OPERATIONS

        // Get all courses
        public async Task<List<Course>> GetCoursesAsync()
        {
            try
            {
                return await _database.Table<Course>().ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching courses: {ex.Message}");
                throw;
            }
        }

        // Get a specific course
        public async Task<Course> GetCourseAsync(int id)
        {
            try
            {
                return await _database.Table<Course>()
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching course with ID {id}: {ex.Message}");
                throw;
            }
        }

        // Save a course
        public async Task<int> SaveCourseAsync(Course course)
        {
            try
            {
                if (course.Id != 0)
                {
                    return await _database.UpdateAsync(course);
                }
                else
                {
                    return await _database.InsertAsync(course);
                }
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
                return await _database.DeleteAsync(course);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting course: {ex.Message}");
                throw;
            }
        }


        // ASSIGNMENT CRUD OPERATIONS

        // Get all assignments
        public async Task<List<Assignment>> GetAssignmentsAsync()
        {
            try
            {
                return await _database.Table<Assignment>().ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching assignments: {ex.Message}");
                throw;
            }
        }

        // Get a specific assignment
        public async Task<Assignment> GetAssignmentAsync(int id)
        {
            try
            {
                return await _database.Table<Assignment>()
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching assignment with ID {id}: {ex.Message}");
                throw;
            }
        }

        // Save an assignment
        public async Task<int> SaveAssignmentAsync(Assignment assignment)
        {
            try
            {
                if (assignment.Id != 0)
                {
                    return await _database.UpdateAsync(assignment);
                }
                else
                {
                    return await _database.InsertAsync(assignment);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving assignment: {ex.Message}");
                throw;
            }
        }

        // Delete an assignment
        public async Task<int> DeleteAssignmentAsync(Assignment assignment)
        {
            try
            {
                return await _database.DeleteAsync(assignment);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting assignment: {ex.Message}");
                throw;
            }
        }

        // Get assignments by course ID
        public async Task<List<Assignment>> GetAssignmentsByCourseIdAsync(int courseId)
        {
            try
            {
                return await _database.Table<Assignment>()
                    .Where(i => i.CourseId == courseId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching assignments by course ID: {ex.Message}");
                throw;
            }
        }


        // SUBMISSIONS CRUD OPERATIONS

        // Get all submissions
        public async Task<List<Submission>> GetSubmissionsAsync()
        {
            try
            {
                return await _database.Table<Submission>().ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching submissions: {ex.Message}");
                throw;
            }
        }

        // Get a specific submission
        public async Task<Submission> GetSubmissionAsync(int id)
        {
            try
            {
                return await _database.Table<Submission>()
                    .Where(i => i.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching submission with ID {id}: {ex.Message}");
                throw;
            }
        }
        // Save a submission
        public async Task<int> SaveSubmissionAsync(Submission submission)
        {
            try
            {
                if (submission.Id != 0)
                {
                    return await _database.UpdateAsync(submission);
                }
                else
                {
                    return await _database.InsertAsync(submission);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving submission: {ex.Message}");
                throw;
            }
        }
        // Delete a submission
        public async Task<int> DeleteSubmissionAsync(Submission submission)
        {
            try
            {
                return await _database.DeleteAsync(submission);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting submission: {ex.Message}");
                throw;
            }
        }

        // Seed test data
        public async Task SeedDataAsync()
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine($"Error seeding data: {ex.Message}");
                throw;
            }
        }

        // Clear all data
        public async Task ClearDataAsync()
        {
            // Done: Unhandled exception after dropping table
            try
            {
                // Drop the child tables
                 await _database.DeleteAllAsync<Submission>();
                 await _database.DeleteAllAsync<Assignment>();
                 await _database.DeleteAllAsync<Course>();

                // Drop the parent tables
                await _database.DropTableAsync<Student>();
                await _database.DeleteAllAsync<Teacher>();
            
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
                // Log the exception
                Debug.WriteLine($"Error clearing data: {ex.Message}");
                throw;
            }           
        }

        // Get database statistics
        public async Task<string> GetDatabaseStatisticsAsync()
        {
            var sb = new StringBuilder();
            try
            {
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting database statistics: {ex.Message}");
                throw;
            }            

            return sb.ToString();
        }
    }
}
