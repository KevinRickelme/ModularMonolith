using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcionarios.Application.Query.GetByNome
{
    public sealed record GetFuncionarioByNomeQuery(string Nome) : IQuery<IEnumerable<FuncionarioDTO>>;
}
