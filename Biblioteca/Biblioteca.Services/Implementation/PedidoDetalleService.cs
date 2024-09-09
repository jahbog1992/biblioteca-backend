using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Extensions.Logging;
using Biblioteca.DTO.Request;
using Biblioteca.DTO.Response;
using Biblioteca.Entities;
using Biblioteca.Repositories;
using Biblioteca.Services.Interface;

namespace Biblioteca.Services.Implementation
{
    public class PedidoDetalleService : IPedidoDetalleService
    {
        private readonly IPedidoDetalleRepository PedidoRepository;
        private readonly ILibroRepository LibroRepository; 
        private readonly ILogger<PedidoDetalleService> logger;
        private readonly IMapper mapper;

        public PedidoDetalleService(IPedidoDetalleRepository PedidoRepository, ILibroRepository LibroRepository,
            ILogger<PedidoDetalleService> logger, IMapper mapper)
        {
            this.PedidoRepository = PedidoRepository;
            this.LibroRepository = LibroRepository; 
            this.logger = logger;
            this.mapper = mapper;
        }
         

        public async Task<BaseResponseGeneric<PedidoDetalleResponseDTO>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<PedidoDetalleResponseDTO>();
            try
            {
                var Pedido = await PedidoRepository.GetAsync(id);
                response.Data = mapper.Map<PedidoDetalleResponseDTO>(Pedido);
                response.Success = response.Data != null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener la venta";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }

        public async Task<BaseResponseGeneric<PedidoDetalleResponseDTO>> GetAsyncByPedidoCabecera(int id)
        {
            var response = new BaseResponseGeneric<PedidoDetalleResponseDTO>();
            try
            {
                var Pedido = await PedidoRepository.GetAsync();
                response.Data = mapper.Map<PedidoDetalleResponseDTO>(Pedido);
                response.Success = response.Data != null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener la venta";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
    }
}
