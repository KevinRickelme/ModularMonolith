using Common.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Command.Add
{
    public sealed record AddEmpresaCommand(
        string Nome,
        string CNPJ,
        string Endereco,
        string Telefone,
        string Email,
        string Site,
        string Descricao
    ) : ICommand<Guid>;
}
