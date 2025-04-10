using Empresas.Domain.Abstractions;
using Empresas.Domain.Entities;
using Empresas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Infra.Data.Repositories
{
    public class EmpresaRepository(EmpresaDbContext context) : IEmpresaRepository
    {
        private readonly EmpresaDbContext _context = context;
        public async Task<Guid> AddAsync(Empresa empresa, CancellationToken cancellationToken)
        {
            await _context.Empresas.AddAsync(empresa, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return empresa.Id;
        }

        public async Task<bool> RemoveAsync(Empresa empresa, CancellationToken cancellationToken)
        {
            _context.Empresas.Remove(empresa);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> UpdateAsync(Empresa empresa, CancellationToken cancellationToken)
        {
            _context.Empresas.Update(empresa);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Empresas.ToListAsync(cancellationToken);
        }

        public async Task<Empresa?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken)
        {
            return await _context.Empresas.AsNoTracking()
                .FirstOrDefaultAsync(e => e.CNPJ == cnpj, cancellationToken);
        }

        public async Task<Empresa?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Empresas.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<Empresa?> GetByNomeAsync(string nome, CancellationToken cancellationToken)
        {
            return await _context.Empresas.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Nome == nome, cancellationToken);
        }

        public async Task<IEnumerable<Empresa>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            return await _context.Empresas.AsNoTracking()
                .Where(e => ids.Contains(e.Id))
                .ToListAsync(cancellationToken);
        }
    }
}
