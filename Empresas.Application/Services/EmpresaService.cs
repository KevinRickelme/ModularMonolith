using AutoMapper;
using Common.Application.DTOs;
using Empresas.Application.Abstractions;
using Empresas.Domain.Abstractions;
using Empresas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Services
{
    public class EmpresaService(IEmpresaRepository empresaRepository, IMapper mapper) : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository = empresaRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            var empresa = await _empresaRepository.GetByIdAsync(id, cancellationToken);
            if (empresa == null)
            {
                return await Task.FromResult(false);
            }
            return await _empresaRepository.RemoveAsync(empresa, cancellationToken);
        }

        public async Task<EmpresaDTO?> UpdateAsync(EmpresaDTO empresa, CancellationToken cancellationToken)
        {
            var empresaEntity = _mapper.Map<Empresa>(empresa);
            bool sucesso = await _empresaRepository.UpdateAsync(empresaEntity, cancellationToken);
            if (!sucesso)
            {
                return null;
            }
            return empresa;
        }
    }
}
