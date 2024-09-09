using Biblioteca.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Persistence.Configurations
{
    public class PedidoCabeceraConfiguration : IEntityTypeConfiguration<PedidoCabecera>
    {
        public void Configure(EntityTypeBuilder<PedidoCabecera> builder)
        {

            builder.Property(x => x.NumeroOperacion)
                .IsUnicode(false)
                .HasMaxLength(10);

            builder.Property(x => x.FechaPedido)
                .HasColumnType("date")
                .HasDefaultValueSql("GETDATE()");

            builder.ToTable(nameof(PedidoCabecera), "PedidoCabecera");
        }
    }
}
