﻿using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmpresasFuncionarios.Application.Command.Add
{
    public sealed record AddEmpresaFuncionarioCommand(Guid FuncionarioId,
            Guid EmpresaId,
            DateTime DataAdmissao,
            string Cargo,
            string Departamento) : ICommand<Guid>;
}
