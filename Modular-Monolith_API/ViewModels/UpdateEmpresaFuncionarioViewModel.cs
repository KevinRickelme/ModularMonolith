namespace Modular_Monolith_API.ViewModels
{
    public sealed record UpdateEmpresaFuncionarioViewModel(
            Guid Id,
            DateTime DataAdmissao,
            string Cargo,
            string Departamento);
}
