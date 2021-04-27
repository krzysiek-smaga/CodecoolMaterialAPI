using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Models
{
    public class EduMaterialType : EntityObject
    {
        public string Name { get; set; }
        public string Definition { get; set; }

        public ICollection<EduMaterialNavPoint> EduMaterialNavPoints { get; set; }
    }
}
