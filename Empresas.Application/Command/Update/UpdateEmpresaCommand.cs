using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Command.Update
{
    public sealed record UpdateEmpresaCommand(
        Guid Id,
        string Nome,
        string Endereco,
        string Telefone,
        string Email,
        string Site,
        string Descricao
    ) : ICommand<EmpresaDTO>;
}
