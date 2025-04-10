using AutoMapper;
using Common.Application.DTOs;
using Empresas.Domain.Abstractions;
using EmpresasFuncionarios.Application.Abstractions;
using EmpresasFuncionarios.Domain.Abstractions;
using EmpresasFuncionarios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Services
{
    public class EmpresaFuncionarioService(IEmpresaFuncionarioRepository empresaFuncionarioRepository, IMapper mapper) : IEmpresaFuncionarioService
    {
        private readonly IEmpresaFuncionarioRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            var empresaFuncionario = await _empresaFuncionarioRepository.GetByIdAsync(id, cancellationToken);
            if (empresaFuncionario == null)
            {
                return await Task.FromResult(false);
            }
            return await _empresaFuncionarioRepository.RemoveAsync(empresaFuncionario, cancellationToken);
        }

        public async Task<EmpresaFuncionarioDTO?> UpdateAsync(EmpresaFuncionarioDTO empresaFuncionario, CancellationToken cancellationToken)
        {
            var empresaEntity = _mapper.Map<EmpresaFuncionario>(empresaFuncionario);
            bool sucesso = await _empresaFuncionarioRepository.UpdateAsync(empresaEntity, cancellationToken);
            if (!sucesso)
            {
                return null;
            }
            return empresaFuncionario;
        }
    }
}
