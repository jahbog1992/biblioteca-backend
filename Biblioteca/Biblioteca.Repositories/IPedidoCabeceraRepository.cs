using Biblioteca.DTO.Request;
using Biblioteca.Entities;
using Biblioteca.Entities.Info;
using System.Linq.Expressions;

namespace Biblioteca.Repositories
{
    public interface IPedidoCabeceraRepository : IRepositoryBase<PedidoCabecera>
    {
        Task CreateTransactionAsync();
        Task RollBackAsync();
        Task<ICollection<PedidoCabecera>> GetAsync<TKey>(Expression<Func<PedidoCabecera, bool>> predicate, Expression<Func<PedidoCabecera, TKey>> orderBy,
            PaginationDTO pagination);
        Task<ICollection<ReportInfo>> GetPedidoReportAsync(DateTime dateStart, DateTime dateEnd);
    }
}
