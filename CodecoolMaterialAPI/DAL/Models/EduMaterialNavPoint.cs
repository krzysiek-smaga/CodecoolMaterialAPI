using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodecoolMaterialAPI.DAL.Models
{
    public class EduMaterialNavPoint : EntityObject
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
        public int EduMaterialTypeID { get; set; }
        public EduMaterialType EduMaterialType { get; set; }

        [Required]
        public int AuthorID { get; set; }
        public Author Author { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
