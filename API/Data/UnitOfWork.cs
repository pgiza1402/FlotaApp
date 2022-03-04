using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ICarRepository CarRepository => new CarRepository(_context);

        public ILogRepository LogRepository => new LogRepository(_context);

        public ITiresRepository TiresRepository => new TiresRepository(_context);

        public IInsuranceRepository InsuranceRepository => new InsuranceRepository(_context);

        public IFieldRepository FieldRepository => new FieldRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}