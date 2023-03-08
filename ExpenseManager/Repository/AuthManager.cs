using AutoMapper;
using ExpenseManager.Contracts;
using ExpenseManager.Data;
using ExpenseManager.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseManager.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;
        private readonly IConfiguration configuration;
        private ApiUser _user;

        private const string _loginProvider = "ExpenseManagerApi";
        private const string _refreshToken = "RefreshToken";

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<string> CreateRefreshToken()
        {
            await userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newRefreshToken = await userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            var result = await userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
            return newRefreshToken;

        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            bool isValid = false;
            
                _user = await userManager.FindByEmailAsync(loginDto.Email);
                isValid = await userManager.CheckPasswordAsync(_user, loginDto.Password);
                if(_user == null || isValid == false)
                {
                    return null;
                }
            
            var token = await GenerateToken();

            return new AuthResponseDto
            {
                Token = token,
                UserId = _user.Id,
                RefreshToken = await CreateRefreshToken()
            };
            
        }

        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
        {
            _user = mapper.Map<ApiUser>(userDto);
            _user.UserName = userDto.Email;

            var result = await userManager.CreateAsync(_user, userDto.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(_user, "User");
            }

            return result.Errors;
                    
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent  = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await userManager.FindByNameAsync(username);

            if(_user == null || _user.Id != request.UserId){
                return null;
            }
            var isValid = await userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);

            if (isValid)
            {
                var token = await GenerateToken();
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await userManager.UpdateSecurityStampAsync(_user);

            return null;
        }

        private async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(_user);

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            
            var userClaims = await userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),

            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
