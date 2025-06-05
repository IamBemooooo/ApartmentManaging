using ApartmentManaging.Application.Interfaces;
using ApartmentManaging.Domain.Interfaces;
using ApartmentManaging.Shared.DTOs.Requests.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApartmentManaging.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _jwtSecret = configuration["Jwt:SecretKey"]
                         ?? throw new ArgumentNullException("Jwt:Secret configuration is missing");
        }

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var user = await _userRepository.Login(dto);
            if (user == null)
                return null;

            // Tạo token JWT ngay trong đây
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
                // Thêm claim khác nếu muốn
            }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
