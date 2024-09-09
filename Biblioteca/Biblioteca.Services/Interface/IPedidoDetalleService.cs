using Biblioteca.DTO.Request;
using Biblioteca.DTO.Response;

namespace Biblioteca.Services.Interface
{
    public interface IPedidoDetalleService
    { 
        Task<BaseResponseGeneric<PedidoDetalleResponseDTO>> GetAsync(int id);
        Task<BaseResponseGeneric<PedidoDetalleResponseDTO>> GetAsyncByPedidoCabecera(int id);

    }
}
