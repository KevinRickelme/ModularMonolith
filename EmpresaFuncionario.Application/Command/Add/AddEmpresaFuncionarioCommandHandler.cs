using Common.Application.Abstractions.Messaging;
using Empresas.Application.Contracts;
using EmpresasFuncionarios.Domain.Abstractions;
using EmpresasFuncionarios.Domain.Entities;
using Funcionarios.Application.Contracts;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Command.Add
{
    public class AddEmpresaFuncionarioCommandHandler(IEmpresaFuncionarioRepository empresaFuncionarioRepository, IEmpresaConsultaService empresaConsultaService, IFuncionarioConsultaService funcionarioConsultaService) : ICommandHandler<AddEmpresaFuncionarioCommand, Guid>
    {
        private readonly IEmpresaFuncionarioRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;
        private readonly IEmpresaConsultaService _empresaConsultaService = empresaConsultaService;
        private readonly IFuncionarioConsultaService _funcionarioConsultaService = funcionarioConsultaService;

        public async Task<Result<Guid>> Handle(AddEmpresaFuncionarioCommand request, CancellationToken cancellationToken)
        {
            //verificar se existe primeiro
            var empresaFuncionario = await _empresaFuncionarioRepository.GetByFuncionarioIdAndEmpresaIdAsync(request.FuncionarioId, request.EmpresaId, cancellationToken);
            if(empresaFuncionario != null)
            {
                return Result.Failure<Guid>(EmpresaFuncionarioErrors.VinculoNotUnique);
            }

            //verifica se empresa existe
            var empresa = await _empresaConsultaService.GetByIdAsync(request.EmpresaId, cancellationToken);
            if(empresa.Value == null)
            {
                return Result.Failure<Guid>(EmpresaErrors.NotFound(request.EmpresaId));
            }

            //verifica se funcionario existe
            var funcionario = await _funcionarioConsultaService.GetByIdAsync(request.FuncionarioId, cancellationToken);
            if (funcionario.Value == null)
            {
                return Result.Failure<Guid>(FuncionarioErrors.NotFound(request.FuncionarioId));
            }

            var result = await _empresaFuncionarioRepository.AddAsync(new EmpresaFuncionario(
                request.FuncionarioId,
                request.EmpresaId,
                request.DataAdmissao,
                request.Cargo,
                request.Departamento), cancellationToken);

            return result;
        }
    }
}
