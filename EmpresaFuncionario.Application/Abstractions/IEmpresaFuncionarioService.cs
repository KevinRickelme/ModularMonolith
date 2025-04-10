using Common.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Abstractions
{
    public interface IEmpresaFuncionarioService
    {
        Task<bool> RemoveAsync(Guid Id, CancellationToken cancellationToken);
        Task<EmpresaFuncionarioDTO?> UpdateAsync(EmpresaFuncionarioDTO empresaFuncionario, CancellationToken cancellationToken);
    }
}
