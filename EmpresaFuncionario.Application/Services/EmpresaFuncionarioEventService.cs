using AutoMapper;
using Azure.Core;
using Common.Application.DTOs;
using Common.Infra.Data.Abstractions;
using EmpresasFuncionarios.Application.Abstractions;
using EmpresasFuncionarios.Domain.Abstractions;
using EmpresasFuncionarios.Domain.Entities;
using EmpresasFuncionarios.Domain.Events;
using EmpresasFuncionarios.Infra.Data.Context;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Services
{
    public class EmpresaFuncionarioEventService(IEventStore<EmpresaFuncionarioDbContext> eventRepository, IEmpresaFuncionarioReadRepository empresaFuncionarioRepository, IMapper mapper) : IEmpresaFuncionarioEventService
    {
        private readonly IEventStore<EmpresaFuncionarioDbContext> _eventRepository = eventRepository;
        private readonly IEmpresaFuncionarioReadRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Guid> AdmitirFuncionarioAsync(FuncionarioAdmitidoEvent @event, CancellationToken cancellationToken)
        {
            return await SalvarEvento(@event, TipoEvento.FuncionarioAdmitidoEvent);
        }


        public async Task<Guid> AlterarCargo(CargoAlteradoEvent @event, CancellationToken cancellationToken)
        {
            return await SalvarEvento(@event, TipoEvento.CargoAlteradoEvent);
        }

        public async Task<Guid> AlterarDepartamento(DepartamentoAlteradoEvent @event, CancellationToken cancellationToken)
        {
            return await SalvarEvento(@event, TipoEvento.DepartamentoAlteradoEvent);
        }

        public async Task<List<EmpresaFuncionarioEvent>> GetByIdAsync(Guid id)
        {
            var eventos = await _eventRepository.ObterEventosPorStreamIdAsync<EmpresaFuncionarioEvent>(id);
            return eventos;
        }

        public async Task<bool> VerificarStreamIdAsync(Guid streamId, CancellationToken cancellationToken)
        {
            var eventos = await GetByIdAsync(streamId);
            if (eventos.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private async Task<Guid> SalvarEvento<T>(T @event, TipoEvento tipo) where T : Event
        {
            var dadosJson = System.Text.Json.JsonSerializer.Serialize(@event);
            var empresaFuncionarioEvent = new EmpresaFuncionarioEvent(Guid.NewGuid(), @event.StreamId, tipo, dadosJson);
            await _eventRepository.SalvarEventoAsync(empresaFuncionarioEvent);


            return @event.StreamId;
        }
    }
}
