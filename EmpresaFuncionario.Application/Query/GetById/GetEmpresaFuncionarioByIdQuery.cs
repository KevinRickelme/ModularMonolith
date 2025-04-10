using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;

namespace EmpresasFuncionarios.Application.Query.GetById
{
    public sealed record GetEmpresaFuncionarioByIdQuery(Guid Id) : IQuery<EmpresaFuncionarioDTO>;
}
