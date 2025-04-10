namespace Modular_Monolith_API.ViewModels
{
    public sealed record CreateEmpresaViewModel(
            string Nome,
            string CNPJ,
            string Endereco,
            string Telefone,
            string Email,
            string Site,
            string Descricao
        );
}
