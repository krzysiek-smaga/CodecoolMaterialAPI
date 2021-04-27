using AutoMapper;
using CodecoolMaterialAPI.DAL.Models;
using CodecoolMaterialAPI.DTOs.EduMaterialNavPointDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.Profiles
{
    public class EduMaterialNavPointProfile : Profile
    {
        public EduMaterialNavPointProfile()
        {
            // Source -> Target

            CreateMap<EduMaterialNavPoint, EduMaterialNavPointReadDTO>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.EduMaterialTypeName, opt => opt.MapFrom(src => src.EduMaterialType.Name));

            CreateMap<Review, ReviewInEduMaterialNavPointReadDTO>();

            CreateMap<EduMaterialNavPointCreateDTO, EduMaterialNavPoint>();

            CreateMap<EduMaterialNavPointUpdateDTO, EduMaterialNavPoint>();
        }
    }
}
