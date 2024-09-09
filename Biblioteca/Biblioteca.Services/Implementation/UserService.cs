using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Biblioteca.DTO.Request;
using Biblioteca.DTO.Response;
using Biblioteca.Entities;
using Biblioteca.Persistence;
using Biblioteca.Repositories;
using Biblioteca.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;

namespace Biblioteca.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<BibliotecaUserIdentity> userManager;
        private readonly ILogger<UserService> logger;
        private readonly IOptions<AppSettings> options; 
        private readonly SignInManager<BibliotecaUserIdentity> signInManager; 

        public UserService(UserManager<BibliotecaUserIdentity> userManager, ILogger<UserService> logger,
            IOptions<AppSettings> options,  
            SignInManager<BibliotecaUserIdentity> signInManager)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.options = options; 
            this.signInManager = signInManager; 
        }
         
        public async Task<BaseResponseGeneric<LoginResponseDTO>> LoginAsync(LoginRequestDTO request)
        {
            var response = new BaseResponseGeneric<LoginResponseDTO>();
            try
            {
                var resultado = await signInManager.PasswordSignInAsync(request.Username, request.Password, isPersistent: false, lockoutOnFailure: false);
                if (resultado.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(request.Username);
                    response.Success = true;
                    response.Data = await ConstruirToken(user!);
                }
                else
                {
                    response.Success = false;
                    response.ErrorMessage = "Credenciales incorrectas.";
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        private async Task<LoginResponseDTO> ConstruirToken(BibliotecaUserIdentity user)
        {
            //creamos los claims, que son informaciones emitidas por una fuente confiable, pueden contener cualquier key/value que definamos y que son añadidas al TOKEN
            var claims = new List<Claim>()
           {
               new (ClaimTypes.NameIdentifier,user.UserName), //Nunca enviar data sensible en un claim, ya que es leído por el cliente
               new (ClaimTypes.Name,$"{user.NombreCompleto}")
           };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //firmando el JWT
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Jwt.JWTKey)); //nos valemos del proveedor de configuracion appsettings.Development.json para guardar una llaveJWT
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddSeconds(options.Value.Jwt.LifetimeInSeconds);//se puede configurar cualquier espacio de tiempo de validez de un token según las reglas de negocio

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, signingCredentials: credenciales, expires: expiracion);
            return new LoginResponseDTO(new JwtSecurityTokenHandler().WriteToken(securityToken), expiracion); 
        } 
    }
}
