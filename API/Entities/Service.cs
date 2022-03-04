using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Service
    {
       public int Id { get; set; } 
       [Required]
       public DateTime ServiceExpirationDate { get; set; }
       [Required]
       public int NextServiceMeterStatus { get; set; }
       [Required]
       
       public int CarId { get; set; }
       [Required]
       public Car Car { get; set; }
    }
}