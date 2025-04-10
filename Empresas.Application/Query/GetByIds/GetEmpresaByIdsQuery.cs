using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Query.GetByIds
{
    public sealed record GetEmpresaByIdsQuery(List<Guid> Ids) : IQuery<IEnumerable<EmpresaDTO>>;
}
