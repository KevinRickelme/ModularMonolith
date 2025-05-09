using EmpresasFuncionarios.Domain.Entities;
using SharedKernel;

namespace EmpresasFuncionarios.Domain.Events
{
    public record CargoAlteradoEvent(Guid StreamId, string NovoCargo) : Event(StreamId), IAppliesOn<EmpresaFuncionario>
    {
        public void Apply(EmpresaFuncionario aggregate)
        {
            aggregate.AlterarCargo(NovoCargo);
        }
    }
}
