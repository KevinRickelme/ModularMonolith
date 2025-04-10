using Funcionarios.Domain.Abstractions;
using Funcionarios.Domain.Entities;
using Funcionarios.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcionarios.Infra.Data.Repositories
{
    public class FuncionarioRepository(FuncionarioDbContext context) : IFuncionarioRepository
    {
        private readonly FuncionarioDbContext _context = context;

        public async Task AddAsync(Funcionario funcionario, CancellationToken cancellationToken)
        {
            await _context.Funcionarios.AddAsync(funcionario, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Funcionario>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Funcionarios.ToListAsync(cancellationToken);
        }

        public async Task<Funcionario?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Funcionarios.Where(f => f.Email == email).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Funcionario?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Funcionarios.Where(f => f.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> GetTotalCount(CancellationToken cancellationToken)
        {
            return await _context.Funcionarios.CountAsync(cancellationToken);
        }

        public async Task UpdateAsync(Funcionario funcionario, CancellationToken cancellationToken)
        {
            _context.Update(funcionario);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Funcionario>?> GetByNomeAsync(string nome, CancellationToken cancellationToken)
        {
            return await _context.Funcionarios.Where(f => f.Nome == nome).ToListAsync(cancellationToken);
        }

        public async Task<List<Funcionario>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
        {
            return await _context.Funcionarios.AsNoTracking().Where(f=>ids.Contains(f.Id)).ToListAsync(cancellationToken);

        }
    }
}
