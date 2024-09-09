using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.DTO.Request;
using Biblioteca.Services.Interface;
using System.Security.Claims;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }
         
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            var response = await service.LoginAsync(request);
            return response.Success ? Ok(response) : Unauthorized(response);
        } 
    }
}
