using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface ITiresRepository
    {
        Task <IReadOnlyList<Tires>> GetTiresAsync();

        Task<Tires> GetTiresByIdAsync(int id);

        void Update(Tires tires);

        Task<Tires> AddTiresAsync(Tires tires);

    }
}