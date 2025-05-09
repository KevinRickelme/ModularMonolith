using EmpresasFuncionarios.Domain.Entities;

namespace EmpresasFuncionarios.Infra.Data.Repositories
{
    public interface IEventEmpresaFuncionarioStore
    {
        Task<EmpresaFuncionario?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}