using AutoMapper;
using Common.Application.Abstractions.Messaging;
using Common.Application.DTOs;
using EmpresasFuncionarios.Domain.Abstractions;
using SharedKernel;
using SharedKernel.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Query.GetById
{
    public class GetEmpresaFuncionarioByIdQueryHandler(IEmpresaFuncionarioRepository empresaFuncionarioRepository, IMapper mapper) : IQueryHandler<GetEmpresaFuncionarioByIdQuery, EmpresaFuncionarioDTO>
    {
        private readonly IEmpresaFuncionarioRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<EmpresaFuncionarioDTO>> Handle(GetEmpresaFuncionarioByIdQuery request, CancellationToken cancellationToken)
        {
            var empresaFuncionario = await _empresaFuncionarioRepository.GetByIdAsync(request.Id, cancellationToken);
            if (empresaFuncionario is null)
                return Result.Failure<EmpresaFuncionarioDTO>(EmpresaFuncionarioErrors.NotFound(request.Id, TipoIdEmpresaFuncionario.EmpresaFuncionario));

            var empresaFuncionarioDTO = _mapper.Map<EmpresaFuncionarioDTO>(empresaFuncionario);
            return Result.Success(empresaFuncionarioDTO);
        }
    }
}
