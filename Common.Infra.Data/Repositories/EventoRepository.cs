using Common.Infra.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Common.Infra.Data.Repositories
{
    public class EventoRepository<TDbContext> : IEventStore<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        public EventoRepository(TDbContext context)
        {
            _context = context;
        }

        // Salvar evento no banco
        public async Task<Guid> SalvarEventoAsync<T>(T evento) where T : Event
        {
            _context.Set<T>().Add(evento);
            await _context.SaveChangesAsync();
            //retornar o StreamId do evento adicionado
            return evento.StreamId; // Supondo que o StreamId é uma propriedade do evento

        }

        // Obter eventos por StreamId
        public async Task<List<T>> ObterEventosPorStreamIdAsync<T>(Guid streamId) where T : Event
        {
            return await _context.Set<T>()
                .Where(e => e.StreamId == streamId) // Supondo que o streamId está na entidade
                .OrderBy(e => e.Timestamp) // Ordenar pelo timestamp
                .ToListAsync();
        }
    }
}
