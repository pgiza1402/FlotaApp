using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        ICarRepository CarRepository {get;}
        ILogRepository LogRepository {get;}

        ITiresRepository TiresRepository {get;}

        IInsuranceRepository InsuranceRepository {get;}

        IFieldRepository FieldRepository {get;}
        Task<bool> Complete();
        
    }
}