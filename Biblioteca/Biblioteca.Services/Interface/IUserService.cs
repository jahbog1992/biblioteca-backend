using Biblioteca.DTO.Request;
using Biblioteca.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services.Interface
{
    public interface IUserService
    { 
        Task<BaseResponseGeneric<LoginResponseDTO>> LoginAsync(LoginRequestDTO request);  

    }
}
