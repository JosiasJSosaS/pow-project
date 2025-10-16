using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using pow_project.Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace pow_project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName = model.userName,     // usa Identity
                Email = model.email,           // usa Identity
                DisplayName = model.userName,  // extra
                ListCount = 0
            };

            var result = await _userManager.CreateAsync(user, model.password!);
            return result.Succeeded ? Ok("User registered successfully") : BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.email!);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.password!))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = GenerateToken(authClaims);
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized("Invalid username or password");
        }

        private JwtSecurityToken GenerateToken(IEnumerable<Claim> claims)
        {
            var secret = _configuration["Jwt:Secret"]
                ?? throw new InvalidOperationException("Jwt:Secret no configurado.");

            var issuer = _configuration["Jwt:ValidIssuer"];
            var audience = _configuration["Jwt:ValidAudience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.UtcNow.AddHours(3),
                claims: claims,
                signingCredentials: creds
            );
        }
    }
}
