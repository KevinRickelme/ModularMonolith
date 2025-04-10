using Funcionarios.Application.Command.Add;
using Common.Application.DTOs;
using Funcionarios.Application.Query.GetByNome;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.AccessControl;
using SharedKernel.Errors;
using Funcionarios.Application.Query.GetAll;

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
            var result = (await _mediatR.Send(new AddFuncionarioCommand(funcionario.Nome,funcionario.Idade,funcionario.Email)));
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

    }
}
