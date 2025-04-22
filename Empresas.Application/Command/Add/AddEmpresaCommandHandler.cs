using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using Empresas.Application.Abstractions;
using Empresas.Domain.Abstractions;
using Empresas.Domain.Entities;
using FluentValidation;
using MediatR;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Command.Add
{
    public class AddEmpresaCommandHandler(IEmpresaService empresaService, IValidator<EmpresaDTO> validator) : ICommandHandler<AddEmpresaCommand, Guid>
    {
        private readonly IEmpresaService _empresaService = empresaService;
        private readonly IValidator<EmpresaDTO> _validator = validator;
        public async Task<Result<Guid>> Handle(AddEmpresaCommand request, CancellationToken cancellationToken)
        {
            var empresaExists = await _empresaService.GetByCnpjAsync(request.CNPJ, cancellationToken);
            if (empresaExists != null)
            {
                return Result.Failure<Guid>(EmpresaErrors.CnpjNotUnique);
            }

            var empresa = new EmpresaDTO(Guid.NewGuid(), request.Nome, request.CNPJ, DateTime.Now, request.Endereco, request.Telefone, request.Email, request.Site, request.Descricao);
            var validationResult = _validator.Validate(empresa);
            if (!validationResult.IsValid)
            {
                var erros = validationResult.ToDictionary();
                return Result.Failure<Guid>(errors: erros);
            }
                
            await _empresaService.AddAsync(empresa, cancellationToken);
            return empresa.Id;
        }
    }
}
