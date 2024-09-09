using AutoMapper;
using Biblioteca.DTO.Request;
using Biblioteca.DTO.Response;
using Biblioteca.Entities;
using Biblioteca.Entities.Info;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Biblioteca.Services.Profiles
{
    public class PedidoDetalleProfile : Profile
    {
        private static readonly CultureInfo culture = new ("es-PE");//ddmmaaa hh:mm
        public PedidoDetalleProfile()
        {
            CreateMap<PedidoDetalleRequestDTO, PedidoDetalle>(); 
            CreateMap<PedidoDetalle, PedidoDetalleResponseDTO>()
                .ForMember(d => d.Id, o => o.MapFrom(x => x.Id)) 
                .ForMember(d => d.PedidoId, o => o.MapFrom(x => x.Pedido))
                .ForMember(d => d.LibroId, o => o.MapFrom(x => x.Libro));
        }
    }
}
