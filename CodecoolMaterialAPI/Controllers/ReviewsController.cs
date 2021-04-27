﻿using AutoMapper;
using CodecoolMaterialAPI.DAL.Interfaces;
using CodecoolMaterialAPI.DAL.Models;
using CodecoolMaterialAPI.DTOs.ReviewDTOs;
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
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _db;
        private readonly ILogger<ReviewsController> _logger;
        private readonly IMapper _mapper;

        public ReviewsController(IUnitOfWork db, ILogger<ReviewsController> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        //GET api/reviews
        /// <summary>
        /// GET method returns all reviews
        /// </summary>
        /// <returns>Collection of all reviews</returns>
        [HttpGet]
        public async Task<ActionResult<ICollection<ReviewReadDTO>>> GetAllReviews()
        {
            var reviews = await _db.Reviews.GetAllReviewsWithModels();
            if (reviews == null)
            {
                _logger.LogError("GET api/reviews - No Content");
                return NoContent();
            }

            var reviewsDTO = _mapper.Map<ICollection<ReviewReadDTO>>(reviews);
            _logger.LogInformation("GET api/reviews - Ok");
            return Ok(reviewsDTO);
        }

        //GET api/reviews/{id}
        /// <summary>
        /// GET method returns one review by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewReadDTO>> GetReviewById(int id)
        {
            var review = await _db.Reviews.GetReviewByIdWithModels(id);
            if (review == null)
            {
                _logger.LogError($"GET api/reviews/{id} - Not Found");
                return NotFound();
            }

            var reviewDTO = _mapper.Map<ReviewReadDTO>(review);
            _logger.LogInformation($"GET api/reviews/{id} - Ok");
            return Ok(reviewDTO);
        }

        //POST api/reviews
        /// <summary>
        /// POST method creates new review
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ReviewReadDTO>> CreateReview(ReviewCreateDTO reviewCreateDTO)
        {
            var review = _mapper.Map<Review>(reviewCreateDTO);

            var eduMatID = review.EduMaterialNavPointID;
            var eduMatById = await _db.EduMaterialNavPoints.GetById(eduMatID);

            if (eduMatById == null)
            {
                _logger.LogError($"POST api/reviews - Bad Request - EduMaterialNavPoint with id:{eduMatID} doesn't exists");
                return BadRequest($"Error - EduMaterialNavPoint with id:{eduMatID} doesn't exists");
            }

            await _db.Reviews.Create(review);
            await _db.Save();

            var reviewDTO = _mapper.Map<ReviewReadDTO>(review);
            _logger.LogInformation($"POST api/reviews - Review added to database");

            return CreatedAtAction(nameof(GetReviewById), new { id = reviewDTO.ID }, reviewDTO);
        }

        //PUT api/reviews/{id}
        /// <summary>
        /// PUT method updates review
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReviewById(int id, ReviewUpdateDTO reviewUpdateDTO)
        {
            var review = await _db.Reviews.GetById(id);

            if (review == null)
            {
                _logger.LogError($"PUT api/reviews/{id} - Not Found");
                return NotFound();
            }

            var eduMatID = reviewUpdateDTO.EduMaterialNavPointID;
            var eduMatById = await _db.EduMaterialNavPoints.GetById(eduMatID);

            if (eduMatById == null)
            {
                _logger.LogError($"PUT api/reviews - Bad Request - EduMaterialNavPoint with id:{eduMatID} doesn't exists");
                return BadRequest($"Error - EduMaterialNavPoint with id:{eduMatID} doesn't exists");
            }

            _mapper.Map(reviewUpdateDTO, review);
            await _db.Reviews.Update(review);
            await _db.Save();

            _logger.LogInformation($"PUT api/reviews/{id} - No Content - Review updated");
            return NoContent();
        }

        //DELETE api/reviews/{id}
        /// <summary>
        /// DELETE method deletes review
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReviewById(int id)
        {
            var review = await _db.Reviews.GetById(id);

            if (review == null)
            {
                _logger.LogError($"DELETE api/reviews/{id} - Not Found");
                return NotFound();
            }

            await _db.Reviews.Delete(review);
            await _db.Save();

            _logger.LogInformation($"GET api/reviews/{id} - No Content - Review deleted");
            return NoContent();
        }
    }
}
