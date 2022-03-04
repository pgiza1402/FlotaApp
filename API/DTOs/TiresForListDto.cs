using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class TiresForListDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public string StoragePlace { get; set; }
        
        public string Car { get; set; }
        public string CarRegistrationNumber { get; set; }
    }
}