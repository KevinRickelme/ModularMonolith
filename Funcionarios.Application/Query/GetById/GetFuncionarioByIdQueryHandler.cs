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

namespace Funcionarios.Application.Query.GetById
{
    public class GetFuncionarioByIdQueryHandler(IFuncionarioRepository funcionarioRepository, IMapper mapper) : IQueryHandler<GetFuncionarioByIdQuery, FuncionarioDTO>
    {
        private readonly IFuncionarioRepository _funcionarioRepository = funcionarioRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<FuncionarioDTO>> Handle(GetFuncionarioByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _funcionarioRepository.GetByIdAsync(request.Id, cancellationToken);
            if(result == null)
            {
                return Result.Failure<FuncionarioDTO>(FuncionarioErrors.NotFound(request.Id));
            }
            var funcionarioDTO = _mapper.Map<FuncionarioDTO>(result);
            return Result.Success(funcionarioDTO);
        }
    }
}
