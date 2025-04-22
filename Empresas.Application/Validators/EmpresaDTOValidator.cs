using Common.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Validators
{
    internal sealed class EmpresaDTOValidator : AbstractValidator<EmpresaDTO>
    {
        public EmpresaDTOValidator()
        {
            RuleFor(EmpresaDTOValidator => EmpresaDTOValidator.Nome)
                .NotEmpty()
                .WithMessage("Nome não pode ser vazio.")
                .MaximumLength(150)
                .WithMessage("Nome não pode ter mais de 150 caracteres.");

            RuleFor(EmpresaDTOValidator => EmpresaDTOValidator.CNPJ)
                .NotEmpty()
                .WithMessage("CNPJ não pode ser vazio.")
                .MaximumLength(14)
                .WithMessage("CNPJ não pode ter mais de 14 caracteres.")
                .MinimumLength(14)
                .WithMessage("CNPJ não pode ter menos de 14 caracteres.");

            RuleFor(EmpresaDTOValidator => EmpresaDTOValidator.Email)
                .NotEmpty().WithMessage("O Email é Obrigatório")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(EmpresaDTOValidator => EmpresaDTOValidator.Telefone)
                .Length(10, 11)
                .WithMessage("Telefone deve ter entre 10 e 11 caracteres.")
                .Matches(@"^\d+$")
                .WithMessage("Telefone deve conter apenas números.");

        }
    }
}
