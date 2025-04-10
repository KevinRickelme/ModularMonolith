using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Errors
{
    public static class EmpresaFuncionarioErrors
    {
        public static Error NotFound(Guid Id, TipoIdEmpresaFuncionario tipo) => Error.NotFound(
            "EmpresaFuncionario.NotFound",
            $"O Vínculo entre Empresa e Funcionario com {tipo} Id = '{Id}' não foi Encontrado");

        public static Error NotFound(Guid FuncionarioId, Guid EmpresaId) => Error.NotFound(
            "EmpresaFuncionario.NotFound",
            $"O Vínculo entre Empresa e Funcionario com Empresa Id = '{EmpresaId}' e Funcionario Id = '{FuncionarioId}' não foi Encontrado");


        public static readonly Error VinculoNotUnique = Error.Conflict(
            "Empresa.VinculoNotUnique",
            "Já exite um Vínculo com os mesmos IDs de Empresa e Funcionario");


    }

    public enum TipoIdEmpresaFuncionario
    {
        EmpresaFuncionario,
        Funcionario,
        Empresa

    }
}
