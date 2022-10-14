using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication4.Data;
using WebApplication4.Data.Repository;
using WebApplication4.Dto;

namespace WebApplication4.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUniversityRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        public AuthenticationService(IUniversityRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<LoginResponseDTO> LoginAsync(LoginDTO model)
        {
            var users = await _userRepository.GetByFilterAsync(n => n.Username == model.Username);
            var user = users.FirstOrDefault();

            var response = new LoginResponseDTO() {
                User = new UserDTO()
                {
                    Username = model.Username
                }
            };

            if (user == null)
            {
                response.Token = "";
                return response;
            }
            if(user.Password == model.Password)
            {
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("APISettings:JWTSecret"));
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.Now.AddHours(8),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                response.Token = tokenHandler.WriteToken(token);
            }
            response.User.Fullname = user.Fullname;
            response.User.Role = user.Role;
            return response;
        }

        public async Task<LoginResponseDTO> RegisterAsync(RegisterDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
