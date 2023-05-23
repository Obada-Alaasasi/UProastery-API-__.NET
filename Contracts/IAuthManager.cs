using Microsoft.AspNetCore.Identity;
using UProastery.Data.User;

namespace UProastery.Contracts {
    
    public interface IAuthManager {

        public Task<IEnumerable<IdentityError>> Register(ApiUserDTO user_dto);
        public Task<AuthResponse> Login(LoginDTO user_dto);
        public Task<string> GenerateToken(ApiUser user);
    }
}
