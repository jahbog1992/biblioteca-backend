using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entities
{
    public class AppSettings
    {
        public Jwt Jwt { get; set; } = default!;
    }

    public class Jwt
    {
        public string JWTKey { get; set; } = string.Empty;
        public int LifetimeInSeconds { get; set; }
    } 
}
