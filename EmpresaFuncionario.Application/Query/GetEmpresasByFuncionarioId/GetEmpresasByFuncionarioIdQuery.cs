using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Query.GetEmpresasByFuncionarioId
{
    public sealed record GetEmpresasByFuncionarioIdQuery(Guid FuncionarioId) : IQuery<IEnumerable<EmpresaDTO>>;
}
