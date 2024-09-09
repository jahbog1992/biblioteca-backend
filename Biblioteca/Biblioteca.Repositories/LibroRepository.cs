using Microsoft.EntityFrameworkCore;
using Biblioteca.Entities;
using Biblioteca.Persistence;

namespace Biblioteca.Repositories
{
    public class LibroRepository : RepositoryBase<Libro>, ILibroRepository
    {
        public LibroRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
