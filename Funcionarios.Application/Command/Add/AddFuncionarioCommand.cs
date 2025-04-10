using Common.Application.Abstractions.Messaging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Funcionarios.Application.Command.Add
{
    public sealed record AddFuncionarioCommand(string Nome, int Idade, string Email) : ICommand<Guid>;
}
