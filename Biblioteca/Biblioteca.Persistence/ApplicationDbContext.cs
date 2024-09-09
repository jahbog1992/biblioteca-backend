using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Entities;
using Biblioteca.Entities.Info;
using System.Reflection;

namespace Biblioteca.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<BibliotecaUserIdentity>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ReportInfo>().HasNoKey(); 

            modelBuilder.Entity<BibliotecaUserIdentity>(x => x.ToTable("Usuarios"));
            modelBuilder.Entity<IdentityRole>(x => x.ToTable("Roles"));
            modelBuilder.Entity<IdentityUserRole<string>>(x => x.ToTable("UsuariosRoles"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
