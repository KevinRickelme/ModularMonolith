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

namespace Empresas.Application.Query.GetAll
{
    public class GetAllEmpresasQueryHandler(IEmpresaService empresaService, IMapper mapper) : IQueryHandler<GetAllEmpresasQuery, IEnumerable<EmpresaDTO>>
    {
        private readonly IEmpresaService _empresaService = empresaService;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<IEnumerable<EmpresaDTO>>> Handle(GetAllEmpresasQuery request, CancellationToken cancellationToken)
        {
            var empresas = await _empresaService.GetAllAsync(cancellationToken);
            if(empresas.Count() == 0)
            {
                return Result.Failure<IEnumerable<EmpresaDTO>>(EmpresaErrors.SemEmpresas);
            }   

            var empresasDTO = _mapper.Map<IEnumerable<EmpresaDTO>>(empresas);
            return Result.Success(empresasDTO);
        }
    }
}
