using Common.Application.DTOs;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Contracts
{
    public interface IEmpresaConsultaService 
    {
        Task<Result<EmpresaDTO>> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<Result<IEnumerable<EmpresaDTO>>> GetByIdsAsync(List<Guid> empresaIds, CancellationToken cancellationToken);
    }
}
