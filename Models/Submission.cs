using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3SQLite.Models
{
    public class Submission
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        [SQLite.NotNull]
        public int AssignmentId { get; set; }

        [SQLite.Ignore]
        public Assignment? Assignment { get; set; }
        [SQLite.Ignore]
        public string AssignmentName => 
            Assignment != null ? Assignment.Description : string.Empty;

        [SQLite.NotNull]
        public int StudentId { get; set; }

        [SQLite.Ignore]
        public string StudentNameId => 
            Student != null ? $"{Student.FullName}, {Student.StudentIdNumber}" : string.Empty;
        [SQLite.Ignore]
        public Student? Student { get; set; }

        public DateTime? SubmissionTime { get; set; }
        public int? Score { get; set; }
        public Submission(int score, DateTime submissionTime, Assignment assignment, Student student)
        {
            Score = score;
            SubmissionTime = submissionTime;
            Assignment = assignment;
            Student = student;
            AssignmentId = assignment.Id;
            StudentId = student.Id;
        }
        public Submission() { }
    }
}
