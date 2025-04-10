using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Command.Remove
{
    public sealed record RemoveEmpresaCommand(Guid Id) : ICommand<bool>;
}
