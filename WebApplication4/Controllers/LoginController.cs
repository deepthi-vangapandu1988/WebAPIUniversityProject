using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Dto;
using WebApplication4.Models;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        public async Task<APIResponse> Login([FromBody] LoginDTO model)
        {
            if (model == null)
            {
                var response = new APIResponse(System.Net.HttpStatusCode.BadRequest, false);
                var errors = new List<string> { "input model can not be null" };
                response.Errors = errors;
                return response;
            }

            var result  = await _authenticationService.LoginAsync(model);
            return new APIResponse(System.Net.HttpStatusCode.OK, true, result);
        }
    }
}
