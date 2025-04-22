using Common.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Validators
{
    internal sealed class EmpresaFuncionarioDTOValidator : AbstractValidator<EmpresaFuncionarioDTO>
    {
        public EmpresaFuncionarioDTOValidator()
        {
            RuleFor(EmpresaFuncionarioDTOValidator => EmpresaFuncionarioDTOValidator.EmpresaId)
                .NotEmpty()
                .WithMessage("Id de Empresa não pode ser vazio.");

            RuleFor(EmpresaFuncionarioDTOValidator => EmpresaFuncionarioDTOValidator.FuncionarioId)
                .NotEmpty()
                .WithMessage("Id de Funcionário não pode ser vazio.");

            RuleFor(EmpresaFuncionarioDTOValidator => EmpresaFuncionarioDTOValidator.DataAdmissao)
                .NotEmpty()
                .WithMessage("Data de Admissão não pode ser vazia.")
                .Must(data => data != DateTime.MinValue)
                .WithMessage("Data de Admissão não pode ser a data mínima.")
                .Must(data => data <= DateTime.Now)
                .WithMessage("Data de Admissão não pode ser maior que a data atual.");

            RuleFor(EmpresaFuncionarioDTOValidator => EmpresaFuncionarioDTOValidator.Cargo)
                .NotEmpty()
                .WithMessage("Cargo não pode ser vazio.")
                .MaximumLength(100)
                .WithMessage("Cargo não pode ter mais de 100 caracteres.");

            RuleFor(EmpresaFuncionarioDTOValidator => EmpresaFuncionarioDTOValidator.Departamento)
                .NotEmpty()
                .WithMessage("Departamento não pode ser vazio.")
                .MaximumLength(100)
                .WithMessage("Departamento não pode ter mais de 100 caracteres.");
        }
    }
}
