namespace Biblioteca.DTO.Response
{
    public record PedidoCabeceraResponseDTO(int Id, string FechaPedido, string NumeroOperacion, int ClienteId, string NombreCliente, short Cantidad);
}
