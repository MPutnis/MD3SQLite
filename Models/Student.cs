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
        public int id { get; set; }

        [SQLite.NotNull]
        public string? StudentIdNumber { get; set; }

        public Student(string name, string surname, Genders gender, string studentIdNumber) 
            : base(name, surname, gender)
        { 
            StudentIdNumber = studentIdNumber; 
        }

        public Student() { }
    }
}
