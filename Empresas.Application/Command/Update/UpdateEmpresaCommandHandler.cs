using AutoMapper;
using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using Empresas.Application.Abstractions;
using Empresas.Domain.Abstractions;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Command.Update
{
    public class UpdateEmpresaCommandHandler(IEmpresaRepository empresaRepository, IEmpresaService empresaService) : ICommandHandler<UpdateEmpresaCommand, EmpresaDTO>
    {
        private readonly IEmpresaRepository _empresaRepository = empresaRepository;
        private readonly IEmpresaService _empresaService = empresaService;
        public async Task<Result<EmpresaDTO>> Handle(UpdateEmpresaCommand request, CancellationToken cancellationToken)
        {
            var empresa = await _empresaRepository.GetByIdAsync(request.Id, cancellationToken);
            if (empresa == null)
            {
                return Result.Failure<EmpresaDTO>(EmpresaErrors.NotFound(request.Id));
            }

            var empresaDTO = new EmpresaDTO(request.Id, request.Nome, empresa.CNPJ, empresa.DataCriacao, request.Endereco, request.Telefone, request.Email, request.Site, request.Descricao);
            return await _empresaService.UpdateAsync(empresaDTO, cancellationToken);
        }
    }
}
