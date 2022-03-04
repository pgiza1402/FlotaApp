using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ServiceDto
    {
       public DateTime ServiceExpirationDate { get; set; }
       public int NextServiceMeterStatus { get; set; } 
    }
}