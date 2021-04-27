using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodecoolMaterialAPI.DAL.Models
{
    public class Author : EntityObject
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(200)]
        public string Description { get; set; }

        public ICollection<EduMaterialNavPoint> EduMaterialNavPoints { get; set; }

        public int AmountOfCreatedMaterials { get { return EduMaterialNavPoints.Count; } }
    }
}
