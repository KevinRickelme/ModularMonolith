using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using Empresas.Application.Contracts;
using EmpresasFuncionarios.Domain.Abstractions;
using SharedKernel;
using SharedKernel.Errors;

namespace EmpresasFuncionarios.Application.Query.GetEmpresasByFuncionarioId
{
    public class GetEmpresasByFuncionarioIdQueryHandler(IEmpresaConsultaService empresaConsultaService, IEmpresaFuncionarioReadRepository empresaFuncionarioRepository) : IQueryHandler<GetEmpresasByFuncionarioIdQuery, IEnumerable<EmpresaDTO>>
    {
        private readonly IEmpresaConsultaService _empresaConsultaService = empresaConsultaService;
        private readonly IEmpresaFuncionarioReadRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;
        public async Task<Result<IEnumerable<EmpresaDTO>>> Handle(GetEmpresasByFuncionarioIdQuery request, CancellationToken cancellationToken)
        {
            var vinculos = await _empresaFuncionarioRepository.GetByFuncionarioIdAsync(request.FuncionarioId, cancellationToken);
            if(vinculos == null)
                return Result.Failure<IEnumerable<EmpresaDTO>>(EmpresaFuncionarioErrors.NotFound(request.FuncionarioId, TipoIdEmpresaFuncionario.Funcionario));

            var empresaIds = vinculos.Select(v => v.EmpresaId).ToList();
            return await _empresaConsultaService.GetByIdsAsync(empresaIds, cancellationToken);
        }
    }
}
