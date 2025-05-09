using Common.Infra.Data.Abstractions;
using EmpresasFuncionarios.Domain.Entities;
using EmpresasFuncionarios.Domain.Events;
using EmpresasFuncionarios.Infra.Data.Context;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Infra.Data.Repositories
{
    public class EventEmpresaFuncionarioStore(IEventStore<EmpresaFuncionarioDbContext> eventStore) : IEventEmpresaFuncionarioStore
    {
        private readonly IEventStore<EmpresaFuncionarioDbContext> _eventStore = eventStore;
        public async Task<List<EmpresaFuncionarioEvent>> GetByIdAsync(Guid id)
        {
            var eventos = await _eventStore.ObterEventosPorStreamIdAsync<EmpresaFuncionarioEvent>(id);
            return eventos;
        }
    
    
        public async Task<EmpresaFuncionario?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var registros = await _eventStore.ObterEventosPorStreamIdAsync<EmpresaFuncionarioEvent>(id);
            var eventos = new List<Event>();

            foreach (var registro in registros)
            {
                var evento = DesserializarEvento(registro.TipoEvento, registro.Dados);

                if (evento is not null)
                    eventos.Add(evento);
            }

            return EmpresaFuncionario.ReplayEvents(eventos);
        }

        private Event? DesserializarEvento(TipoEvento tipo, string dados) => tipo switch
        {
            TipoEvento.FuncionarioAdmitidoEvent => JsonSerializer.Deserialize<FuncionarioAdmitidoEvent>(dados),
            TipoEvento.CargoAlteradoEvent => JsonSerializer.Deserialize<CargoAlteradoEvent>(dados),
            TipoEvento.DepartamentoAlteradoEvent => JsonSerializer.Deserialize<DepartamentoAlteradoEvent>(dados),
            TipoEvento.FuncionarioDemitidoEvent => throw new NotImplementedException(),
            _ => null
        };
    }
}
