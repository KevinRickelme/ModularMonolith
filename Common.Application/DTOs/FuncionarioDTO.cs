using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.DTOs
{
    public sealed record FuncionarioDTO(
            string Nome,
            int Idade,
            string Email
        );
}
