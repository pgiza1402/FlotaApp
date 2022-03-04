using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Car
    {
       public int Id { get; set; } 
       [Required]
       public string Brand { get; set; }
       [Required]
       public string Model { get; set; }
       [Required]
       public string RegistrationNumber { get; set; }
       [Required]
       public int MeterStatus { get; set; }
       [Required]
       public string VAT { get; set; }
       public AppUser AppUser { get; set; }
       public CarInsurance CarInsurance { get; set; }
       [Required]
       public TechnicalExamination TechnicalExamination { get; set; }
       [Required]
       public Service Service { get; set; }
       [Required]
       public int Year { get; set; }
       public bool isArchival { get; set; }
      // public IReadOnlyList<Tires> Tires { get; set; }
    }
}