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

namespace EmpresasFuncionarios.Application.Command.CargoAlterado
{
    public class CargoAlteradoCommandHandler(IEmpresaFuncionarioEventService empresaFuncionarioEventService, IMediator mediator) : ICommandHandler<CargoAlteradoCommand, Guid>
    {
        private readonly IEmpresaFuncionarioEventService _empresaFuncionarioEventService = empresaFuncionarioEventService;
        private readonly IMediator _mediator = mediator;
        public async Task<Result<Guid>> Handle(CargoAlteradoCommand request, CancellationToken cancellationToken)
        {
            var @event = new CargoAlteradoEvent(
                request.StreamId,
                request.Cargo);

            var id = await _empresaFuncionarioEventService.AlterarCargo(@event, cancellationToken);
            await _mediator.Publish(@event, cancellationToken);
            return Result.Success(id);
        }
    }
}
