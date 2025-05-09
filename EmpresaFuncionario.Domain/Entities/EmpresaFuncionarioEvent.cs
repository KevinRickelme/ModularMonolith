using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Domain.Entities
{
    public record EmpresaFuncionarioEvent(Guid Id, Guid StreamId, TipoEvento TipoEvento, string Dados) : Event(StreamId);

    public enum TipoEvento
    {
        FuncionarioAdmitidoEvent = 1,
        CargoAlteradoEvent = 2,
        DepartamentoAlteradoEvent = 3,
        FuncionarioDemitidoEvent = 4
    }
}
