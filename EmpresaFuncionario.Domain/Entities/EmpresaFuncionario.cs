using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Domain.Entities
{
    public sealed class EmpresaFuncionario
    {
        public Guid Id { get; private set; }
        public Guid FuncionarioId { get; private set; }
        public Guid EmpresaId { get; private set; }
        public DateTime DataAdmissao { get; private set; }
        public string Cargo { get; private set; }
        public string Departamento { get; private set; }

        public EmpresaFuncionario()
        {
            Cargo = string.Empty;
            Departamento = string.Empty;
            DataAdmissao = DateTime.MinValue;
        }

        public EmpresaFuncionario(Guid funcionarioId, Guid empresaId, DateTime dataAdmissao, string cargo, string departamento)
        {
            ValidateDomain(funcionarioId, empresaId, dataAdmissao, cargo, departamento);
            Id = Guid.NewGuid();
            FuncionarioId = funcionarioId;
            EmpresaId = empresaId;
            Cargo = cargo;
            Departamento = departamento;
            DataAdmissao = dataAdmissao;
        }

        private static void ValidateDomain(Guid funcionarioId, Guid empresaId, DateTime dataAdmissao, string cargo, string departamento)
        {
            if (funcionarioId == Guid.Empty)
                throw new ArgumentException("Funcionário ID não pode ser vazio.", nameof(funcionarioId));
            if (empresaId == Guid.Empty)
                throw new ArgumentException("Empresa ID não pode ser vazio.", nameof(empresaId));
            if (dataAdmissao == DateTime.MinValue)
                throw new ArgumentException("Data de admissão não pode ser a data mínima.", nameof(dataAdmissao));
            if (string.IsNullOrWhiteSpace(cargo))
                throw new ArgumentException("Cargo não pode ser vazio ou nulo.", nameof(cargo));
            if (string.IsNullOrWhiteSpace(departamento))
                throw new ArgumentException("Departamento não pode ser vazio ou nulo.", nameof(departamento));
        }
    }
}
