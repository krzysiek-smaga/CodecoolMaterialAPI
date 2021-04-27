using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.DTOs.ReviewDTOs
{
    public class ReviewReadDTO
    {
        public int ID { get; set; }
        public string MaterialTitle { get; set; }
        public string MaterialAuthor { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
    }
}
