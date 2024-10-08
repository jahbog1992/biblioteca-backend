﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.DTO.Request;
using Biblioteca.Entities;
using Biblioteca.Services.Interface;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/libros")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.RoleAdmin)]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroService service; 

        public LibrosController(ILibroService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await service.GetAsync();
            return Ok(response);
        }

        [HttpGet("{id:int}")] 
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(LibroRequestDTO request)
        {
            var response = await service.AddAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, LibroRequestDTO request)
        {
            var response = await service.UpdateAsync(id, request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await service.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        } 
    }
}
