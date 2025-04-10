using Common.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Abstractions
{
    public interface IEmpresaService
    {
        Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken);
        Task<EmpresaDTO?> UpdateAsync(EmpresaDTO empresa, CancellationToken cancellationToken);
    }
}
