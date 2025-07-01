using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infra.Data.Abstractions
{
    public interface IEventStore<TDbContext> where TDbContext : DbContext 
    {
        Task<Guid> SalvarEventoAsync<T>(T evento) where T : Event;
        Task<List<T>> ObterEventosPorStreamIdAsync<T>(Guid streamId) where T : Event;
    }
}
