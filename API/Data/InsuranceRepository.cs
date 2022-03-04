using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly DataContext _context;
        public InsuranceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<CarInsurance> AddInsuranceAsync(CarInsurance insurance)
        {
             await _context.CarInsurances.AddAsync(insurance);

             await _context.SaveChangesAsync();

             return insurance;
        }

        public void Delete(CarInsurance insurance)
        {
            _context.CarInsurances.Remove(insurance);
        }

        public async Task<CarInsurance> GetInsuranceById(int id)
        {
            return await _context.CarInsurances
            .Include(c => c.Car)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<CarInsurance>> GetInsurancesAsync()
        {
            return await _context.CarInsurances
            .Include(c => c.Car)
            .ToListAsync();
        }

        public void Update(CarInsurance insurance)
        {
            _context.CarInsurances.Update(insurance);
        }
    }
}