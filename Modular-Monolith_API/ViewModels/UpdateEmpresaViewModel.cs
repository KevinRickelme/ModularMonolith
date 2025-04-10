namespace Modular_Monolith_API.ViewModels
{
    public sealed record UpdateEmpresaViewModel(
        Guid Id,
        string Nome,
        string Endereco,
        string Telefone,
        string Email,
        string Site,
        string Descricao);
}
