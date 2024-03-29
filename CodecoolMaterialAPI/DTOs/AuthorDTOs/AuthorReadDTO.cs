﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DTOs.AuthorDTOs
{
    public class AuthorReadDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int AmountOfCreatedMaterials { get { return CreatedMaterials.Count; } }

        public ICollection<EduMaterialNavPointInAuthorReadDTO> CreatedMaterials { get; set; }

        
    }
}
