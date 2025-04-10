using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.DTOs
{
    public sealed record EmpresaFuncionarioDTO(
            Guid Id,
            Guid FuncionarioId,
            Guid EmpresaId,
            DateTime DataAdmissao,
            string Cargo,
            string Departamento
        );
}
