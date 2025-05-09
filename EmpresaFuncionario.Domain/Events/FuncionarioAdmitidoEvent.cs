using EmpresasFuncionarios.Domain.Entities;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Domain.Events
{
    public record FuncionarioAdmitidoEvent(
        Guid EmpresaFuncionarioId,
        Guid FuncionarioId,
        Guid EmpresaId,
        DateTime DataAdmissao,
        string Cargo,
        string Departamento
    ) : Event(EmpresaFuncionarioId), IAppliesOn<EmpresaFuncionario>
    {
        public void Apply(EmpresaFuncionario aggregate)
        {
            aggregate.Admitir(EmpresaFuncionarioId, FuncionarioId, EmpresaId, DataAdmissao, Cargo, Departamento);
        }
    }
}
