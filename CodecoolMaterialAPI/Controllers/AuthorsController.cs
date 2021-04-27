using AutoMapper;
using CodecoolMaterialAPI.DAL.Interfaces;
using CodecoolMaterialAPI.DAL.Models;
using CodecoolMaterialAPI.DTOs.AuthorDTOs;
using Microsoft.AspNetCore.JsonPatch;
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

        //GET api/authors
        /// <summary>
        /// GET method returns all authors
        /// </summary>
        /// <returns>Collection of all authors</returns>
        [HttpGet]
        public async Task<ActionResult<ICollection<AuthorReadDTO>>> GetAllAuthors()
        {
            var authors = await _db.Authors.GetAllAuthorsWithModels();
            if (authors == null)
            {
                _logger.LogError("GET api/authors - No Content");
                return NoContent();
            }

            var authorsDTO = _mapper.Map<ICollection<AuthorReadDTO>>(authors);
            _logger.LogInformation("GET api/authors - Ok");
            return Ok(authorsDTO);
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

        //POST api/songs
        /// <summary>
        /// POST method creates new author
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AuthorReadDTO>> CreateAuthor(AuthorCreateDTO authorCreateDTO)
        {
            var author = _mapper.Map<Author>(authorCreateDTO);

            var existAuthor = await _db.Authors.CheckIfAuthorExists(author);

            if (existAuthor != null)
            {
                _logger.LogError("POST api/authors - Bad Request - Author already exists");
                return BadRequest("Error - Author already exists");
            }

            await _db.Authors.Create(author);
            await _db.Save();

            var authorDTO = _mapper.Map<AuthorReadDTO>(author);
            _logger.LogInformation($"POST api/authors - Author added to database");

            return CreatedAtAction(nameof(GetAuthorById), new { id = authorDTO.ID }, authorDTO);
        }

        //PUT api/authors/{id}
        /// <summary>
        /// PUT method updates author
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthorById(int id, AuthorUpdateDTO authorUpdateDTO)
        {
            var author = await _db.Authors.GetById(id);

            if (author == null)
            {
                _logger.LogError($"PUT api/authors/{id} - Not Found");
                return NotFound();
            }

            _mapper.Map(authorUpdateDTO, author);
            await _db.Authors.Update(author);
            await _db.Save();

            _logger.LogInformation($"PUT api/authors/{id} - No Content - Author updated");
            return NoContent();
        }

        //PATCH api/authors/{id}
        /// <summary>
        /// PATCH method partially updates author
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateAuthorById(int id, JsonPatchDocument<AuthorUpdateDTO> patchDocument)
        {
            var author = await _db.Authors.GetById(id);

            if (author == null)
            {
                _logger.LogError($"PATCH api/authors/{id} - Not Found");
                return NotFound();
            }

            var authorToPatch = _mapper.Map<AuthorUpdateDTO>(author);
            patchDocument.ApplyTo(authorToPatch, ModelState);

            if (!TryValidateModel(authorToPatch))
            {
                _logger.LogError($"PATCH api/authors/{id} - Problem with validation");
                return ValidationProblem(ModelState);
            }

            _mapper.Map(authorToPatch, author);
            await _db.Authors.Update(author);
            await _db.Save();

            _logger.LogInformation($"PATCH api/authors/{id} - No Content - Author updated partially");
            return NoContent();
        }

        //DELETE api/authors/{id}
        /// <summary>
        /// DELETE method deletes author
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthorById(int id)
        {
            var author = await _db.Authors.GetById(id);

            if (author == null)
            {
                _logger.LogError($"DELETE api/authors/{id} - Not Found");
                return NotFound();
            }

            await _db.Authors.Delete(author);
            await _db.Save();

            _logger.LogInformation($"GET api/authors/{id} - No Content - Author deleted");
            return NoContent();
        }
    }
}
