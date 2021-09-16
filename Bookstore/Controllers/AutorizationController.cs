using Bookstore.Security.Credentials;
using Bookstore.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
       
        public AutorizationController(UserManager<IdentityUser> userManager,
                                      SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
         
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserEntity userEntity)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            }

            var user = new IdentityUser
            {
                UserName = userEntity.Email,
                Email = userEntity.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userEntity.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, false);
            return Ok(GenerateToken(userEntity));

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserEntity userEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            var result = await _signInManager.PasswordSignInAsync(userEntity.Email, userEntity.Password,
                          isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(GenerateToken(userEntity));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Failed....");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenerateToken(UserEntity userEntity)
        {
            var claims = new[]
            {
             new Claim(JwtRegisteredClaimNames.UniqueName, userEntity.Email),
             new Claim("Admim","Saturno"),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
         };

            var key = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(Credentials.Key));

            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracao = Credentials.ExpireHours;
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: Credentials.Issuer,
               audience: Credentials.Audience,
               claims: claims,
               expires: expiration,
               signingCredentials: credenciais);

            return new UserToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT Generated with Success!!!"

            };
        }

    }
}
