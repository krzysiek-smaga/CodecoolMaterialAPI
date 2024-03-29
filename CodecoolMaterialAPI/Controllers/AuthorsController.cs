﻿using AutoMapper;
using CodecoolMaterialAPI.DAL.Interfaces;
using CodecoolMaterialAPI.DAL.Models;
using CodecoolMaterialAPI.DTOs.AuthorDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
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
        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public async Task<ActionResult<ICollection<AuthorReadDTO>>> GetAllAuthors()
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("GET api/authors - Problem with Database");
                return StatusCode(500, "Internal Server Error. Cannot connect wiht Database!");
            }
        }

        //GET api/authors/{id}
        /// <summary>
        /// GET method returns one author by id
        /// </summary>
        [Authorize(Roles = "admin,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadDTO>> GetAuthorById(int id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("GET api/authors - Problem with Database");
                return StatusCode(500, "Internal Server Error. Cannot connect wiht Database!");
            }
        }

        //POST api/author
        /// <summary>
        /// POST method creates new author
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<AuthorReadDTO>> CreateAuthor(AuthorCreateDTO authorCreateDTO)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("GET api/authors - Problem with Database");
                return StatusCode(500, "Internal Server Error. Cannot connect wiht Database!");
            }
        }

        //PUT api/authors/{id}
        /// <summary>
        /// PUT method updates author
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthorById(int id, AuthorUpdateDTO authorUpdateDTO)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("GET api/authors - Problem with Database");
                return StatusCode(500, "Internal Server Error. Cannot connect wiht Database!");
            }
        }

        //PATCH api/authors/{id}
        /// <summary>
        /// PATCH method partially updates author
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateAuthorById(int id, JsonPatchDocument<AuthorUpdateDTO> patchDocument)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("GET api/authors - Problem with Database");
                return StatusCode(500, "Internal Server Error. Cannot connect wiht Database!");
            }
        }

        //DELETE api/authors/{id}
        /// <summary>
        /// DELETE method deletes author
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthorById(int id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("GET api/authors - Problem with Database");
                return StatusCode(500, "Internal Server Error. Cannot connect wiht Database!");
            }
        }
    }
}
