using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class InsuranceForListDto
    {
        public int Id { get; set; }
        public string Insurer { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Package { get; set; }
        public string Car { get; set; }
        public string CarRegistrationNumber { get; set; }
    }
}
