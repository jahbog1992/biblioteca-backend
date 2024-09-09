using Biblioteca.DTO.Request;
using Biblioteca.DTO.Response;

namespace Biblioteca.Services.Interface
{
    public interface IClienteService
    {
        Task<BaseResponseGeneric<ICollection<ClienteResponseDTO>>> GetAsync();
        Task<BaseResponseGeneric<ClienteResponseDTO>> GetAsync(int id);
        Task<BaseResponseGeneric<int>> AddAsync(ClienteRequestDTO request);
        Task<BaseResponse> UpdateAsync(int id, ClienteRequestDTO request);
        Task<BaseResponse> DeleteAsync(int id);
    }
}
