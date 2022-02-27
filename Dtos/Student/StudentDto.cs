using Dtos.Bus;
using Dtos.Class;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Student
{
    public class StudentDto
    {
        public StudentDto()
        {
            Classes = new List<ClassDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
        public Gender Gender { get; set; }
        public BusDto Bus { get; set; }
        public int BusId { get; set; }
        public List<ClassDto> Classes { get; set; }
        public List<int> ClassesId { get; set; }
    }
}
