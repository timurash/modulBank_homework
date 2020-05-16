using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace modulBank_hw.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly AuthOptions _authOptions;
        private readonly IUserRepository repo;
        
        public UserController(IUserService service, IOptions<AuthOptions> authOptionsAccessor, IUserRepository r)
        {
            _service = service;
            _authOptions = authOptionsAccessor.Value;
            repo = r;
        }

        [HttpPost("api/token")]
        public IActionResult Get([FromBody] UserCredentials user)
        {
            if (_service.IsValidUser(user.Username, user.Password))
            {
                var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    _authOptions.Issuer,
                    _authOptions.Audience,
                    expires: DateTime.Now.AddMinutes(_authOptions.ExpiresInMinutes),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecureKey)),
                        SecurityAlgorithms.HmacSha256Signature)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }

        [HttpPost("api/register")]
        public IActionResult Register([FromBody] UserCredentials user)
        {
            repo.Register(new User(user.Username, user.Password));
            return Ok();
        }

        [Authorize]
        [HttpGet("api/get/{id}")]
        public IActionResult GetUser(int id)
        {
            User result = repo.GetUser(id);
            return Ok(new { result });
        }

        [Authorize]
        [HttpGet("api/getUsers")]
        public IActionResult GetUsers()
        {
            return Ok(new { List = repo.GetUsers() });
        }

        [Authorize]
        [HttpPost("api/updateuser")]
        public IActionResult UpdateUser([FromBody] UserCredentials user)
        {
            repo.Update(new User(user.Username, user.Password) { Id = user.Id });
            return Ok();
        }

        [Authorize]
        [HttpPost("api/deleteuser")]
        public IActionResult DeleteUser([FromBody] UserCredentials user)
        {
            repo.DeleteUser(user.Id);
            return Ok();
        }
    }
} 