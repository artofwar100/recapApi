using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Class
{
    public class ClassDtoCreateUpdate
    {
        public int Id { get; set; }
        public string Subject { get; set; }

        public int TeacherId { get; set; }
    }
}
