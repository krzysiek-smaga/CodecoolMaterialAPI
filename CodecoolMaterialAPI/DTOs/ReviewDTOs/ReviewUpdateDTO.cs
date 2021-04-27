using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DTOs.ReviewDTOs
{
    public class ReviewUpdateDTO
    {
        [Required, Range(0, 10)]
        public int Rate { get; set; }
        [Required, MaxLength(200), MinLength(2)]
        public string Comment { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int EduMaterialNavPointID { get; set; }
    }
}
