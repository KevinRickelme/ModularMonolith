using EmpresasFuncionarios.Domain.Entities;
using SharedKernel;

namespace EmpresasFuncionarios.Domain.Events
{
    public record DepartamentoAlteradoEvent(Guid StreamId, string NovoDepartamento) : Event(StreamId), IAppliesOn<EmpresaFuncionario>
    {
        public void Apply(EmpresaFuncionario aggregate)
        {
            aggregate.AlterarDepartamento(NovoDepartamento);
        }
    }
}
