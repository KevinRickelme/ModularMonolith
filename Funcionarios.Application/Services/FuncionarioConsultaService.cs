using Common.Application.DTOs;
using Funcionarios.Application.Contracts;
using Funcionarios.Application.Query.GetById;
using Funcionarios.Application.Query.GetByIds;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcionarios.Application.Services
{
    public class FuncionarioConsultaService(IMediator mediator) : IFuncionarioConsultaService
    {
        private readonly IMediator _mediator = mediator;
        public async Task<Result<FuncionarioDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetFuncionarioByIdQuery(id), cancellationToken);
        }

        public async Task<Result<IEnumerable<FuncionarioDTO>>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetFuncionariosByIdsQuery(ids), cancellationToken);
        }
    }
}
