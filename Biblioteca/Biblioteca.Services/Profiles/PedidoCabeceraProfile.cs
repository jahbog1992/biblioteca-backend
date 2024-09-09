using AutoMapper;
using Biblioteca.DTO.Request;
using Biblioteca.DTO.Response;
using Biblioteca.Entities;
using Biblioteca.Entities.Info;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Biblioteca.Services.Profiles
{
    public class PedidoCabeceraProfile : Profile
    {
        private static readonly CultureInfo culture = new ("es-PE");//ddmmaaa hh:mm
        public PedidoCabeceraProfile()
        {
            CreateMap<PedidoCabeceraRequestDTO, PedidoCabecera>();
            CreateMap<ReportInfo, PedidoReporteResponseDTO>();
            CreateMap<PedidoCabecera, PedidoCabeceraResponseDTO>()
                .ForMember(d => d.Id, o => o.MapFrom(x => x.Id)) 
                .ForMember(d => d.ClienteId, o => o.MapFrom(x => x.Cliente)) 
                .ForMember(d => d.FechaPedido, o => o.MapFrom(x => x.FechaPedido.ToString("dd/MM/yyyy HH:mm", culture)));
        }
    }
}
