using Empresas.Application.Command.Add;
using Empresas.Application.Command.Remove;
using Empresas.Application.Command.Update;
using Empresas.Application.Query.GetAll;
using Empresas.Application.Query.GetByCNPJ;
using Empresas.Application.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modular_Monolith_API.ViewModels;
using SharedKernel.Errors;

namespace Modular_Monolith_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;
        [HttpPost("Add", Name = "AddEmpresa")]
        public async Task<IActionResult> AddEmpresa(CreateEmpresaViewModel empresa)
        {
            var result = (await _mediator.Send(new AddEmpresaCommand(empresa.Nome, empresa.CNPJ, empresa.Endereco, empresa.Telefone, empresa.Email, empresa.Site, empresa.Descricao)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                if (result != null && result.Error == EmpresaErrors.CnpjNotUnique)
                {
                    return Conflict(result.Error);
                }
                else
                {
                    return BadRequest(result?.Error);
                }
            }
        }

        [HttpPatch("Update",Name = "UpdateEmpresa")]
        public async Task<IActionResult> UpdateEmpresa(UpdateEmpresaViewModel empresa)
        {
            var result = (await _mediator.Send(new UpdateEmpresaCommand(empresa.Id, empresa.Nome, empresa.Endereco, empresa.Telefone, empresa.Email, empresa.Site, empresa.Descricao)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                if (result != null && result.Error == EmpresaErrors.NotFound(empresa.Id))
                {
                    return NotFound(result.Error);
                }
                else
                {
                    return BadRequest(result?.Error);
                }
            }
        }

        [HttpDelete("Delete", Name = "DeleteEmpresa")]
        public async Task<IActionResult> DeleteEmpresa(Guid Id)
        {
            var result = (await _mediator.Send(new RemoveEmpresaCommand(Id)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                if (result != null && result.Error == EmpresaErrors.NotFound(Id))
                {
                    return NotFound(result.Error);
                }
                else
                {
                    return BadRequest(result?.Error);
                }
            }
        }


        [HttpGet("GetAll", Name = "GetAllEmpresas")]
        public async Task<IActionResult> GetAllEmpresasEmpresa()
        {
            var result = (await _mediator.Send(new GetAllEmpresasQuery()));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                if (result != null && result.Error == EmpresaErrors.SemEmpresas)
                {
                    return NotFound(result.Error);
                }
                else
                {
                    return BadRequest(result?.Error);
                }
            }
        }

        [HttpGet("GetById", Name = "GetEmpresaById")]
        public async Task<IActionResult> GetEmpresaById(Guid Id)
        {
            var result = (await _mediator.Send(new GetEmpresaByIdQuery(Id)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                if (result != null && result.Error == EmpresaErrors.NotFound(Id))
                {
                    return NotFound(result.Error);
                }
                else
                {
                    return BadRequest(result?.Error);
                }
            }
        }

        [HttpGet("GetByCNPJ", Name = "GetEmpresaByCNPJ")]
        public async Task<IActionResult> GetEmpresaByCNPJ(string cnpj)
        {
            var result = (await _mediator.Send(new GetEmpresaByCNPJQuery(cnpj)));
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                if (result != null && result.Error == EmpresaErrors.NotFound(cnpj))
                {
                    return NotFound(result.Error);
                }
                else
                {
                    return BadRequest(result?.Error);
                }
            }
        }
    }
}
