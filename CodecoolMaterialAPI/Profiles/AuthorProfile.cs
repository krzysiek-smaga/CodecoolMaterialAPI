using AutoMapper;
using CodecoolMaterialAPI.DAL.Models;
using CodecoolMaterialAPI.DTOs.AuthorDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            // Source -> Target

            CreateMap<Author, AuthorReadDTO>()
                .ForMember(dest => dest.CreatedMaterials, opt => opt.MapFrom(src => src.EduMaterialNavPoints));

            CreateMap<EduMaterialNavPoint, EduMaterialNavPointInAuthorReadDTO>()
                .ForMember(dest => dest.EduMaterialTypeName, opt => opt.MapFrom(src => src.EduMaterialType.Name));

            CreateMap<AuthorCreateDTO, Author>();

            CreateMap<AuthorUpdateDTO, Author>();
        }
    }
}
