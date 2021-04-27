using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Models
{
    public class EduMaterialType : EntityObject
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(200)]
        public string Definition { get; set; }

        public ICollection<EduMaterialNavPoint> EduMaterialNavPoints { get; set; }
    }
}
