using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Models
{
    public class Author : EntityObject
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<EduMaterialNavPoint> EduMaterialNavPoints { get; set; }

        public int AmountOfCreatedMaterials { get { return EduMaterialNavPoints.Count; } }
    }
}
