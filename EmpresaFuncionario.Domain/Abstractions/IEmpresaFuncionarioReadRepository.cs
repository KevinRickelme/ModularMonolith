using EmpresasFuncionarios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Domain.Abstractions
{
    public interface IEmpresaFuncionarioReadRepository
    {
        Task<Guid> AddAsync(EmpresaFuncionario empresaFuncionario, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(EmpresaFuncionario empresaFuncionario, CancellationToken cancellationToken);
        Task<bool> RemoveAsync(EmpresaFuncionario empresaFuncionario, CancellationToken cancellationToken);
        Task<EmpresaFuncionario?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<EmpresaFuncionario?> GetByFuncionarioIdAndEmpresaIdAsync(Guid funcionarioId, Guid empresaId, CancellationToken cancellationToken);
        Task<IEnumerable<EmpresaFuncionario>> GetByFuncionarioIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<IEnumerable<EmpresaFuncionario>> GetByEmpresaIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<EmpresaFuncionario> GetByStreamIdAsync(Guid streamId, CancellationToken cancellationToken);
        Task<List<EmpresaFuncionario>> GetAllAsync(CancellationToken cancellationToken);
    }
}
