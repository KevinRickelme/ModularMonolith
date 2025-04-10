using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Command.Update
{
    public sealed record UpdateEmpresaFuncionarioCommand(
            Guid Id,
            DateTime DataAdmissao,
            string Cargo,
            string Departamento) : ICommand<EmpresaFuncionarioDTO>;
}
