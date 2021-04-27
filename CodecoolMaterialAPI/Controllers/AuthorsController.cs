using AutoMapper;
using CodecoolMaterialAPI.DAL.Interfaces;
using CodecoolMaterialAPI.DTOs.AuthorDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.Controllers
{
    /// <summary>
    /// Authors API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _db;
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMapper _mapper;

        public AuthorsController(IUnitOfWork db, ILogger<AuthorsController> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        //GET api/authors/{id}
        /// <summary>
        /// GET method returns one author by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadDTO>> GetAuthorById(int id)
        {
            var author = await _db.Authors.GetAuthorByIdWithModels(id);
            if (author == null)
            {
                _logger.LogError($"GET api/authors/{id} - Not Found");
                return NotFound();
            }

            var authorDTO = _mapper.Map<AuthorReadDTO>(author);
            _logger.LogInformation($"GET api/authors/{id} - Ok");
            return Ok(authorDTO);
        }
    }
}
