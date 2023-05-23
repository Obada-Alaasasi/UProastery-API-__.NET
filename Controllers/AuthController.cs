using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UProastery.Contracts;
using UProastery.Data.User;

namespace UProastery.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager auth_manager)
        {
            this._authManager = auth_manager;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register(ApiUserDTO user_dto) {

            var errors = await _authManager.Register(user_dto);
            if (errors.Any()) {
                foreach (IdentityError error in errors) {
                    ModelState.AddModelError(error.Code, error.Description); 
                }
                return BadRequest(errors);
            }
            else return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDTO user_dto) {

            var response = await _authManager.Login(user_dto);
            if (response == null) {
                return BadRequest();
            }
            else return Ok(response);
        }









    }
}
