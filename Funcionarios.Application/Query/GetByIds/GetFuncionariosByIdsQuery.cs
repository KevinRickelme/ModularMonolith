using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcionarios.Application.Query.GetByIds
{
    public sealed record GetFuncionariosByIdsQuery(List<Guid> Ids) : IQuery<IEnumerable<FuncionarioDTO>>;
}
