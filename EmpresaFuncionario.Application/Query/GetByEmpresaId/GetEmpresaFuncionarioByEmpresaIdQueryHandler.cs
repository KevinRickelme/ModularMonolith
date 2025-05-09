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

namespace EmpresasFuncionarios.Application.Query.GetByEmpresaId
{
    public class GetEmpresaFuncionarioByEmpresaIdQueryHandler(IEmpresaFuncionarioReadRepository empresaFuncionarioRepository, IMapper mapper) : IQueryHandler<GetEmpresaFuncionarioByEmpresaIdQuery, IEnumerable<EmpresaFuncionarioDTO>>
    {
        private readonly IEmpresaFuncionarioReadRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<EmpresaFuncionarioDTO>>> Handle(GetEmpresaFuncionarioByEmpresaIdQuery request, CancellationToken cancellationToken)
        {
            var empresaFuncionarios = await _empresaFuncionarioRepository.GetByEmpresaIdAsync(request.Id, cancellationToken);
            if (empresaFuncionarios is null || !empresaFuncionarios.Any())
                return Result.Failure<IEnumerable<EmpresaFuncionarioDTO>>(EmpresaFuncionarioErrors.NotFound(request.Id, TipoIdEmpresaFuncionario.Empresa));
            var empresaFuncionariosDTO = _mapper.Map<IEnumerable<EmpresaFuncionarioDTO>>(empresaFuncionarios);
            return Result.Success(empresaFuncionariosDTO);
        }
    }
}
