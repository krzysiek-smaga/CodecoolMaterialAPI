using AutoMapper;
using CodecoolMaterialAPI.DAL.Models;
using CodecoolMaterialAPI.DTOs.EduMaterialTypeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.Profiles
{
    public class EduMaterialTypeProfile : Profile
    {
        public EduMaterialTypeProfile()
        {
            // Source -> Target

            CreateMap<Review, EduMaterialTypeReadDTO>()
                .ForMember(dest => dest.EduMaterialsOfThisType, opt => opt.MapFrom(src => src.EduMaterialNavPoint));

            CreateMap<EduMaterialNavPoint, EduMaterialNavPointInEduMaterialTypeReadDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name));

            CreateMap<EduMaterialTypeCreateDTO, EduMaterialType>();

            CreateMap<EduMaterialTypeUpdateDTO, EduMaterialType>();
        }
    }
}
