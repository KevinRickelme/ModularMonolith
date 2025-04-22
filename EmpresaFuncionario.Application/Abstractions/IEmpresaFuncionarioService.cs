using Common.Application.DTOs;

namespace EmpresasFuncionarios.Application.Abstractions
{
    public interface IEmpresaFuncionarioService
    {
        Task<bool> RemoveAsync(Guid Id, CancellationToken cancellationToken);
        Task<EmpresaFuncionarioDTO?> UpdateAsync(EmpresaFuncionarioDTO empresaFuncionario, CancellationToken cancellationToken);
        Task<Guid> AddAsync(EmpresaFuncionarioDTO empresaFuncionario, CancellationToken cancellationToken);
        Task<EmpresaFuncionarioDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<EmpresaFuncionarioDTO?> GetByFuncionarioIdAndEmpresaIdAsync(Guid funcionarioId, Guid empresaId, CancellationToken cancellationToken);
        Task<IEnumerable<EmpresaFuncionarioDTO>> GetByFuncionarioIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<IEnumerable<EmpresaFuncionarioDTO>> GetByEmpresaIdAsync(Guid Id, CancellationToken cancellationToken);

    }
}
