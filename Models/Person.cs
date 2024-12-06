using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3SQLite.Models
{
    public class Person
    {
        [SQLite.NotNull]
        public string? Name { get; set; }
        [SQLite.NotNull]
        public string? Surname { get; set; }
        [SQLite.NotNull]
        public Genders? Gender { get; set; }
        [SQLite.Ignore]
        public string FullName => $"{Name} {Surname}";
        public Person(string name, string surname, Genders gender)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
        }

        public Person() { }
    }
}
