using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Errors
{
    public static class EmpresaErrors
    {
        public static Error NotFound(Guid empresaId) => Error.NotFound(
            "Empresa.NotFound",
            $"A Empresa com Id = '{empresaId}' não foi Encontrado");

        public static Error NotFound(List<Guid> Ids) => Error.NotFound(
            "Empresa.NotFound",
            $"Não foi Possível Encontrar Nenhuma Empresa com os IDs Informados {Ids}");

        public static Error NotFound(string CNPJ) => Error.NotFound(
            "Empresa.NotFound",
            $"A Empresa com CNPJ = '{CNPJ}' não foi Encontrada");

        public static readonly Error CnpjNotUnique = Error.Conflict(
            "Empresa.CnpjNotUnique",
            "O CNPJ Informado Já Possui um Cadastro com outra Empresa");

        public static readonly Error SemEmpresas = Error.Conflict(
           "Empresa.ListNotFound",
           "Não Existem Empresas Cadastradas na Base de Dados");

    }
}
