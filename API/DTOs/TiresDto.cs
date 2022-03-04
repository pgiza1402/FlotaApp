using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class TiresDto
    {
        public string Model { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public string StoragePlace { get; set; }
        public int CarId { get; set; }
        
    }
}