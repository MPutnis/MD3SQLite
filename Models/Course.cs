using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3SQLite.Models
{
    public class Course
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        [SQLite.NotNull]
        public string? Name { get; set; }
        [SQLite.NotNull]
        public int? TeacherId { get; set; }

        [SQLite.Ignore]
        public Teacher? Teacher { get; set; }
        public Course(string name, int teacherId)
        {
            Name = name;
            TeacherId = teacherId;
        }
        public Course() { }
    }
}
