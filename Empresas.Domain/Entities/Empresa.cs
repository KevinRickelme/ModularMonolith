using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Empresas.Domain.Entities
{
    public sealed class Empresa
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string CNPJ { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public string Endereco { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public string Site { get; private set; }
        public string Descricao { get; private set; }

        public Empresa() 
        {
            Nome = string.Empty;
            CNPJ = string.Empty;
            DataCriacao = DateTime.MinValue;
            Endereco = string.Empty;
            Telefone = string.Empty;
            Email = string.Empty;
            Site = string.Empty;
            Descricao = string.Empty;
        }

        public Empresa(string nome, string cnpj, DateTime dataCriacao, string endereco, string telefone, string email, string site, string descricao)
        {
            ValidateDomain(nome, cnpj, dataCriacao, email);
            Id = Guid.NewGuid();
            Nome = nome;
            CNPJ = cnpj;
            DataCriacao = dataCriacao;
            Endereco = endereco;
            Telefone = telefone;
            Email = email;
            Site = site;
            Descricao = descricao;
        }

        private static void ValidateDomain(string nome, string cnpj, DateTime dataCriacao, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio ou nulo.");

            if (cnpj.Length != 14 || !cnpj.All(char.IsDigit))
                throw new ArgumentException("CNPJ inválido.");

            if (dataCriacao == DateTime.MinValue)
                throw new ArgumentException("Data de criação inválida.");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                throw new ArgumentException("Email inválido.");
        }
    }
}
