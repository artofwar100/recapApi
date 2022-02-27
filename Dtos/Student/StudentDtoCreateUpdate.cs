using Dtos.Bus;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Student
{
    public class StudentDtoCreateUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
        public Gender Gender { get; set; }
        public BusDto BusId { get; set; }
        public List<int> ClassesId { get; set; }
    }
}
