﻿using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Query.GetFuncionariosByEmpresaId
{
    public sealed record GetFuncionariosByEmpresaIdQuery(Guid EmpresaId) : IQuery<IEnumerable<FuncionarioDTO>>;
}
