using Microsoft.AspNetCore.Authorization;
using Biblioteca.Services.Interface;

namespace Biblioteca.API.Endpoints
{
    public static class ReportEndpoints
    {
        public static void MapReports(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("api/Reports")
                .WithDescription("Reportes de biblioteca")
                .WithTags("Reports");

            group.MapGet("/", [Authorize(Roles = Entities.Constants.RoleAdmin)] async (IPedidoCabeceraService service, string dateStart, string dateEnd) =>
            {
                var response = await service.GetPedidoCabeceraReportAsync(DateTime.Parse(dateStart), DateTime.Parse(dateEnd));
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            });
        }

    }
}
