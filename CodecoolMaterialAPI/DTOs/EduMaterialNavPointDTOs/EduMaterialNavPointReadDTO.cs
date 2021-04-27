using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DTOs.EduMaterialNavPointDTOs
{
    public class EduMaterialNavPointReadDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        public string EduMaterialTypeName { get; set; }
        public string AuthorName { get; set; }

        public ICollection<ReviewInEduMaterialNavPointReadDTO> Reviews { get; set; }
    }
}
