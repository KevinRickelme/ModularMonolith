using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcionarios.Domain.Entities
{
    public sealed class Funcionario : Entity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Email { get; set; }

        public Funcionario() 
        {
            Email = String.Empty;
            Nome  = String.Empty;
        }

        public Funcionario(string nome, int idade, string email)
        {
            ValidateDomain(nome, idade, email);
            Id = Guid.NewGuid();
            Nome = nome;
            Idade = idade;
            Email = email;
        }

        private static void ValidateDomain(string nome, int idade, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio ou nulo.");
            if (idade <= 0)
                throw new ArgumentException("Idade deve ser maior que zero.");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("Email inválido.");
        }
    }
}
