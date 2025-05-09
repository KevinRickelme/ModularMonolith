using Common.Application.DTOs;
using EmpresasFuncionarios.Domain.Entities;
using EmpresasFuncionarios.Domain.Events;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Abstractions
{
    public interface IEmpresaFuncionarioEventService
    {
        Task<Guid> AdmitirFuncionarioAsync(FuncionarioAdmitidoEvent empresaFuncionario, CancellationToken cancellationToken);
        Task<Guid> AlterarCargo(CargoAlteradoEvent @event, CancellationToken cancellationToken);
        Task<Guid> AlterarDepartamento(DepartamentoAlteradoEvent @event, CancellationToken cancellationToken);
        Task<List<EmpresaFuncionarioEvent>> GetByIdAsync(Guid id);
        Task<bool> VerificarStreamIdAsync(Guid streamId, CancellationToken cancellationToken);
    }
}
