using Common.Application.DTOs;
using Empresas.Application.Contracts;
using Empresas.Application.Query.GetById;
using Empresas.Application.Query.GetByIds;
using Empresas.Domain.Abstractions;
using Empresas.Domain.Entities;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Services
{
    public class EmpresaConsultaService(IMediator mediator) : IEmpresaConsultaService
    {
        private readonly IMediator _mediator = mediator;
        public async Task<Result<EmpresaDTO>> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetEmpresaByIdQuery(Id), cancellationToken);
        }

        public async Task<Result<IEnumerable<EmpresaDTO>>> GetByIdsAsync(List<Guid> empresaIds, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetEmpresaByIdsQuery(empresaIds), cancellationToken);
        }
    }
}
