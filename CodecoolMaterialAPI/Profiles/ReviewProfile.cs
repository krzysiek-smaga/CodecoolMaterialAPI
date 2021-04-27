using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodecoolMaterialAPI.DAL.Models;
using CodecoolMaterialAPI.DTOs.ReviewDTOs;

namespace CodecoolMaterialAPI.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            // Source -> Target

            CreateMap<Review, ReviewReadDTO>()
                .ForMember(dest => dest.MaterialTitle, opt => opt.MapFrom(src => src.EduMaterialNavPoint.Title))
                .ForMember(dest => dest.MaterialAuthor, opt => opt.MapFrom(src => src.EduMaterialNavPoint.Author.Name));

            CreateMap<ReviewCreateDTO, Review>();

            CreateMap<ReviewUpdateDTO, Review>();
        }
    }
}
