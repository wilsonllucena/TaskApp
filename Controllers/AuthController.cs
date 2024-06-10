using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskApp.Models;

namespace TaskApp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] AuthModel authModel)
        {
            if (authModel.Username == "admin" && authModel.Password == "admin")
            {
                var token = GetTokenJWT();
                return Ok(new { token });
            }

            return BadRequest(new { mensagem = "Ivalid credentials, Please verify username or password" });
        }

        private string GetTokenJWT()
        {
            var secretKeyApi = "a7055d95-2581-4faa-b848-90815198c661";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyApi));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", "admin")
            };

            var token = new JwtSecurityToken(
                issuer: "your_business",
                audience: "your_application",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
