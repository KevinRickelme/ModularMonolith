using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using EmpresasFuncionarios.Application.Abstractions;
using EmpresasFuncionarios.Domain.Abstractions;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Command.Update
{
    public class UpdateEmpresaFuncionarioCommandHandler(IEmpresaFuncionarioService empresaFuncionarioService, IEmpresaFuncionarioRepository empresaFuncionarioRepository) : ICommandHandler<UpdateEmpresaFuncionarioCommand, EmpresaFuncionarioDTO>
    {
        private readonly IEmpresaFuncionarioService _empresaFuncionarioService = empresaFuncionarioService;
        private readonly IEmpresaFuncionarioRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;
        public async Task<Result<EmpresaFuncionarioDTO>> Handle(UpdateEmpresaFuncionarioCommand request, CancellationToken cancellationToken)
        {
            var empresaFuncionario = await _empresaFuncionarioRepository.GetByIdAsync(request.Id, cancellationToken);
            if (empresaFuncionario == null)
            {
                return Result.Failure<EmpresaFuncionarioDTO>(EmpresaFuncionarioErrors.NotFound(request.Id, TipoIdEmpresaFuncionario.EmpresaFuncionario));
            }

            return await _empresaFuncionarioService.UpdateAsync(new EmpresaFuncionarioDTO(request.Id, empresaFuncionario.FuncionarioId, empresaFuncionario.EmpresaId, request.DataAdmissao, request.Cargo, request.Departamento), cancellationToken);
        }
    }

}
