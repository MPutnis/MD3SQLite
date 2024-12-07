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

        [SQLite.NotNull]
        public int StudentId { get; set; }

        [SQLite.Ignore]
        public Student? Student { get; set; }

        public DateTime? SubmissionTime { get; set; }
        public int? Score { get; set; }
        public Submission(int score, DateTime submissionTime, int assignmentId, int studentId)
        {
            Score = score;
            SubmissionTime = submissionTime;
            AssignmentId = assignmentId;
            StudentId = studentId;
        }
        public Submission() { }
    }
}
