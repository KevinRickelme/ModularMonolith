namespace Modular_Monolith_API.ViewModels
{
    public sealed record CreateEmpresaFuncionarioViewModel(
            Guid FuncionarioId,
            Guid EmpresaId,
            DateTime DataAdmissao,
            string Cargo,
            string Departamento);
}
