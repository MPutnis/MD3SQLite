using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3SQLite.Models
{
    public class Assignment
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        [SQLite.NotNull]
        public DateTime? DeadLine { get; set; }

        [SQLite.NotNull]
        public int CourseId { get; set; }

        [SQLite.Ignore]
        public Course? Course { get; set; }

        public string? Description { get; set; }

        public Assignment(DateTime deadLine, int courseId, string description)
        {
            DeadLine = deadLine;
            CourseId = courseId;
            Description = description;
        }
        public Assignment() { }
    }
}
