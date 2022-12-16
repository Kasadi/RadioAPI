using Microsoft.AspNetCore.Mvc;
using RadioAPI.Data;
using RadioAPI.Model;
using BCrypt.Net;
using RadioAPI.Data.Requests.User;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace RadioAPI.Controller
{
    public class UserController : ControllerBase
    {
        private readonly RadioDbContext _context;
        private readonly IConfiguration _configuration;

        public UserController(RadioDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("/Login"), Authorize]
        public async Task<ActionResult<User>> Login(string email, string password)
        {
            var user = _context.User.Where(s => s.Email == email).Single();




            if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password))
            {
                return Unauthorized("Wrong username or password");
            }

            var token = CreateToken(user);

            return user;

        }

        //REGISTRATION: api/Stations/Registration

        [HttpPost("/Registration")]
        public async Task<IActionResult> Registration(RegistrationCredentialsRequest registrationCredentials)
        {
            bool exsits = _context.User.Any(s => s.Email == registrationCredentials.Email);


            if (registrationCredentials.Password != registrationCredentials.ConfirmPassword || exsits)
            {
                return Conflict();
            }



            _context.User.Add(new User
            {
                Email = registrationCredentials.Email,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(registrationCredentials.ConfirmPassword),
                Name = registrationCredentials.Name
            });

            await _context.SaveChangesAsync();

            return NoContent();

        }


        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
