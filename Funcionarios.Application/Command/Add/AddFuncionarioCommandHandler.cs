using Common.Application.Abstractions.Messaging;
using Funcionarios.Domain.Abstractions;
using Funcionarios.Domain.Entities;
using SharedKernel;
using SharedKernel.Errors;

namespace Funcionarios.Application.Command.Add
{
    public class AddFuncionarioCommandHandler(IFuncionarioRepository funcionarioRepository) : ICommandHandler<AddFuncionarioCommand, Guid>
    {
        private readonly IFuncionarioRepository _funcionarioRepository = funcionarioRepository;

        public async Task<Result<Guid>> Handle(AddFuncionarioCommand request, CancellationToken cancellationToken)
        {
            //verify if funcionario already exists
            var funcionarioExists = await _funcionarioRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (funcionarioExists != null)
            {
                return Result.Failure<Guid>(FuncionarioErrors.EmailNotUnique);
            }
            //create funcionario

            var funcionario = new Funcionario(request.Nome,request.Idade, request.Email);
            await _funcionarioRepository.AddAsync(funcionario, cancellationToken);
            return funcionario.Id;
        }
    }
}
