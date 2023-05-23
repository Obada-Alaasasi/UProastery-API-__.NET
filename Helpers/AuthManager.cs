using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UProastery.Contracts;
using UProastery.Data.User;

namespace UProastery.Helpers {
    public class AuthManager: IAuthManager {

        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        
        public AuthManager(IMapper mapper, UserManager<ApiUser> user_manager, IConfiguration configuration) {
            this._mapper = mapper;
            this._userManager = user_manager;
            this._configuration = configuration;
        }
        
        // helper method for register endpoint
        public async Task<IEnumerable<IdentityError>> Register(ApiUserDTO user_dto) {

            // Assign default values then map to ApiUser and set username
            user_dto.DateEnrolled = DateOnly.FromDateTime(DateTime.Now);
            user_dto.Points = 0;
            
            var user = _mapper.Map<ApiUser>(user_dto);
            user.UserName = user.Email;

            var result = await _userManager.CreateAsync(user, user_dto.Password);
            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, "Customer");
                await _userManager.AddClaimsAsync(user, new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                });
            }
            return result.Errors;
        }

        // helper method for login endpoint
        public async Task<AuthResponse> Login(LoginDTO user_dto) {

            // Fetch user from database
            var user = await _userManager.FindByEmailAsync(user_dto.Email);
            if (user == null) {
                return null;
            }
            //Check password
            var isValid = await _userManager.CheckPasswordAsync(user, user_dto.Password);
            if(isValid == false) {
                return null;
            }

            return new AuthResponse {
                UserId = user._Id,
                Token = await GenerateToken(user)
            };
        }

        // generate a JWT token
        public async Task<string> GenerateToken(ApiUser user) {
            
            // create security Key and token credentials obj
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // get all claims => token payload
            var roles = await _userManager.GetRolesAsync(user);
            var rolesClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("UID", user._Id.ToString())
            }.Union(rolesClaims).Union(userClaims);

            //create security token
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                claims: claims,
                signingCredentials: credentials
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
