using AutoMapper;
using CodecoolMaterialAPI.DAL.Interfaces;
using CodecoolMaterialAPI.DAL.Models;
using CodecoolMaterialAPI.DTOs.EduMaterialNavPointDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.Controllers
{
    /// <summary>
    /// Educational Material Navigation Points API Controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EduMaterialNavPointsController : ControllerBase
    {
        private readonly IUnitOfWork _db;
        private readonly ILogger<EduMaterialNavPointsController> _logger;
        private readonly IMapper _mapper;

        public EduMaterialNavPointsController(IUnitOfWork db, ILogger<EduMaterialNavPointsController> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        //GET api/edumaterialnavpoints
        /// <summary>
        /// GET method returns all educational material navigation points
        /// </summary>
        /// <returns>Collection of all educational material navigation points</returns>
        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public async Task<ActionResult<ICollection<EduMaterialNavPointReadDTO>>> GetAllEduMaterialNavPoint(
            [FromQuery] string materialTypeName, [FromQuery] string orderByPublishDate)
        {
            try
            {
                var eduMaterialNavPoints = await _db.EduMaterialNavPoints.GetAllEduMaterialNavPointsWithModels();

                if (eduMaterialNavPoints == null)
                {
                    _logger.LogError("GET api/edumaterialnavpoints - No Content");
                    return NoContent();
                }

                if (!String.IsNullOrEmpty(materialTypeName))
                {
                    var eduMaterialTypes = await _db.EduMaterialTypes.GetAll();
                    var eduMaterialType = eduMaterialTypes.FirstOrDefault(x => x.Name.ToLower() == materialTypeName.ToLower());

                    if (eduMaterialType != null)
                    {
                        eduMaterialNavPoints = eduMaterialNavPoints.Where(x => x.EduMaterialType.Name.ToLower() == materialTypeName.ToLower()).ToList();
                    }
                    else
                    {
                        _logger.LogError($"GET api/edumaterialnavpoints - Bad Request - Educational material type with name: '{materialTypeName}' doesn't exists. Cannot filter!");
                        return BadRequest($"Bad Request - Educational material type with name: '{materialTypeName}' doesn't exists. Cannot filter!");
                    }
                }

                if (!String.IsNullOrEmpty(orderByPublishDate))
                {
                    if (orderByPublishDate.ToLower() == "asc")
                    {
                        eduMaterialNavPoints = eduMaterialNavPoints.OrderBy(x => x.PublishDate).ToList();
                    }
                    else if (orderByPublishDate.ToLower() == "desc")
                    {
                        eduMaterialNavPoints = eduMaterialNavPoints.OrderByDescending(x => x.PublishDate).ToList();
                    }
                    else
                    {
                        _logger.LogError($"GET api/edumaterialnavpoints - Bad Request - Wrong string in order parameter!");
                        return BadRequest($"Bad Request - Wrong string in order parameter! Type 'asc' or 'desc'");
                    }
                }

                var eduMaterialNavPointsDTO = _mapper.Map<ICollection<EduMaterialNavPointReadDTO>>(eduMaterialNavPoints);
                _logger.LogInformation("GET api/edumaterialnavpoints - Ok");
                return Ok(eduMaterialNavPointsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("GET api/authors - Problem with Database");
                return StatusCode(500, "Internal Server Error. Cannot connect wiht Database!");
            }
        }

        //GET api/edumaterialnavpoints/{id}
        /// <summary>
        /// GET method returns one educational material navigation point by id
        /// </summary>
        [Authorize(Roles = "admin,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<EduMaterialNavPointReadDTO>> GetEduMaterialNavPointById(int id)
        {
            try
            {
                var eduMaterialNavPoint = await _db.EduMaterialNavPoints.GetEduMaterialNavPointByIdWithModels(id);
                if (eduMaterialNavPoint == null)
                {
                    _logger.LogError($"GET api/edumaterialnavpoints/{id} - Not Found");
                    return NotFound();
                }

                var eduMaterialNavPointDTO = _mapper.Map<EduMaterialNavPointReadDTO>(eduMaterialNavPoint);
                _logger.LogInformation($"GET api/edumaterialnavpoints/{id} - Ok");
                return Ok(eduMaterialNavPointDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("GET api/authors - Problem with Database");
                return StatusCode(500, "Internal Server Error. Cannot connect wiht Database!");
            }
        }

        //POST api/edumaterialnavpoints
        /// <summary>
        /// POST method creates new educational material navigation point
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<EduMaterialNavPointReadDTO>> CreateEduMaterialNavPoint(EduMaterialNavPointCreateDTO eduMaterialNavPointCreateDTO)
        {
            try
            {
                var eduMaterialNavPoint = _mapper.Map<EduMaterialNavPoint>(eduMaterialNavPointCreateDTO);

                var eduMatTypeID = eduMaterialNavPoint.EduMaterialTypeID;
                var eduMatTypeById = await _db.EduMaterialTypes.GetById(eduMatTypeID);
                if (eduMatTypeById == null)
                {
                    _logger.LogError($"POST api/edumaterialnavpoints - Bad Request - EduMaterialType with id:{eduMatTypeID} doesn't exists");
                    return BadRequest($"Error - EduMaterialType with id:{eduMatTypeID} doesn't exists");
                }

                var authorID = eduMaterialNavPoint.AuthorID;
                var authorById = await _db.Authors.GetById(authorID);
                if (authorById == null)
                {
                    _logger.LogError($"POST api/edumaterialnavpoints - Bad Request - Author with id:{authorID} doesn't exists");
                    return BadRequest($"Error - Author with id:{authorID} doesn't exists");
                }

                var existEduMaterialNavPoint = await _db.EduMaterialNavPoints.CheckIfEduMaterialNavPointExists(eduMaterialNavPoint);
                if (existEduMaterialNavPoint != null)
                {
                    _logger.LogError("POST api/edumaterialnavpoints - Bad Request - Educational material navigation point already exists");
                    return BadRequest("Error - Educational material navigation point already exists");
                }

                await _db.EduMaterialNavPoints.Create(eduMaterialNavPoint);
                await _db.Save();

                var eduMaterialNavPointDTO = _mapper.Map<EduMaterialNavPointReadDTO>(eduMaterialNavPoint);
                _logger.LogInformation($"POST api/edumaterialnavpoints - Educational material navigation point added to database");

                return CreatedAtAction(nameof(GetEduMaterialNavPointById), new { id = eduMaterialNavPointDTO.ID }, eduMaterialNavPointDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("GET api/authors - Problem with Database");
                return StatusCode(500, "Internal Server Error. Cannot connect wiht Database!");
            }
        }

        //PUT api/edumaterialnavpoints/{id}
        /// <summary>
        /// PUT method updates educational material navigation point
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEduMaterialTypeById(int id, EduMaterialNavPointUpdateDTO eduMaterialNavPointUpdateDTO)
        {
            try
            {
                var eduMaterialNavPoint = await _db.EduMaterialNavPoints.GetById(id);

                if (eduMaterialNavPoint == null)
                {
                    _logger.LogError($"PUT api/edumaterialnavpoints/{id} - Not Found");
                    return NotFound();
                }

                var eduMatTypeID = eduMaterialNavPointUpdateDTO.EduMaterialTypeID;
                var eduMatTypeById = await _db.EduMaterialTypes.GetById(eduMatTypeID);
                if (eduMatTypeById == null)
                {
                    _logger.LogError($"PUT api/edumaterialnavpoints - Bad Request - EduMaterialType with id:{eduMatTypeID} doesn't exists");
                    return BadRequest($"Error - EduMaterialType with id:{eduMatTypeID} doesn't exists");
                }

                var authorID = eduMaterialNavPointUpdateDTO.AuthorID;
                var authorById = await _db.Authors.GetById(authorID);
                if (authorById == null)
                {
                    _logger.LogError($"PUT api/edumaterialnavpoints - Bad Request - Author with id:{authorID} doesn't exists");
                    return BadRequest($"Error - Author with id:{authorID} doesn't exists");
                }

                _mapper.Map(eduMaterialNavPointUpdateDTO, eduMaterialNavPoint);
                await _db.EduMaterialNavPoints.Update(eduMaterialNavPoint);
                await _db.Save();

                _logger.LogInformation($"PUT api/edumaterialnavpoints/{id} - No Content - Educational material navigation point updated");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("GET api/authors - Problem with Database");
                return StatusCode(500, "Internal Server Error. Cannot connect wiht Database!");
            }
        }

        //DELETE api/edumaterialnavpoints/{id}
        /// <summary>
        /// DELETE method deletes educational material navigation point
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEduMaterialNavPointById(int id)
        {
            try
            {
                var eduMaterialNavPoint = await _db.EduMaterialNavPoints.GetById(id);

                if (eduMaterialNavPoint == null)
                {
                    _logger.LogError($"DELETE api/edumaterialnavpoints/{id} - Not Found");
                    return NotFound();
                }

                await _db.EduMaterialNavPoints.Delete(eduMaterialNavPoint);
                await _db.Save();

                _logger.LogInformation($"GET api/edumaterialnavpoints/{id} - No Content - Educational material navigation point deleted");
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
