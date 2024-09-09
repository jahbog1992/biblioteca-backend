using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Persistence
{
    public class BibliotecaUserIdentity : IdentityUser
    {
        [StringLength(100)]
        public string NombreCompleto { get; set; } = default!; 
    }
     
}