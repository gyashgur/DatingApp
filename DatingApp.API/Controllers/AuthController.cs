using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    
    {
        // Inject IAuthRepository  to bring in the data namespace
        private readonly iAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController   (iAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register(Dtos.UserForRegisterDto userForRegisterDto)  
        // object instead of username and password
        {

         //   if (!ModelState.IsValid)
         //       return BadRequest(ModelState);

            //validate request
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if(await _repo.UserExists(userForRegisterDto.Username))
            return BadRequest("user already exists");
          

          var userToCreate = new User {
              Username = userForRegisterDto.Username
          };

          var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

          return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {
        var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(),userForLoginDto.Password);

        if(userFromRepo == null)
               return Unauthorized(); 

        var claims = new[]  
        {
           new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),new Claim(ClaimTypes.Name, userFromRepo.Username) 
        };      

        var key = new SymmetricSecurityKey (Encoding.UTF8
                  .GetBytes(_config.GetSection("Appsettings:Token").Value));   

        var creds =  new SigningCredentials(key,
                              SecurityAlgorithms.HmacSha512Signature);    

        var tokenDescriptor = new SecurityTokenDescriptor
        {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
        };   

        var tokenHandler  = new JwtSecurityTokenHandler();

        var token =  tokenHandler.CreateToken(tokenDescriptor);    

        return Ok(new {
            token = tokenHandler.WriteToken(token)
        });                                          
    }
   }
}