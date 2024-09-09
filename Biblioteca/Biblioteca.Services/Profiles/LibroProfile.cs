using AutoMapper;
using Biblioteca.DTO.Request;
using Biblioteca.DTO.Response;
using Biblioteca.Entities;

namespace Biblioteca.Services.Profiles
{
    public class LibroProfile : Profile
    {
        public LibroProfile()
        {
            CreateMap<Libro, LibroResponseDTO>();
            CreateMap<LibroRequestDTO, Libro>();
        }
    }
}
