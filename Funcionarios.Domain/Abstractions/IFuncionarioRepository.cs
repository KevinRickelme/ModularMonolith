using Funcionarios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcionarios.Domain.Abstractions
{
    public interface IFuncionarioRepository
    {
        Task AddAsync(Funcionario funcionario, CancellationToken cancellationToken);
        Task<Funcionario?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Funcionario?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<int> GetTotalCount(CancellationToken cancellationToken);
        Task<List<Funcionario>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(Funcionario funcionario, CancellationToken cancellationToken);
        Task<List<Funcionario>?> GetByNomeAsync(string nome, CancellationToken cancellationToken);
        Task<List<Funcionario>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    }
}
