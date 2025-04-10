using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;

namespace Funcionarios.Application.Query.GetAll
{
    public sealed record GetAllFuncionariosQuery() : IQuery<IEnumerable<FuncionarioDTO>>;
}
