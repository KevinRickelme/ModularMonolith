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

namespace EmpresasFuncionarios.Application.Query.GetByFuncionarioId
{
    public class GetEmpresaFuncionarioByFuncionarioIdQueryHandler(IEmpresaFuncionarioRepository empresaFuncionarioRepository, IMapper mapper) : IQueryHandler<GetEmpresaFuncionarioByFuncionarioIdQuery, IEnumerable<EmpresaFuncionarioDTO>>
    {
        private readonly IEmpresaFuncionarioRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<IEnumerable<EmpresaFuncionarioDTO>>> Handle(GetEmpresaFuncionarioByFuncionarioIdQuery request, CancellationToken cancellationToken)
        {
            var empresaFuncionarios = await _empresaFuncionarioRepository.GetByFuncionarioIdAsync(request.Id, cancellationToken);
            if (empresaFuncionarios is null || !empresaFuncionarios.Any())
                return Result.Failure<IEnumerable<EmpresaFuncionarioDTO>>(EmpresaFuncionarioErrors.NotFound(request.Id, TipoIdEmpresaFuncionario.Funcionario));
            var empresaFuncionariosDTO = _mapper.Map<IEnumerable<EmpresaFuncionarioDTO>>(empresaFuncionarios);
            return Result.Success(empresaFuncionariosDTO);
        }
    }
}
