using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaBox.Storing {
    public partial class PizzaProjectContext : DbContext {
        public PizzaProjectContext()
        {
        }

        public PizzaProjectContext(DbContextOptions<PizzaProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<PizzaOrder> PizzaOrder { get; set; }
        public virtual DbSet<Store> Store { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=localhost;database=PizzaProject;user id=sa;password=Passw0rd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Menu", "Project");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Pizza)
                    .WithMany()
                    .HasForeignKey(d => d.PizzaId)
                    .HasConstraintName("FK__Menu__PizzaID__2FCF1A8A");

                entity.HasOne(d => d.Store)
                    .WithMany()
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Menu__StoreID__2EDAF651");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("Pizza", "Project");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Crust)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Toppings)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PizzaOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__PizzaOrd__C3905BAFE99F24F2");

                entity.ToTable("PizzaOrder", "Project");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.WhenOrdered).HasColumnType("datetime");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzaOrder)
                    .HasForeignKey(d => d.PizzaId)
                    .HasConstraintName("FK__PizzaOrde__Pizza__3B40CD36");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.PizzaOrder)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__PizzaOrde__Store__3A4CA8FD");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store", "Project");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
