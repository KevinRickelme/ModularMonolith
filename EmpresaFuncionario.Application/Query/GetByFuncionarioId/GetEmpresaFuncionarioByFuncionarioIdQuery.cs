using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Query.GetByFuncionarioId
{
    public sealed record GetEmpresaFuncionarioByFuncionarioIdQuery(Guid Id) : IQuery<IEnumerable<EmpresaFuncionarioDTO>>;
}
