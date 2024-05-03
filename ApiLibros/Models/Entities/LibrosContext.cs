using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ApiLibros.Models.Entities;

public partial class LibrosContext : DbContext
{
    public LibrosContext()
    {
    }

    public LibrosContext(DbContextOptions<LibrosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Libros> Libros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=204.93.216.11;database=itesrcne_libros;user=itesrcne_libuser;password=8Ex298hJU3Ts", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.29-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Libros>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("libros");

            entity.HasIndex(e => e.Id, "libros_Id_IDX");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Autor).HasMaxLength(100);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.Portada).HasMaxLength(100);
            entity.Property(e => e.Titulo).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
