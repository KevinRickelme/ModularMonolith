using Common.Application.Abstractions.Messaging;
using Empresas.Application.Contracts;
using EmpresasFuncionarios.Application.Abstractions;
using EmpresasFuncionarios.Domain.Abstractions;
using EmpresasFuncionarios.Domain.Entities;
using EmpresasFuncionarios.Domain.Events;
using Funcionarios.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Command.FuncionarioAdmitido
{
    public class FuncionarioAdmitidoCommandHandler(IEmpresaFuncionarioEventService empresaFuncionarioEventService, IMediator mediator) : ICommandHandler<FuncionarioAdmitidoCommand, Guid>
    {
        private readonly IEmpresaFuncionarioEventService _empresaFuncionarioEventService = empresaFuncionarioEventService;
        private readonly IMediator _mediator = mediator;
        public async Task<Result<Guid>> Handle(FuncionarioAdmitidoCommand request, CancellationToken cancellationToken)
        {
            List<string> values = [request.EmpresaId.ToString(), request.FuncionarioId.ToString()];
            var @event = new FuncionarioAdmitidoEvent(
                StreamIdGenerator.GenerateStreamId(values),
                request.FuncionarioId,
                request.EmpresaId,
                request.DataAdmissao,
                request.Cargo,
                request.Departamento);

            //verificar se o streamId ja existe 
            var streamId = await _empresaFuncionarioEventService.VerificarStreamIdAsync(@event.EmpresaFuncionarioId, cancellationToken);
            if (streamId)
                return Result.Failure<Guid>(EmpresaFuncionarioErrors.VinculoNotUnique);


            var id = await _empresaFuncionarioEventService.AdmitirFuncionarioAsync(@event, cancellationToken);
            await _mediator.Publish(@event, cancellationToken);
            return Result.Success(id);
        }
    }
}
