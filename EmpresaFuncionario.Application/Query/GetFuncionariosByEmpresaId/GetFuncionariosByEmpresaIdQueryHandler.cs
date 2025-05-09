using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using EmpresasFuncionarios.Domain.Abstractions;
using Funcionarios.Application.Contracts;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Query.GetFuncionariosByEmpresaId
{
    public class GetFuncionariosByEmpresaIdQueryHandler(IFuncionarioConsultaService funcionarioConsultaService, IEmpresaFuncionarioReadRepository empresaFuncionarioRepository) : IQueryHandler<GetFuncionariosByEmpresaIdQuery, IEnumerable<FuncionarioDTO>>
    {
        private readonly IFuncionarioConsultaService _funcionarioConsultaService = funcionarioConsultaService;
        private readonly IEmpresaFuncionarioReadRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;

        public async Task<Result<IEnumerable<FuncionarioDTO>>> Handle(GetFuncionariosByEmpresaIdQuery request, CancellationToken cancellationToken)
        {
            var vinculos = await _empresaFuncionarioRepository.GetByEmpresaIdAsync(request.EmpresaId, cancellationToken);
            
            if (vinculos == null)
                return Result.Failure<IEnumerable<FuncionarioDTO>>(EmpresaFuncionarioErrors.NotFound(request.EmpresaId, TipoIdEmpresaFuncionario.Empresa));

            var funcionarioIds = vinculos.Select(v => v.FuncionarioId).ToList();
            return await _funcionarioConsultaService.GetByIdsAsync(funcionarioIds, cancellationToken);
        }
    }
}
