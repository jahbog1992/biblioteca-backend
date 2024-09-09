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
    public class PedidoCabeceraRepository : RepositoryBase<PedidoCabecera>, IPedidoCabeceraRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public PedidoCabeceraRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateTransactionAsync()
        {
            await context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        }

        public override async Task<int> AddAsync(PedidoCabecera entity)
        {
            entity.FechaPedido = DateTime.Now; 
            var nextNumber = await context.Set<PedidoCabecera>().CountAsync() + 1;
            entity.NumeroOperacion = $"{nextNumber:000000}";

            await context.AddAsync(entity);
            return entity.Id;
        }

        public override async Task UpdateAsync()
        {
            await context.Database.CommitTransactionAsync();
            await base.UpdateAsync();
        }

        public override async Task<PedidoCabecera?> GetAsync(int id)
        {
            return await context.Set<PedidoCabecera>()
                .Include(x => x.Cliente)  
                .Where(x => x.Id == id)
                .AsNoTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<PedidoCabecera>> GetAsync<TKey>(Expression<Func<PedidoCabecera, bool>> predicate, Expression<Func<PedidoCabecera, TKey>> orderBy,
            PaginationDTO pagination)
        {
            var queryable = context.Set<PedidoCabecera>()
                .Include(x => x.Cliente) 
                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();

            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();
            return response;
        }

        public async Task RollBackAsync()
        {
            await context.Database.RollbackTransactionAsync();
        }

        public async Task<ICollection<ReportInfo>> GetPedidoReportAsync(DateTime dateStart, DateTime dateEnd)
        {
            //Raw query
            var query = context.Set<ReportInfo>().FromSqlRaw(
                @"select L.Nombre [NombreLibro], count(d.LibroId) [Total]
				from [PedidoCabecera].[PedidoCabecera] c
				inner join [PedidoDetalle].[PedidoDetalle] d
				on c.Id = d.PedidoId
				inner join [Libros].[Libro] l
				on d.LibroId = l.Id
                where c.FechaPedido between {0} and {1}
                group by L.Nombre",
                dateStart, dateEnd);
            return await query.ToListAsync();
        }
    }
}
