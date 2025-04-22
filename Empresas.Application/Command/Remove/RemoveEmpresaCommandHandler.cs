using Common.Application.Abstractions.Messaging;
using Empresas.Application.Abstractions;
using Empresas.Domain.Abstractions;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Command.Remove
{
    public class RemoveEmpresaCommandHandler(IEmpresaService empresaService) : ICommandHandler<RemoveEmpresaCommand, bool>
    {
        private readonly IEmpresaService _empresaService = empresaService;
        public async Task<Result<bool>> Handle(RemoveEmpresaCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return Result.Failure<bool>(EmpresaErrors.NotFound(request.Id));
            }

            var empresa = await _empresaService.GetByIdAsync(request.Id, cancellationToken);
            
            if (empresa == null) {
                return Result.Failure<bool>(EmpresaErrors.NotFound(request.Id));
            }

            return await _empresaService.RemoveAsync(request.Id, cancellationToken);
        }
    }
}
