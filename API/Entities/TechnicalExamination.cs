using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class TechnicalExamination
    {
        public int Id { get; set; }
        [Required]
        public DateTime TechnicalExaminationExpirationDate { get; set; }
        [Required]

        public int CarId { get; set; }
        [Required]
        public Car Car { get; set; }
    }
}