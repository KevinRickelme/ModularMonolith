﻿using Azure.Core;
using Common.Infra.Data.Abstractions;
using EmpresasFuncionarios.Domain.Abstractions;
using EmpresasFuncionarios.Domain.Entities;
using EmpresasFuncionarios.Domain.Events;
using EmpresasFuncionarios.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System.Text.Json;

namespace EmpresasFuncionarios.Infra.Data.Repositories
{
    public class EmpresaFuncionarioReadRepository(EmpresaFuncionarioDbContext context) : IEmpresaFuncionarioReadRepository
    {
        private readonly EmpresaFuncionarioDbContext _context = context;

        public async Task<Guid> AddAsync(EmpresaFuncionario empresaFuncionario, CancellationToken cancellationToken)
        {
            await _context.EmpresasFuncionarios.AddAsync(empresaFuncionario, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return empresaFuncionario.Id;
        }

        public async Task<bool> RemoveAsync(EmpresaFuncionario empresaFuncionario, CancellationToken cancellationToken)
        {
            _context.EmpresasFuncionarios.Remove(empresaFuncionario);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> UpdateAsync(EmpresaFuncionario empresaFuncionario, CancellationToken cancellationToken)
        {
            _context.EmpresasFuncionarios.Update(empresaFuncionario);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<IEnumerable<EmpresaFuncionario>> GetByEmpresaIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.EmpresasFuncionarios
                .AsNoTracking()
                .Where(e => e.EmpresaId == Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<EmpresaFuncionario?> GetByFuncionarioIdAndEmpresaIdAsync(Guid funcionarioId, Guid empresaId, CancellationToken cancellationToken)
        {
            return await _context.EmpresasFuncionarios
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.FuncionarioId == funcionarioId && e.EmpresaId == empresaId, cancellationToken);
        }

        public async Task<IEnumerable<EmpresaFuncionario>> GetByFuncionarioIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.EmpresasFuncionarios
                .AsNoTracking()
                .Where(e => e.FuncionarioId == Id)
                .ToListAsync(cancellationToken);
        }

        public async Task<EmpresaFuncionario?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.EmpresasFuncionarios
                .AsNoTracking()
                .Where(e => e.StreamId == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<EmpresaFuncionario?> GetByStreamIdAsync(Guid streamId, CancellationToken cancellationToken)
        {
            return await _context.EmpresasFuncionarios
                .AsNoTracking()
                .Where(e => e.StreamId == streamId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<EmpresaFuncionario>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.EmpresasFuncionarios
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
