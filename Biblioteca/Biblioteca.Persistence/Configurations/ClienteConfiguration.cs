using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.Entities;

namespace Biblioteca.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.Property(x => x.Apellidos)
                .HasMaxLength(200);

            builder.Property(x => x.Nombres)
                .HasMaxLength(200);

            builder.Property(x => x.DNI)
                .HasMaxLength(8) 
                .IsUnicode(false);

            builder.HasIndex(x => x.DNI)
                .IsUnique();

            builder.ToTable(nameof(Cliente), "Clientes");
        }
    }
}
