using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CarRepository : ICarRepository
    {

        private readonly DataContext _context;
        public CarRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Car> AddCarAsync(Car car)
        {
            await _context.Cars.AddAsync(car);

            await _context.SaveChangesAsync();

            return car;
        }

        public void Delete(Car car)
        {
            _context.Cars.Remove(car);

        }

        public async Task<Car> GetCarById(int id)
        {
            var car = await _context.Cars
            .Include(a => a.AppUser)
            .Include(c => c.CarInsurance)
            .Include(s => s.Service)
            .Include(t => t.TechnicalExamination)
            .FirstOrDefaultAsync(x => x.Id == id);
            

            return car;
        }

        public async Task<IReadOnlyList<Car>> GetCarsAsync()
        {
            return await _context.Cars
            .Include(a => a.AppUser)
            .Include(c => c.CarInsurance)
            .Include(s => s.Service)
            .Include(t => t.TechnicalExamination)
            .ToListAsync();
        }


        public void Update(Car car)
        {
            _context.Update(car);
        }
    }
}