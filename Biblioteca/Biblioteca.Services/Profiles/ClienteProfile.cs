using AutoMapper;
using Biblioteca.DTO.Request;
using Biblioteca.DTO.Response;
using Biblioteca.Entities;

namespace Biblioteca.Services.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteResponseDTO>();
            CreateMap<ClienteRequestDTO, Cliente>();
        }
    }
}
