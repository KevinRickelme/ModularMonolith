using EmpresasFuncionarios.Application.Command.Add;
using EmpresasFuncionarios.Application.Command.Delete;
using EmpresasFuncionarios.Application.Command.Update;
using EmpresasFuncionarios.Application.Query.GetByEmpresaId;
using EmpresasFuncionarios.Application.Query.GetByFuncionarioAndEmpresaId;
using EmpresasFuncionarios.Application.Query.GetByFuncionarioId;
using EmpresasFuncionarios.Application.Query.GetById;
using EmpresasFuncionarios.Application.Query.GetEmpresasByFuncionarioId;
using EmpresasFuncionarios.Application.Query.GetFuncionariosByEmpresaId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modular_Monolith_API.ViewModels;
using SharedKernel.Errors;

namespace Modular_Monolith_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaFuncionarioController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;
        [HttpPost("Add", Name = "AddEmpresaFuncionario")]
        public async Task<IActionResult> AddEmpresaFuncionario(CreateEmpresaFuncionarioViewModel empresaFuncionario)
        {
            var result = (await _mediator.Send(new AddEmpresaFuncionarioCommand(empresaFuncionario.FuncionarioId, empresaFuncionario.EmpresaId, empresaFuncionario.DataAdmissao, empresaFuncionario.Cargo, empresaFuncionario.Departamento)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                if (result != null && result.Error == EmpresaFuncionarioErrors.VinculoNotUnique)
                {
                    return Conflict(result.Error);
                }
                else
                {
                    return BadRequest(result?.Error);
                }
            }
        }

        [HttpDelete("Delete", Name = "DeleteEmpresaFuncionario")]
        public async Task<IActionResult> DeleteEmpresaFuncionario(Guid id)
        {
            var result = (await _mediator.Send(new RemoveEmpresaFuncionarioCommand(id)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result?.Error);
            }
        }

        [HttpPatch("Update", Name = "UpdateEmpresaFuncionario")]
        public async Task<IActionResult> UpdateEmpresaFuncionario(UpdateEmpresaFuncionarioViewModel empresaFuncionario)
        {
            var result = (await _mediator.Send(new UpdateEmpresaFuncionarioCommand(empresaFuncionario.Id, empresaFuncionario.DataAdmissao, empresaFuncionario.Cargo, empresaFuncionario.Departamento)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result?.Error);
            }
        }

        [HttpGet("GetById", Name = "GetEmpresaFuncionarioById")]
        public async Task<IActionResult> GetEmpresaFuncionarioById(Guid id)
        {
            var result = (await _mediator.Send(new GetEmpresaFuncionarioByIdQuery(id)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result?.Error);
            }
        }

        [HttpGet("GetByEmpresaId", Name = "GetEmpresaFuncionarioByEmpresaId")]
        public async Task<IActionResult> GetEmpresaFuncionarioByEmpresaId(Guid empresaId)
        {
            var result = (await _mediator.Send(new GetEmpresaFuncionarioByEmpresaIdQuery(empresaId)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result?.Error);
            }
        }

        [HttpGet("GetByFuncionarioId", Name = "GetEmpresaFuncionarioByFuncionarioId")]
        public async Task<IActionResult> GetEmpresaFuncionarioByFuncionarioId(Guid funcionarioId)
        {
            var result = (await _mediator.Send(new GetEmpresaFuncionarioByFuncionarioIdQuery(funcionarioId)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result?.Error);
            }
        }
        [HttpGet("GetByFuncionarioIdAndEmpresaId", Name = "GetEmpresaFuncionarioByFuncionarioIdAndEmpresaId")]
        public async Task<IActionResult> GetEmpresaFuncionarioByFuncionarioIdAndEmpresaId(Guid funcionarioId, Guid empresaId)
        {
            var result = (await _mediator.Send(new GetEmpresaFuncionarioByFuncionarioIdAndEmpresaIdQuery(funcionarioId, empresaId)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result?.Error);
            }
        }

        [HttpGet("GetEmpresasByFuncionarioId", Name = "GetEmpresasByFuncionarioId")]
        public async Task<IActionResult> GetEmpresasByFuncionarioId(Guid funcionarioId)
        {
            var result = (await _mediator.Send(new GetEmpresasByFuncionarioIdQuery(funcionarioId)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result?.Error);
            }
        }

        [HttpGet("GetFuncionariosByEmpresaId", Name = "GetFuncionariosByEmpresaId")]
        public async Task<IActionResult> GetFuncionariosByEmpresaId(Guid empresaId)
        {
            var result = (await _mediator.Send(new GetFuncionariosByEmpresaIdQuery(empresaId)));
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
