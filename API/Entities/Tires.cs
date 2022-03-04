using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Tires
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Model { get; set; }
        public int Quantity { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string StoragePlace { get; set; }
        [Required]
         public int CarId { get; set; }
         [Required]
         public Car Car { get; set; }
    }
}