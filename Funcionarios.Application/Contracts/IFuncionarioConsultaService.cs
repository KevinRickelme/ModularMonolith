using Common.Application.DTOs;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcionarios.Application.Contracts
{
    public interface IFuncionarioConsultaService
    {
        Task<Result<FuncionarioDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Result<IEnumerable<FuncionarioDTO>>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    }
}
