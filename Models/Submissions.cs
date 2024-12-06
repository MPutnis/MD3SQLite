using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3SQLite.Models
{
    public class Submissions
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int id { get; set; }
        [SQLite.NotNull]
        public int AssignmentId { get; set; }
        [SQLite.NotNull]
        public int StudentId { get; set; }
        public DateTime? SubmissionTime { get; set; }
        public int? Score { get; set; }
        public Submissions(int score, DateTime submissionTime, int assignmentId, int studentId)
        {
            Score = score;
            SubmissionTime = submissionTime;
            AssignmentId = assignmentId;
            StudentId = studentId;
        }
        public Submissions() { }
    }
}
