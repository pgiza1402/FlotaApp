using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TiresRepository : ITiresRepository
    {
        private readonly DataContext _context;
        public TiresRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Tires> AddTiresAsync(Tires tires)
        {
            await _context.Tires.AddAsync(tires);

            await _context.SaveChangesAsync();

            return tires;
        }

        public async Task<IReadOnlyList<Tires>> GetTiresAsync()
        {
            return await _context.Tires
            .Include(c => c.Car)
            .ToListAsync();
        }

        public async Task<Tires> GetTiresByIdAsync(int id)
        {
            var car = await _context.Tires.FirstOrDefaultAsync(x => x.Id == id);

            return car;
        }

        public void Update(Tires tires)
        {
            _context.Update(tires);
        }
    }
}