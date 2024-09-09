using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Biblioteca.DTO.Request;
using Biblioteca.Entities;
using Biblioteca.Entities.Info;
using Biblioteca.Persistence;
using Biblioteca.Repositories.Utils;
using System.Data;
using System.Linq.Expressions;

namespace Biblioteca.Repositories
{
    public class PedidoDetalleRepository : RepositoryBase<PedidoDetalle>, IPedidoDetalleRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public PedidoDetalleRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
          
        public override async Task<int> AddAsync(PedidoDetalle entity)
        {  
            await context.AddAsync(entity);
            return entity.Id;
        }

        public override async Task UpdateAsync()
        {
            await context.Database.CommitTransactionAsync();
            await base.UpdateAsync();
        }

        public override async Task<PedidoDetalle?> GetAsync(int id)
        {
            return await context.Set<PedidoDetalle>()
                .Include(x => x.Libro)  
                .Where(x => x.PedidoId == id)
                .AsNoTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<PedidoDetalle>> GetAsync<TKey>(Expression<Func<PedidoDetalle, bool>> predicate, Expression<Func<PedidoDetalle, TKey>> orderBy,
            PaginationDTO pagination)
        {
            var queryable = context.Set<PedidoDetalle>()
                .Include(x => x.Libro) 
                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();

            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();
            return response;
        }
          
    }
}
