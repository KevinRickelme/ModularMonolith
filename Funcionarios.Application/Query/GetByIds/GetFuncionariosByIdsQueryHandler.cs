using AutoMapper;
using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using Funcionarios.Domain.Abstractions;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcionarios.Application.Query.GetByIds
{
    public class GetFuncionariosByIdsQueryHandler(IFuncionarioRepository funcionarioRepository, IMapper mapper) : IQueryHandler<GetFuncionariosByIdsQuery, IEnumerable<FuncionarioDTO>>
    {
        private readonly IFuncionarioRepository _funcionarioRepository = funcionarioRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<IEnumerable<FuncionarioDTO>>> Handle(GetFuncionariosByIdsQuery request, CancellationToken cancellationToken)
        {
            var result = await _funcionarioRepository.GetByIdsAsync(request.Ids, cancellationToken);
            if (!result.Any())
            {
                return Result.Failure<IEnumerable<FuncionarioDTO>>(FuncionarioErrors.NotFound(request.Ids));
            }

            var funcionariosDTO = _mapper.Map<IEnumerable<FuncionarioDTO>>(result);
            return Result.Success(funcionariosDTO);
        }
    }
}
