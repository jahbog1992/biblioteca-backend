namespace Biblioteca.DTO.Request;

public record PedidoCabeceraRequestDTO(int ClienteId, List<PedidoDetalleRequestDTO> PedidoDetalle); 