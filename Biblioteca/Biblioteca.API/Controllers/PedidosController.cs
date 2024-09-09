using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.DTO.Request;
using Biblioteca.Services.Interface;
using System.Security.Claims;
using Biblioteca.Entities;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.RoleAdmin)]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoCabeceraService service;

        public PedidosController(IPedidoCabeceraService service)
        {
            this.service = service;
        }

        [HttpGet("ListPedidosByDate")]
        public async Task<IActionResult> GetByDate([FromQuery] PedidoPorFechaBuscarDTO search, 
            [FromQuery] PaginationDTO pagination)
        {
            var response = await service.GetAsync(search, pagination);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ListPedidos")]
        public async Task<IActionResult> Get([FromQuery]string? title, [FromQuery]PaginationDTO pagination)
        {
            var response = await service.GetAsync( title, pagination);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult>Get(int id)
        {
            var response = await service.GetAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost] 
        public async Task<IActionResult> Post(PedidoCabeceraRequestDTO request)
        { 
            var response = await service.AddAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
