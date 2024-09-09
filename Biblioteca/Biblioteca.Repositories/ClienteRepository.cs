using Microsoft.EntityFrameworkCore;
using Biblioteca.Entities;
using Biblioteca.Persistence;

namespace Biblioteca.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
