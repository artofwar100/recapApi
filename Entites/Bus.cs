using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
    public class Bus
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public FuelType FuelType { get; set; }
    }
}
