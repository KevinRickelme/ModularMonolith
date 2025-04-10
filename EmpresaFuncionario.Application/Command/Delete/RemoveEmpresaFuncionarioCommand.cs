using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Command.Delete
{
    public sealed record RemoveEmpresaFuncionarioCommand(Guid Id) : ICommand<bool> 
    {
    }
}
