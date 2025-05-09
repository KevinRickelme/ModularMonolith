using Common.Application.Abstractions.Messaging;

namespace EmpresasFuncionarios.Application.Command.CargoAlterado
{
    public sealed record CargoAlteradoCommand(Guid StreamId,
        string Cargo) : ICommand<Guid>; 
}
