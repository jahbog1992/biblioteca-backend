using Biblioteca.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore; 

namespace Biblioteca.Persistence.Configurations
{
    public class LibroConfiguration : IEntityTypeConfiguration<Libro>
    {
        public void Configure(EntityTypeBuilder<Libro> builder)
        {
            builder.Property(x => x.Nombre)
                .HasMaxLength(200);

            builder.Property(x => x.Autor)
                .HasMaxLength(200);

            builder.Property(x => x.ISBN)
                .HasMaxLength(200);

            builder.ToTable(nameof(Libro), "Libros");
        }
    }
}
