using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodecoolMaterialAPI.DAL.Models
{
    public class Review : EntityObject
    {
        [Required, Range(0, 10)]
        public int Rate { get; set; }
        [Required, MaxLength(200)]
        public string Comment { get; set; }

        [Required]
        public int EduMaterialNavPointID { get; set; }
        public EduMaterialNavPoint EduMaterialNavPoint { get; set; }
    }
}
