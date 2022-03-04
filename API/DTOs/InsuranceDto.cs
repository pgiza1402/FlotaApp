using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class InsuranceDto
    {
        public string Insurer { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Package { get; set; }
    
    }
}