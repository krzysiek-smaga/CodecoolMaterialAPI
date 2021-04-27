using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DTOs.EduMaterialTypeDTOs
{
    public class EduMaterialTypeReadDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
        public ICollection<EduMaterialNavPointInEduMaterialTypeReadDTO> EduMaterialsOfThisType { get; set; }
    }
}
