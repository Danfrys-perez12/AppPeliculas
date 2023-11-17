using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppPeliculas.Models;

public partial class AppPeliculasContext : DbContext
{
    public AppPeliculasContext()
    {
    }

    public AppPeliculasContext(DbContextOptions<AppPeliculasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actores> Actores { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actores>(entity =>
        {
            entity.HasKey(e => e.IdActor).HasName("PK__ACTORES__DF65C9BF4E238D25");

            entity.ToTable("ACTORES");

            entity.Property(e => e.IdActor).HasColumnName("Id_Actor");
            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaNacimiento)
                .HasMaxLength(50)
                .HasColumnName("Fecha_Nacimiento");
            entity.Property(e => e.IdPelicula).HasColumnName("Id_Pelicula");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Pelicula).HasMaxLength(50);

            entity.HasOne(d => d.OPeliculas).WithMany(p => p.Actores)
                .HasForeignKey(d => d.IdPelicula)
                .HasConstraintName("fk_Pelicula");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.IdPelicula).HasName("PK__PELICULA__E239B742D40243E4");

            entity.ToTable("PELICULAS");

            entity.Property(e => e.IdPelicula).HasColumnName("Id_Pelicula");
            entity.Property(e => e.Actor).HasMaxLength(50);
            entity.Property(e => e.Estreno).HasMaxLength(50);
            entity.Property(e => e.Titulo).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__USUARIOS__D03DEDCB3B9F23DC");

            entity.ToTable("USUARIOS");

            entity.Property(e => e.IdUser).HasColumnName("Id_User");
            entity.Property(e => e.Clave).HasMaxLength(100);
            entity.Property(e => e.Imail).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
