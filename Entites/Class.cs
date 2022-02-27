using System.Collections.Generic;

namespace Entites
{
    public class Class
    {
        public Class()
        {
            Students = new List<Student>();
        }
        public int Id { get; set; }
        public string Subject { get; set; }

        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }
    }
}
