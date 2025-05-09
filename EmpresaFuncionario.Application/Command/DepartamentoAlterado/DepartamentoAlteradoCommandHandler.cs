using Common.Application.Abstractions.Messaging;
using EmpresasFuncionarios.Application.Abstractions;
using EmpresasFuncionarios.Domain.Events;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Command.DepartamentoAlterado
{
    public class DepartamentoAlteradoCommandHandler(IEmpresaFuncionarioEventService empresaFuncionarioEventService, IMediator mediator) : ICommandHandler<DepartamentoAlteradoCommand, Guid>
    {
        private readonly IEmpresaFuncionarioEventService _empresaFuncionarioEventService = empresaFuncionarioEventService;
        private readonly IMediator _mediator = mediator;
        public async Task<Result<Guid>> Handle(DepartamentoAlteradoCommand request, CancellationToken cancellationToken)
        {
            var @event = new DepartamentoAlteradoEvent(
                request.StreamId,
                request.Departamento);

            var id = await _empresaFuncionarioEventService.AlterarDepartamento(@event, cancellationToken);
            await _mediator.Publish(@event, cancellationToken);
            return Result.Success(id);
        }
    }
}
