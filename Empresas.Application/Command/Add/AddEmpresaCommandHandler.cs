using Common.Application.Abstractions.Messaging;
using Empresas.Domain.Abstractions;
using Empresas.Domain.Entities;
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
    public class AddEmpresaCommandHandler(IEmpresaRepository empresaRepository) : ICommandHandler<AddEmpresaCommand, Guid>
    {
        private readonly IEmpresaRepository _empresaRepository = empresaRepository;
        public async Task<Result<Guid>> Handle(AddEmpresaCommand request, CancellationToken cancellationToken)
        {
            var empresaExists = await _empresaRepository.GetByCnpjAsync(request.CNPJ, cancellationToken);
            if (empresaExists != null)
            {
                return Result.Failure<Guid>(EmpresaErrors.CnpjNotUnique);
            }

            var empresa = new Empresa(request.Nome, request.CNPJ, DateTime.Now, request.Endereco, request.Telefone, request.Email, request.Site, request.Descricao);
            await _empresaRepository.AddAsync(empresa, cancellationToken);
            return empresa.Id;
        }
    }
}
