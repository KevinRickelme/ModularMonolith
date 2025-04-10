using Common.Application.Abstractions.Messaging;
using EmpresasFuncionarios.Application.Abstractions;
using EmpresasFuncionarios.Domain.Abstractions;
using MediatR;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Command.Delete
{
    public class RemoveEmpresaFuncionarioQueryHandler(IEmpresaFuncionarioService empresaFuncionarioService, IEmpresaFuncionarioRepository empresaFuncionarioRepository) : ICommandHandler<RemoveEmpresaFuncionarioCommand, bool>
    {
        private readonly IEmpresaFuncionarioService _empresaFuncionarioService = empresaFuncionarioService;
        private readonly IEmpresaFuncionarioRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;
        public async Task<Result<bool>> Handle(RemoveEmpresaFuncionarioCommand request, CancellationToken cancellationToken)
        {
            var vinculo = await _empresaFuncionarioRepository.GetByIdAsync(request.Id, cancellationToken);
            if(vinculo == null)
            {
                return Result.Failure<bool>(EmpresaFuncionarioErrors.NotFound(request.Id, TipoIdEmpresaFuncionario.EmpresaFuncionario));
            }
            return await _empresaFuncionarioService.RemoveAsync(request.Id, cancellationToken);
        }
    }
}
