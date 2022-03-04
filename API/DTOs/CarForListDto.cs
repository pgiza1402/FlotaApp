using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CarForListDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string UserName { get; set; }
        public string RegistrationNumber { get; set; }
        public string MeterStatus { get; set; }
        public string VAT { get; set; }
        public DateTime carInsuranceExpirationDate { get; set; }
        public DateTime TechnicalExaminationExpirationDate { get; set; }
        public DateTime ServiceExpirationDate { get; set; }
        public int NextServiceMeterStatus { get; set; }
        public int Year { get; set; }
        public bool isArchival { get; set; }
    }
}