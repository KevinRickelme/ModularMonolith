using AutoMapper;
using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using Empresas.Domain.Abstractions;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Query.GetByIds
{
    public class GetEmpresaByIdsQueryHandler(IEmpresaRepository empresaRepository, IMapper mapper) : IQueryHandler<GetEmpresaByIdsQuery, IEnumerable<EmpresaDTO>>
    {
        private readonly IEmpresaRepository _empresaRepository = empresaRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<IEnumerable<EmpresaDTO>>> Handle(GetEmpresaByIdsQuery request, CancellationToken cancellationToken)
        {
            var result = await _empresaRepository.GetByIdsAsync(request.Ids, cancellationToken);
            if (result == null)
                return Result.Failure<IEnumerable<EmpresaDTO>>(EmpresaErrors.NotFound(request.Ids));
            
            var empresasDTO = _mapper.Map<IEnumerable<EmpresaDTO>>(result);

            return Result.Success(empresasDTO);
        }
    }
}
