using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DTOs.EduMaterialNavPointDTOs
{
    public class EduMaterialNavPointUpdateDTO
    {
        [Required, MaxLength(200), MinLength(2)]
        public string Title { get; set; }
        [Required, MaxLength(500), MinLength(2)]
        public string Description { get; set; }
        [Required, MaxLength(500), MinLength(2)]
        public string Location { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int EduMaterialTypeID { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int AuthorID { get; set; }
    }
}
