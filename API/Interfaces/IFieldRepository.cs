using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IFieldRepository
    {
        Task<IReadOnlyList<Field>> GetFieldsAsync();
        Task<Field> GetFieldByIdAsync(int id);
        Task<Field> AddFieldAsync(Field field);
        void Update(Field field);
        void Delete(Field field);



    }
}