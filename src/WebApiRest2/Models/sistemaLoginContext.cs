using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiRest2.Models
{
    public partial class sistemaLoginContext : DbContext
    {
        public sistemaLoginContext()
        {
        }

        public sistemaLoginContext(DbContextOptions<sistemaLoginContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TablaUsuarios> TablaUsuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
 optionsBuilder.UseSqlServer("Data Source=VALK2-PC\\SQLEXPRESS01; Initial Catalog=sistemaLogin;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TablaUsuarios>(entity =>
            {
                entity.HasKey(e => e.IdPruenbas);

                entity.Property(e => e.IdPruenbas)
                    .HasColumnName("idPruenbas")
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("idUsuario")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Idempresa)
                    .HasColumnName("idempresa")
                    .HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
