using AutoMapper;
using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using EmpresasFuncionarios.Domain.Abstractions;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Query.GetByFuncionarioAndEmpresaId
{
    public class GetByFuncionarioIdAndEmpresaIdQueryHandler(IEmpresaFuncionarioRepository empresaFuncionarioRepository, IMapper mapper) : IQueryHandler<GetEmpresaFuncionarioByFuncionarioIdAndEmpresaIdQuery, EmpresaFuncionarioDTO>
    {
        private readonly IEmpresaFuncionarioRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<EmpresaFuncionarioDTO>> Handle(GetEmpresaFuncionarioByFuncionarioIdAndEmpresaIdQuery request, CancellationToken cancellationToken)
        {
            var empresaFuncionario = await _empresaFuncionarioRepository.GetByFuncionarioIdAndEmpresaIdAsync(request.FuncionarioId, request.EmpresaId, cancellationToken);
            if (empresaFuncionario is null)
                return Result.Failure<EmpresaFuncionarioDTO>(EmpresaFuncionarioErrors.NotFound(request.EmpresaId, request.FuncionarioId));
            var empresaFuncionarioDTO = _mapper.Map<EmpresaFuncionarioDTO>(empresaFuncionario);
            return Result.Success(empresaFuncionarioDTO);
        }
    }
}
