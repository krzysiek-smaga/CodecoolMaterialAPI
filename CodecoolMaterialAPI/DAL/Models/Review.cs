using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Models
{
    public class Review : EntityObject
    {
        public int Rate { get; set; }
        public string Comment { get; set; }

        public int EduMaterialNavPointID { get; set; }
        public EduMaterialNavPoint EduMaterialNavPoint { get; set; }
    }
}
