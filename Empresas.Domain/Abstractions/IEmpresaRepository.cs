using Empresas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Domain.Abstractions
{
    public interface IEmpresaRepository
    {
        Task<Guid> AddAsync(Empresa empresa, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Empresa empresa, CancellationToken cancellationToken);
        Task<bool> RemoveAsync(Empresa empresa, CancellationToken cancellationToken);

        Task<Empresa?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Empresa?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken);
        Task<Empresa?> GetByNomeAsync(string nome, CancellationToken cancellationToken);

        Task<IEnumerable<Empresa>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Empresa>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    }
}
