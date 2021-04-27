using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DTOs.AuthorDTOs
{
    public class EduMaterialNavPointInAuthorReadDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        public string EduMaterialTypeName { get; set; }
    }
}
