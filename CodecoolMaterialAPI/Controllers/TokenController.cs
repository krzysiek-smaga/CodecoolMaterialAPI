using CodecoolMaterialAPI.DAL.Data;
using CodecoolMaterialAPI.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodecoolMaterialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenController(ApplicationDbContext db, UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _db = db;
            _userManager = userManager;
            _config = config;
        }

        /// <summary>
        /// POST method creates JWT token for user
        /// </summary>
        [HttpPost("/login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (await IsValidUsernameAndPassword(username, password))
            {

                return Ok(new ObjectResult(await GenerateToken(username)));
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// POST method creates JWT token for user
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpPost("/register-admin")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new IdentityUser();
            user.UserName = model.Username;
            user.Email = model.Email;
            user.EmailConfirmed = true;

            IdentityResult result = _userManager.CreateAsync(user, model.Password).Result;

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "admin").Wait();
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        private async Task<bool> IsValidUsernameAndPassword(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            return await _userManager.CheckPasswordAsync(user, password);
        }

        private async Task<dynamic> GenerateToken(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var roles = from ur in _db.UserRoles
                        join r in _db.Roles on ur.RoleId equals r.Id
                        where ur.UserId == user.Id
                        select new { ur.UserId, ur.RoleId, r.Name };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(1)).ToUnixTimeSeconds().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretKey"])),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = username
            };

            return output;
        }
    }
}
