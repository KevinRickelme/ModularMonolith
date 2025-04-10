using AutoMapper;
using Common.Application.DTOs;
using Empresas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresas.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Empresa, EmpresaDTO>().ReverseMap();
        }
    }
}
