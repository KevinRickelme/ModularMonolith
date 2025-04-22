using AutoMapper;
using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using Empresas.Application.Abstractions;
using Empresas.Domain.Abstractions;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Query.GetByCNPJ
{
    public class GetEmpresaByCNPJQueryHandler(IEmpresaService empresaService, IMapper mapper) : IQueryHandler<GetEmpresaByCNPJQuery, EmpresaDTO>
    {
        private readonly IEmpresaService _empresaService = empresaService;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<EmpresaDTO>> Handle(GetEmpresaByCNPJQuery request, CancellationToken cancellationToken)
        {
            var empresa = await _empresaService.GetByCnpjAsync(request.CNPJ, cancellationToken);
            if (empresa == null)
            {
                return Result.Failure<EmpresaDTO>(EmpresaErrors.NotFound(request.CNPJ));
            }
            var empresaDTO = _mapper.Map<EmpresaDTO>(empresa);
            return Result.Success(empresaDTO);
        }
    }
}
