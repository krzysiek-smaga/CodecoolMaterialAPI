using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DAL.Models
{
    public class EduMaterialNavPoint : EntityObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime PublishDate { get; set; }

        public int EduMaterialTypeID { get; set; }
        public EduMaterialType EduMaterialType { get; set; }

        public int AuthorID { get; set; }
        public Author Author { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
