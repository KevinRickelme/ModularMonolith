using Common.Application.DTOs;

namespace Empresas.Application.Abstractions
{
    public interface IEmpresaService
    {
        Task<Guid> AddAsync(EmpresaDTO empresa, CancellationToken cancellationToken);
        Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken);
        Task<EmpresaDTO?> UpdateAsync(EmpresaDTO empresa, CancellationToken cancellationToken);

        Task<EmpresaDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<EmpresaDTO?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken);
        Task<EmpresaDTO?> GetByNomeAsync(string nome, CancellationToken cancellationToken);

        Task<IEnumerable<EmpresaDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<EmpresaDTO>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    }
}
