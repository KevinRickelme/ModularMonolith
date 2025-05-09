using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmpresasFuncionarios.Application.Command.DepartamentoAlterado
{
    public sealed record DepartamentoAlteradoCommand(Guid StreamId, string Departamento) : ICommand<Guid>;
}
