using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DTOs.AuthorDTOs
{
    public class AuthorUpdateDTO
    {
        [Required, MaxLength(50), MinLength(2)]
        public string Name { get; set; }
        [Required, MaxLength(200), MinLength(2)]
        public string Description { get; set; }
    }
}
