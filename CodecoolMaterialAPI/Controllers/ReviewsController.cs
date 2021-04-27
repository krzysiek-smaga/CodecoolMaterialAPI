using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        /// <summary>
        /// Test swagger
        /// </summary>
        [HttpGet]
        public Task Index()
        {
            throw new NotImplementedException();
        }
    }
}
