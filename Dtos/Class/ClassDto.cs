using Dtos.Student;
using Dtos.Teacher;
using System.Collections.Generic;

namespace Dtos.Class
{
    public class ClassDto
    {
        public ClassDto()
        {
            Students = new List<StudentDto>();
        }
        public int Id { get; set; }
        public string Subject { get; set; }

        public TeacherDto Teacher { get; set; }
        public int TeacherId { get; set; }
        public List<StudentDto> Students { get; set; }
        public List<int> StudentsId { get; set; }
    }
}
