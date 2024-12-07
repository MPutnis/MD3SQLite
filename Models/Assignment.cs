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
        [SQLite.Ignore]
        public string CourseName => Course != null ? Course.Name : string.Empty;
        public string? Description { get; set; }

        public Assignment(DateTime deadLine, Course course, string description)
        {
            DeadLine = deadLine;
            Course = course;
            CourseId = course.Id;
            Description = description;
        }
        public Assignment() { }
    }
}
