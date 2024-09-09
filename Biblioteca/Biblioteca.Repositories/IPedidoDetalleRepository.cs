using Biblioteca.DTO.Request;
using Biblioteca.Entities;
using Biblioteca.Entities.Info;
using System.Linq.Expressions;

namespace Biblioteca.Repositories
{
    public interface IPedidoDetalleRepository : IRepositoryBase<PedidoDetalle>
    { 
        Task<ICollection<PedidoDetalle>> GetAsync<TKey>(Expression<Func<PedidoDetalle, bool>> predicate, Expression<Func<PedidoDetalle, TKey>> orderBy,
            PaginationDTO pagination); 
    }
}
