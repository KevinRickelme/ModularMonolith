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
using System.Threading;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Services
{
    public class EmpresaFuncionarioReadModelService(IEmpresaFuncionarioReadRepository empresaFuncionarioRepository, IMapper mapper) : IEmpresaFuncionarioReadModelService
    {
        private readonly IEmpresaFuncionarioReadRepository _empresaFuncionarioRepository = empresaFuncionarioRepository;

        private readonly IMapper _mapper = mapper;

        public async Task<Guid> AddAsync(EmpresaFuncionarioDTO empresaFuncionario, CancellationToken cancellationToken)
        {
            return await _empresaFuncionarioRepository.AddAsync(_mapper.Map<EmpresaFuncionario>(empresaFuncionario), cancellationToken);
        }

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

        public async Task<IEnumerable<EmpresaFuncionarioDTO>> GetByEmpresaIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<EmpresaFuncionarioDTO>>(await _empresaFuncionarioRepository.GetByEmpresaIdAsync(Id, cancellationToken));
        }

        public async Task<EmpresaFuncionarioDTO?> GetByFuncionarioIdAndEmpresaIdAsync(Guid funcionarioId, Guid empresaId, CancellationToken cancellationToken)
        {
            return _mapper.Map<EmpresaFuncionarioDTO>(await _empresaFuncionarioRepository.GetByFuncionarioIdAndEmpresaIdAsync(funcionarioId, empresaId, cancellationToken));
        }

        public async Task<IEnumerable<EmpresaFuncionarioDTO>> GetByFuncionarioIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<EmpresaFuncionarioDTO>>(await _empresaFuncionarioRepository.GetByFuncionarioIdAsync(Id, cancellationToken));
        }

        public async Task<EmpresaFuncionarioDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _mapper.Map<EmpresaFuncionarioDTO>(await _empresaFuncionarioRepository.GetByIdAsync(id, cancellationToken));
        }

        public async Task<EmpresaFuncionarioDTO> GetByStreamIdAsync(Guid streamId, CancellationToken cancellationToken)
        {
            return _mapper.Map<EmpresaFuncionarioDTO>(await _empresaFuncionarioRepository.GetByStreamIdAsync(streamId, cancellationToken));
        }
    }
}
