using AutoMapper;
using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using Funcionarios.Domain.Abstractions;
using MediatR;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcionarios.Application.Query.GetByNome
{
    public class GetFuncionarioByNomeQueryHandler(IFuncionarioRepository funcionarioRepository, IMapper mapper) 
        : IQueryHandler<GetFuncionarioByNomeQuery, IEnumerable<FuncionarioDTO>>
    {
        private readonly IFuncionarioRepository _funcionarioRepository = funcionarioRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<FuncionarioDTO>>> Handle(GetFuncionarioByNomeQuery query, CancellationToken cancellationToken)
        {
            var result = await _funcionarioRepository.GetByNomeAsync(query.Nome, cancellationToken);
            if (result == null || result.Count == 0)
            {
                return Result.Failure<IEnumerable<FuncionarioDTO>>(FuncionarioErrors.NotFound(query.Nome));
            }
            var funcionarioDTOs = _mapper.Map<IEnumerable<FuncionarioDTO>>(result);
            return Result.Success(funcionarioDTOs);
        }
    }
}
