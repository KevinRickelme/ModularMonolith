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

namespace EmpresasFuncionarios.Application.Query.GetAll
{
    public class GetAllEmpresasFuncionariosQueryHandle(IEmpresaFuncionarioReadRepository empresaFuncionarioRepository, IMapper mapper) : IQueryHandler<GetAllEmpresasFuncionariosQuery, List<EmpresaFuncionarioDTO>>
    {
        private readonly IEmpresaFuncionarioReadRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<List<EmpresaFuncionarioDTO>>> Handle(GetAllEmpresasFuncionariosQuery request, CancellationToken cancellationToken)
        {
            var empresasFuncionarios = await _empresaFuncionarioRepository.GetAllAsync(cancellationToken);

            return _mapper.Map<List<EmpresaFuncionarioDTO>>(empresasFuncionarios) is List<EmpresaFuncionarioDTO> empresasFuncionariosDTO
                ? Result.Success(empresasFuncionariosDTO)
                : Result.Failure<List<EmpresaFuncionarioDTO>>(EmpresaFuncionarioErrors.NotFound(Guid.Empty, Guid.Empty));
        }
    }
}
