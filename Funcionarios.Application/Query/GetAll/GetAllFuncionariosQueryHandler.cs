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

namespace Funcionarios.Application.Query.GetAll
{
    public class GetAllFuncionariosQueryHandler(IFuncionarioRepository funcionarioRepository, IMapper mapper) : IQueryHandler<GetAllFuncionariosQuery, IEnumerable<FuncionarioDTO>>
    {
        private readonly IFuncionarioRepository _funcionarioRepository = funcionarioRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<IEnumerable<FuncionarioDTO>>> Handle(GetAllFuncionariosQuery request, CancellationToken cancellationToken)
        {
            var funcionarios = await _funcionarioRepository.GetAllAsync(cancellationToken);
            if (funcionarios is null || funcionarios.Count == 0)
                return Result.Failure<IEnumerable<FuncionarioDTO>>(FuncionarioErrors.NotFound());

            var funcionariosDTO = _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);
            return Result.Success(funcionariosDTO);
        }
    }
}
