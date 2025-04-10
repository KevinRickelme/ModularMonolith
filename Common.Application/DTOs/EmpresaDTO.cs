using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.DTOs
{
    public sealed record EmpresaDTO(
            Guid Id,
            string Nome,
            string CNPJ,
            DateTime DataCriacao,
            string Endereco,
            string Telefone,
            string Email,
            string Site,
            string Descricao
        );
}
