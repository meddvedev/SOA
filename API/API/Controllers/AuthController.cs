using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

using API.Models.Auth;
using API.Models.User;
using API.DBContext;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration Configuration;
    private readonly DBContext db = new DBContext();

    [ActivatorUtilitiesConstructor]
    public AuthController(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    [HttpPost("sign-in")]
    public string SignIn([FromBody] Credentials credentials)
    {
        if (credentials.email.Length == 0 || credentials.password.Length == 0)
        {
            new HttpResponseException(400);
            return "error";
        }

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: credentials.password,
            salt: Encoding.ASCII.GetBytes(Configuration["salt"]),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8)
        );

        var user = db.user.First(p => p.email == credentials.email);

        if (user.passwordhash != hashed)
        {
            new HttpResponseException(401);
            return "error";
        }
        string res = JsonSerializer.Serialize<User>(user);
        return res;
    }

    [HttpPost("getToken")]
    public string GetToken([FromBody] Credentials credentials)
    {
        if (credentials.email.Length == 0 || credentials.password.Length == 0)
        {
            new HttpResponseException(400);
            return "error";
        }

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: credentials.password,
            salt: Encoding.ASCII.GetBytes(Configuration["salt"]),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8)
        );

        var user = db.user.First(p => p.email == credentials.email);

        if (user.passwordhash != hashed)
        {
            new HttpResponseException(401);
            return "error";
        }
        return GenerateToken(credentials);
    }

    private string GenerateToken(Credentials credentials)
    {
        var credentials = new SigningCredentials(Configuration["salt"], SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);

    }

}

