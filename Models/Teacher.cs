using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD3SQLite.Models
{
    public class Teacher : Person
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public DateTime ContractDate { get; set; }
        public Teacher(string name, string surname, Genders gender, DateTime contractDate)
            : base(name, surname, gender)
        {
            ContractDate = contractDate;
        }

        public Teacher() { }
    }
}
