using Microsoft.EntityFrameworkCore;

namespace BancoDesafio.ConsoleApp.Models
{
    public partial class BANCODESAFIOContext : DbContext
    {
        public BANCODESAFIOContext()
        {
        }

        public BANCODESAFIOContext(DbContextOptions<BANCODESAFIOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Financiamento> Financiamentos { get; set; } = null!;
        public virtual DbSet<Parcela> Parcelas { get; set; } = null!;
        public virtual DbSet<TipoFinanciamento> TipoFinanciamentos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=BANCODESAFIO;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Celular)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Uf)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Financiamento>(entity =>
            {
                entity.ToTable("Financiamento");

                entity.Property(e => e.DataVencimento).HasColumnType("datetime");

                entity.Property(e => e.ValorTotal).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Financiamentos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_IdCliente");

                entity.HasOne(d => d.TipoFinanciamentoNavigation)
                    .WithMany(p => p.Financiamentos)
                    .HasForeignKey(d => d.TipoFinanciamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TipoFinanciamento");
            });

            modelBuilder.Entity<Parcela>(entity =>
            {
                entity.ToTable("Parcela");

                entity.Property(e => e.DataPagamento).HasColumnType("datetime");

                entity.Property(e => e.DataVencimento).HasColumnType("datetime");

                entity.Property(e => e.ValorParcela).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdFinanciamentoNavigation)
                    .WithMany(p => p.Parcelas)
                    .HasForeignKey(d => d.IdFinanciamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_IdFinanciamento");
            });

            modelBuilder.Entity<TipoFinanciamento>(entity =>
            {
                entity.ToTable("TipoFinanciamento");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Taxa).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
