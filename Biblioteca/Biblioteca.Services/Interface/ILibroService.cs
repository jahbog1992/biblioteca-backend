using Biblioteca.DTO.Request;
using Biblioteca.DTO.Response;

namespace Biblioteca.Services.Interface
{
    public interface ILibroService
    {
        Task<BaseResponseGeneric<ICollection<LibroResponseDTO>>> GetAsync();
        Task<BaseResponseGeneric<LibroResponseDTO>> GetAsync(int id);
        Task<BaseResponseGeneric<int>> AddAsync(LibroRequestDTO request);
        Task<BaseResponse> UpdateAsync(int id, LibroRequestDTO request);
        Task<BaseResponse> DeleteAsync(int id);
    }
}
