using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IInsuranceRepository
    {
         Task<IReadOnlyList<CarInsurance>> GetInsurancesAsync();
        void Update (CarInsurance insurance);
        Task<CarInsurance> GetInsuranceById(int id);
        void Delete(CarInsurance insurance);
        Task<CarInsurance> AddInsuranceAsync(CarInsurance insurance); 
    }
}