using AutoMapper;
using Common.Application.DTOs;
using Funcionarios.Domain.Entities;

namespace Funcionarios.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Funcionario, FuncionarioDTO>().ReverseMap();
        }
    }
}
