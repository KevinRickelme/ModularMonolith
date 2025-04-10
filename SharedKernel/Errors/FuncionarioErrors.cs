using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Errors
{
    public static class FuncionarioErrors
    {
        public static Error NotFound(Guid funcionarioId) => Error.NotFound(
    "Funcionario.NotFound",
    $"O Funcionário com Id = '{funcionarioId}' não foi Encontrado");

        public static Error NotFound() => Error.NotFound(
   "Funcionario.NotFound",
   $"Não Existem Funcionários Cadastrados na Base de Dados");

        public static Error NotFound(List<Guid> funcionarioId) => Error.NotFound(
    "Funcionario.NotFound",
    $"Os Funcionários da Lista não foram Encontrados");

        public static Error NotFound(string nome) => Error.NotFound(
    "Funcionario.NotFound",
    $"O(s) Funcionário(s) com Nome = '{nome}' não Encontrado(s)");

        public static readonly Error NotFoundByEmail = Error.NotFound(
            "Funcionario.NotFoundByEmail",
            "O Email Informado Não Possui Vínculo com Nenhum Funcionário");

        public static readonly Error EmailNotUnique = Error.Conflict(
            "Funcionario.EmailNotUnique",
            "O Email Informado Já Possui um Vínculo com um Funcionário");
    }
}
