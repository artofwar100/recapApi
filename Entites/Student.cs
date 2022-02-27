using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
    public class Student
    {
        public Student()
        {
            Classes = new List<Class>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
        public Gender Gender { get; set; }
        public Bus Bus { get; set; }
        public List<Class> Classes { get; set; }
    }
}
