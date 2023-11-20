using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CQRSExamp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration configuration;

    public AuthController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    [HttpGet("[action]")]
    public async Task<ActionResult> SignIn([FromHeader] string username, [FromHeader] string password)
    {
        if (username.Equals("admin") && password.Equals("1234"))
        {
            var expire = configuration["CQRSToken:AccessTokenExpire"];
            SecurityKey accessTokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["CQRSToken:AccessTokenKey"]));

            var accessClaims = new List<Claim>
                {
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.UniqueName,username)
                };

            var accessTokenCre = new SigningCredentials(accessTokenKey, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                issuer: configuration["CQRSToken:Issuer"],
                audience: configuration["CQRSToken:Audience"],
                claims: accessClaims,
                signingCredentials: accessTokenCre,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(expire))
                );


            return Ok(new JwtSecurityTokenHandler().WriteToken(accessToken));
        }

        return BadRequest();
    }
}
