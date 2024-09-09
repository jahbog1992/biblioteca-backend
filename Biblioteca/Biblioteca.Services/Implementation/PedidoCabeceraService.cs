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
    public class PedidoCabeceraService : IPedidoCabeceraService
    {
        private readonly IPedidoCabeceraRepository PedidoRepository;
        private readonly ILibroRepository LibroRepository;
        private readonly IClienteRepository ClienteRepository;
        private readonly ILogger<PedidoCabeceraService> logger;
        private readonly IMapper mapper;

        public PedidoCabeceraService(IPedidoCabeceraRepository PedidoRepository, ILibroRepository LibroRepository,
            IClienteRepository ClienteRepository, ILogger<PedidoCabeceraService> logger, IMapper mapper)
        {
            this.PedidoRepository = PedidoRepository;
            this.LibroRepository = LibroRepository;
            this.ClienteRepository = ClienteRepository;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<BaseResponseGeneric<int>> AddAsync(PedidoCabeceraRequestDTO request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                await PedidoRepository.CreateTransactionAsync();
                PedidoCabecera entity = mapper.Map<PedidoCabecera>(request);
        
                var Cliente = await ClienteRepository.GetAsync(request.ClienteId);
                if(Cliente is null)
                {
                    throw new InvalidOperationException($"La cuenta no está registrada como cliente."); 
                }

                entity.ClienteId = Cliente.Id;

                foreach(var LibroReq in entity.PedidoDetalle) { 
                    var Libro = await LibroRepository.GetAsync(LibroReq.LibroId);
                    if (Libro is null)
                    {
                        throw new InvalidOperationException($"El libro con el Id no está registrada.");
                    } 
                }
                 
                   
                await PedidoRepository.AddAsync(entity);  
                await PedidoRepository.UpdateAsync(); 
                 

                response.Data = entity.Id;
                response.Success = true;
                logger.LogInformation($"Se creó correctamente la venta para {request.ClienteId}");
            }
            catch (Exception ex)
            {
                await PedidoRepository.RollBackAsync();
                response.ErrorMessage = "Error al crear la venta";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");                
            }
            return response;
        }

        public async Task<BaseResponseGeneric<PedidoCabeceraResponseDTO>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<PedidoCabeceraResponseDTO>();
            try
            {
                var Pedido = await PedidoRepository.GetAsync(id);
                response.Data = mapper.Map<PedidoCabeceraResponseDTO>(Pedido);
                response.Success = response.Data != null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener la venta";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<PedidoCabeceraResponseDTO>>> GetAsync(PedidoPorFechaBuscarDTO search, PaginationDTO pagination)
        {
            var response = new BaseResponseGeneric<ICollection<PedidoCabeceraResponseDTO>>();
            try
            {
                var dateInit = Convert.ToDateTime(search.FechaInicio);
                var dateEnd = Convert.ToDateTime(search.FechaFin);

                var data = await PedidoRepository.GetAsync(
                    predicate: s => s.FechaPedido >= dateInit && s.FechaPedido <= dateEnd,
                    orderBy: x=> x.Id,
                    pagination
                    );

                response.Data = mapper.Map<ICollection<PedidoCabeceraResponseDTO>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al buscar las ventas por fecha.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<PedidoCabeceraResponseDTO>>> GetAsync(string nombreCliente ,PaginationDTO pagination)
        {
            var response = new BaseResponseGeneric<ICollection<PedidoCabeceraResponseDTO>>();
            try
            {
                var data = await PedidoRepository.GetAsync( //
                    predicate: s=>s.Cliente.Nombres.Contains(nombreCliente ?? string.Empty)    ,
                    orderBy: x => x.Id,
                    pagination
                    );

                response.Data = mapper.Map<ICollection<PedidoCabeceraResponseDTO>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al buscar las ventas por fecha.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<PedidoReporteResponseDTO>>> GetPedidoCabeceraReportAsync(DateTime dateStart, DateTime dateEnd)
        {
            var response = new BaseResponseGeneric<ICollection<PedidoReporteResponseDTO>>();
            try
            {
                // Codigo
                var list = await PedidoRepository.GetPedidoReportAsync(dateStart, dateEnd);
                response.Data = mapper.Map<ICollection<PedidoReporteResponseDTO>>(list);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener los datos del reporte";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}
