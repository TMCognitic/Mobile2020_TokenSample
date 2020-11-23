using Microsoft.AspNetCore.Mvc;
using Mobile2020_TokenSample.Infrastructure.Security;
using Mobile2020_TokenSample.Models.Entities;
using Mobile2020_TokenSample.Models.Forms;
using Mobile2020_TokenSample.Models.Interfaces;
using Mobile2020_TokenSample.Models.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mobile2020_TokenSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        // GET: api/<AuthController>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterForm form)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { LastName = form.LastName, FirstName = form.LastName, Email = form.Email, Passwd = form.Passwd };
                _authService.Register(user.ToDbUser());
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginForm form)
        {
            if (ModelState.IsValid)
            {
                User user = _authService.Login(form.Email, form.Passwd)?.ToApiUser();

                if (user is null)
                    return NotFound();

                //Calculer le Token                
                user.Token = _tokenService.GenerateToken(user);

                return Ok(user);
            }

            return BadRequest();
        }
    }
}
