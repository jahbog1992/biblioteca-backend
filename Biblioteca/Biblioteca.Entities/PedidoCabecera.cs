using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Entities
{
    public class PedidoCabecera : EntityBase
    {
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; } = default!;
        public DateTime FechaPedido { get; set; }
        public string NumeroOperacion { get; set; } = default!;
        public virtual  List<PedidoDetalle> PedidoDetalle { get; set; } = default!;
    }
}
