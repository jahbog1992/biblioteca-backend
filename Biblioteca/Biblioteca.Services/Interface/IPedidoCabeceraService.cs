using Biblioteca.DTO.Request;
using Biblioteca.DTO.Response;

namespace Biblioteca.Services.Interface
{
    public interface IPedidoCabeceraService
    {
        Task<BaseResponseGeneric<int>> AddAsync(PedidoCabeceraRequestDTO request);
        Task<BaseResponseGeneric<PedidoCabeceraResponseDTO>> GetAsync(int id);
        Task<BaseResponseGeneric<ICollection<PedidoCabeceraResponseDTO>>> GetAsync(PedidoPorFechaBuscarDTO search, PaginationDTO pagination);
        Task<BaseResponseGeneric<ICollection<PedidoCabeceraResponseDTO>>> GetAsync(string nombreCliente, PaginationDTO pagination);
        Task<BaseResponseGeneric<ICollection<PedidoReporteResponseDTO>>> GetPedidoCabeceraReportAsync(DateTime dateStart, DateTime dateEnd);

    }
}
