using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class FieldRepository : IFieldRepository
    {
        private readonly DataContext _context;
        public FieldRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Field> AddFieldAsync(Field field)
        {
            await _context.Fields.AddAsync(field);

            await _context.SaveChangesAsync();

            return field;
        }

        public void Delete(Field field)
        {
            _context.Fields.Remove(field);
        }

        public async Task<Field> GetFieldByIdAsync(int id)
        {
            var field = await _context.Fields.FirstOrDefaultAsync(x => x.Id == id );

            return field;
        }

        public async Task<IReadOnlyList<Field>> GetFieldsAsync()
        {
            return await _context.Fields.OrderBy(x => x.Category).ToListAsync();
        }

        public void Update(Field field)
        {
            _context.Fields.Update(field);
        }
    }
}