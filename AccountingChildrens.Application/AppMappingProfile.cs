using System;
using AccountingChildrens.Application.DTOs;
using AccountingChildrens.Domain.Entities;
using AutoMapper;

namespace AccountingChildrens.Application
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Children, ChildrenDTO>().ReverseMap();
            CreateMap<Group, GroupDTO>().ReverseMap();
            CreateMap<Educator, EducatorDTO>().ReverseMap();
            CreateMap<ChildrenGroup, ChildrenGroupDTO>().ReverseMap();
            CreateMap<EducatorGroup, EducatorGroupDTO>().ReverseMap();
        }
    }
}

