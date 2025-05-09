using Funcionarios.Application.Command.Add;
using Common.Application.DTOs;
using Funcionarios.Application.Query.GetByNome;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.AccessControl;
using SharedKernel.Errors;
using Funcionarios.Application.Query.GetAll;
using Empresas.Application.Query.GetAll;
using EmpresasFuncionarios.Application.Query.GetAll;
using Modular_Monolith_API.ViewModels;
using Funcionarios.Application.Query.GetById;

namespace Modular_Monolith_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuncionarioController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediatR = mediator;

        [HttpGet(Name = "GetFuncionariosByNome")]
        public async Task<ActionResult> GetByNome(string nome)
        {
            var result = (await _mediatR.Send(new GetFuncionarioByNomeQuery(nome)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                if (result != null && result.Error == FuncionarioErrors.NotFound(nome))
                {
                    return NotFound(result.Error);
                }
                else
                {
                    return BadRequest(result?.Error);
                }
            }
        }

        [HttpPost(Name = "AddFuncionario")]
        public async Task<ActionResult> Add(FuncionarioDTO funcionario)
        {
            var result = (await _mediatR.Send(new AddFuncionarioCommand(funcionario.Nome, funcionario.Idade, funcionario.Email)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                if (result != null && result.Error == FuncionarioErrors.EmailNotUnique)
                {
                    return Conflict(result.Error);
                }
                else
                {
                    return BadRequest(result?.Error);
                }
            }
        }

        [HttpGet("GetAll", Name = "GetAllFuncionarios")]
        public async Task<ActionResult> GetAll()
        {
            var result = (await _mediatR.Send(new GetAllFuncionariosQuery()));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result?.Error);
            }
        }

        [HttpGet("GetAllWithEmpresaFuncionario", Name = "GetAllWithEmpresaFuncionario")]
        public async Task<ActionResult> GetAllWithEmpresaFuncionario()
        {
            //var result = (await _mediatR.Send(new GetAllFuncionariosQuery()));
            var empresaFuncionario = await _mediatR.Send(new GetAllEmpresasFuncionariosQuery());
            var funcionarioIds = empresaFuncionario.Value.Select(x => x.FuncionarioId);
            List<FuncionarioViewModel> funcionarios = new();
            foreach (var funcionarioId in funcionarioIds)
            {
                var funcionario = await _mediatR.Send(new GetFuncionarioByIdQuery(funcionarioId));
                if (funcionario.IsSuccess)
                {
                    FuncionarioViewModel f = new FuncionarioViewModel()
                    {
                        Id = funcionarioId,
                        Nome = funcionario.Value.Nome,
                        Cargo = empresaFuncionario.Value.Where(e => e.FuncionarioId == funcionarioId).Select(e => e.Cargo).FirstOrDefault(),
                        Departamento = empresaFuncionario.Value.Where(e => e.FuncionarioId == funcionarioId).Select(e => e.Departamento).FirstOrDefault(),
                        Status = "Ativo"
                    };

                    funcionarios.Add(f);
                }
            }
            if (empresaFuncionario.IsSuccess)
            {
                var resultado = new
                {
                    dados = funcionarios,
                    total = funcionarios.Count
                };
                return Ok(resultado);
            }
            else
            {
                return BadRequest(empresaFuncionario?.Error);
            }
        }

    }
}
