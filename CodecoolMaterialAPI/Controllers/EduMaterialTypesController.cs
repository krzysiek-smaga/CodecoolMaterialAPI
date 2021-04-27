using AutoMapper;
using CodecoolMaterialAPI.DAL.Interfaces;
using CodecoolMaterialAPI.DAL.Models;
using CodecoolMaterialAPI.DTOs.EduMaterialTypeDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.Controllers
{
    /// <summary>
    /// Educational Material Types API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EduMaterialTypesController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly ILogger<EduMaterialTypesController> _logger;
        private readonly IMapper _mapper;

        public EduMaterialTypesController(IUnitOfWork db, ILogger<EduMaterialTypesController> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        //GET api/edumaterialtypes
        /// <summary>
        /// GET method returns all educational material types
        /// </summary>
        /// <returns>Collection of all educational material types</returns>
        [HttpGet]
        public async Task<ActionResult<ICollection<EduMaterialTypeReadDTO>>> GetAllEduMaterialTypes()
        {
            var eduMaterialTypes = await _db.EduMaterialTypes.GetAllEduMaterialTypesWithModels();
            if (eduMaterialTypes == null)
            {
                _logger.LogError("GET api/edumaterialtypes - No Content");
                return NoContent();
            }

            var eduMaterialTypesDTO = _mapper.Map<ICollection<EduMaterialTypeReadDTO>>(eduMaterialTypes);
            _logger.LogInformation("GET api/edumaterialtypes - Ok");
            return Ok(eduMaterialTypesDTO);
        }

        //GET api/edumaterialtypes/{id}
        /// <summary>
        /// GET method returns one educational material type by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<EduMaterialTypeReadDTO>> GetEduMaterialTypeById(int id)
        {
            var eduMaterialType = await _db.EduMaterialTypes.GetEduMaterialTypeByIdWithModels(id);
            if (eduMaterialType == null)
            {
                _logger.LogError($"GET api/edumaterialtypes/{id} - Not Found");
                return NotFound();
            }

            var eduMaterialTypeDTO = _mapper.Map<EduMaterialTypeReadDTO>(eduMaterialType);
            _logger.LogInformation($"GET api/edumaterialtypes/{id} - Ok");
            return Ok(eduMaterialTypeDTO);
        }

        //POST api/edumaterialtypes
        /// <summary>
        /// POST method creates new educational material type
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<EduMaterialTypeReadDTO>> CreateEduMaterialType(EduMaterialTypeCreateDTO eduMaterialTypeCreateDTO)
        {
            var eduMaterialType = _mapper.Map<EduMaterialType>(eduMaterialTypeCreateDTO);

            var existEduMaterialType = await _db.EduMaterialTypes.CheckIfEduMaterialTypeExists(eduMaterialType);

            if (existEduMaterialType != null)
            {
                _logger.LogError("POST api/edumaterialtypes - Bad Request - Educational material Type already exists");
                return BadRequest("Error - Educational material Type already exists");
            }

            await _db.EduMaterialTypes.Create(eduMaterialType);
            await _db.Save();

            var eduMaterialTypeDTO = _mapper.Map<EduMaterialTypeReadDTO>(eduMaterialType);
            _logger.LogInformation($"POST api/edumaterialtypes - Educational material Type added to database");

            return CreatedAtAction(nameof(GetEduMaterialTypeById), new { id = eduMaterialTypeDTO.ID }, eduMaterialTypeDTO);
        }

        //PUT api/edumaterialtypes/{id}
        /// <summary>
        /// PUT method updates educational material type
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEduMaterialTypeById(int id, EduMaterialTypeUpdateDTO eduMaterialTypeUpdateDTO)
        {
            var eduMaterialType = await _db.EduMaterialTypes.GetById(id);

            if (eduMaterialType == null)
            {
                _logger.LogError($"PUT api/edumaterialtypes/{id} - Not Found");
                return NotFound();
            }

            _mapper.Map(eduMaterialTypeUpdateDTO, eduMaterialType);
            await _db.EduMaterialTypes.Update(eduMaterialType);
            await _db.Save();

            _logger.LogInformation($"PUT api/edumaterialtypes/{id} - No Content - Educational material Type updated");
            return NoContent();
        }

        //DELETE api/edumaterialtypes/{id}
        /// <summary>
        /// DELETE method deletes educational material type
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEduMaterialTypeById(int id)
        {
            var eduMaterialType = await _db.EduMaterialTypes.GetById(id);

            if (eduMaterialType == null)
            {
                _logger.LogError($"DELETE api/edumaterialtypes/{id} - Not Found");
                return NotFound();
            }

            await _db.EduMaterialTypes.Delete(eduMaterialType);
            await _db.Save();

            _logger.LogInformation($"GET api/edumaterialtypes/{id} - No Content - Educational material Type deleted");
            return NoContent();
        }
    }
}
