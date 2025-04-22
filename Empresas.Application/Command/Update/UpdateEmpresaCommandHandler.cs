using AutoMapper;
using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using Empresas.Application.Abstractions;
using Empresas.Domain.Abstractions;
using FluentValidation;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Command.Update
{
    public class UpdateEmpresaCommandHandler(IEmpresaService empresaService, IValidator<EmpresaDTO> validator) : ICommandHandler<UpdateEmpresaCommand, EmpresaDTO>
    {
        private readonly IEmpresaService _empresaService = empresaService;
        private readonly IValidator<EmpresaDTO> _validator = validator;
        public async Task<Result<EmpresaDTO>> Handle(UpdateEmpresaCommand request, CancellationToken cancellationToken)
        {
            var empresa = await _empresaService.GetByIdAsync(request.Id, cancellationToken);
            if (empresa == null)
            {
                return Result.Failure<EmpresaDTO>(EmpresaErrors.NotFound(request.Id));
            }

            var empresaDTO = new EmpresaDTO(request.Id, request.Nome, empresa.CNPJ, empresa.DataCriacao, request.Endereco, request.Telefone, request.Email, request.Site, request.Descricao);
            var validationResult = _validator.Validate(empresaDTO);
            if (!validationResult.IsValid)
            {
                var erros = validationResult.ToDictionary();
                return Result.Failure<EmpresaDTO>(errors: erros);
            }

            return await _empresaService.UpdateAsync(empresaDTO, cancellationToken);
        }
    }
}
