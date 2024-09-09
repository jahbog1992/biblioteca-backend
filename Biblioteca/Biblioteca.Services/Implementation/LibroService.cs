using AutoMapper;
using Microsoft.Extensions.Logging;
using Biblioteca.DTO.Request;
using Biblioteca.DTO.Response;
using Biblioteca.Entities;
using Biblioteca.Repositories;
using Biblioteca.Services.Interface;

namespace Biblioteca.Services.Implementation
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository repository;
        private readonly ILogger<LibroService> logger;
        private readonly IMapper mapper;

        public LibroService(ILibroRepository repository, ILogger<LibroService> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<BaseResponseGeneric<ICollection<LibroResponseDTO>>> GetAsync()
        {
            var response = new BaseResponseGeneric<ICollection<LibroResponseDTO>>();
            try
            {
                response.Data = mapper.Map<ICollection<LibroResponseDTO>>(await repository.GetAsync());
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la información";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        } 

        public async Task<BaseResponseGeneric<LibroResponseDTO>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<LibroResponseDTO>();
            try
            {
                response.Data = mapper.Map<LibroResponseDTO>(await repository.GetAsync(id));
                response.Success = response.Data != null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la información";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponseGeneric<int>> AddAsync(LibroRequestDTO request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                response.Data = await repository.AddAsync(mapper.Map<Libro>(request));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la información";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse> UpdateAsync(int id, LibroRequestDTO request)
        {
            var response = new BaseResponse();
            try
            {
                var data = await repository.GetAsync(id);
                if (data is null)
                {
                    response.ErrorMessage = $"No existe el genero con id {id}.";
                    return response;
                }

                mapper.Map(request, data);
                await repository.UpdateAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la información";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var data = await repository.GetAsync(id);
                if (data is null)
                {
                    response.ErrorMessage = $"No existe el genero con id {id}.";
                    return response;
                }
                                
                await repository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la información";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}
