using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3SQLite.Models
{
    public class Student : Person
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        [SQLite.NotNull, SQLite.Unique]
        public string? StudentIdNumber { get; set; }

        public Student(string name, string surname, Genders gender, string studentIdNumber) 
            : base(name, surname, gender)
        { 
            StudentIdNumber = studentIdNumber; 
        }

        public Student() { }
    }
}
