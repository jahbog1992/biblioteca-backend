using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entities
{
    public class PedidoDetalle : EntityBase
    {
        public int PedidoId { get; set; }
        public virtual PedidoCabecera Pedido { get; set; } = default!;
        public int LibroId { get; set; }  
        public virtual Libro Libro { get; set; } = default!;
    }
}
