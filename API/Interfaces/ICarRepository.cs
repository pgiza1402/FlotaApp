using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface ICarRepository
    {
        Task<IReadOnlyList<Car>> GetCarsAsync();
        void Update (Car car);
        Task<Car> GetCarById(int id);
        void Delete(Car car);
        Task<Car> AddCarAsync(Car car);
    }
}