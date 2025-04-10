using AutoMapper;
using Common.Application.DTOs;
using EmpresasFuncionarios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresasFuncionarios.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<EmpresaFuncionario, EmpresaFuncionarioDTO>().ReverseMap();
        }
    }
}
