using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace HackaTec.Models.Entities;

public partial class HackatecContext : DbContext
{
    public HackatecContext()
    {
    }

    public HackatecContext(DbContextOptions<HackatecContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrador> Administrador { get; set; }

    public virtual DbSet<ImagenesNecesidad> ImagenesNecesidad { get; set; }

    public virtual DbSet<InstitucionesEducativas> InstitucionesEducativas { get; set; }

    public virtual DbSet<Mensajes> Mensajes { get; set; }

    public virtual DbSet<PublicacionesAgradecimiento> PublicacionesAgradecimiento { get; set; }

    public virtual DbSet<PublicacionesNecesidad> PublicacionesNecesidad { get; set; }

    public virtual DbSet<SalasChat> SalasChat { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("user=root;password=root;server=localhost;database=Hackatec;port=3306", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("administrador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .HasColumnName("contrasena");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<ImagenesNecesidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("imagenes_necesidad");

            entity.HasIndex(e => e.IdPublicacion, "FK_Imagen_Necesidad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPublicacion).HasColumnName("id_publicacion");
            entity.Property(e => e.RutaImagen)
                .HasMaxLength(255)
                .HasColumnName("ruta_imagen");

            entity.HasOne(d => d.IdPublicacionNavigation).WithMany(p => p.ImagenesNecesidad)
                .HasForeignKey(d => d.IdPublicacion)
                .HasConstraintName("FK_Imagen_Necesidad");
        });

        modelBuilder.Entity<InstitucionesEducativas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("instituciones_educativas");

            entity.HasIndex(e => e.Cct, "cct").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cct)
                .HasMaxLength(10)
                .HasColumnName("cct");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .HasColumnName("contrasena");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .HasColumnName("nombre");
            entity.Property(e => e.PersonaResponsable)
                .HasMaxLength(100)
                .HasColumnName("persona_responsable");
            entity.Property(e => e.TelefonoEscuela)
                .HasMaxLength(10)
                .HasColumnName("telefono_escuela");
            entity.Property(e => e.TelefonoResponsable)
                .HasMaxLength(10)
                .HasColumnName("telefono_responsable");
        });

        modelBuilder.Entity<Mensajes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mensajes");

            entity.HasIndex(e => e.IdSala, "FK_Mensaje_Sala");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contenido)
                .HasColumnType("text")
                .HasColumnName("contenido");
            entity.Property(e => e.EstadoLeido).HasColumnName("estado_leido");
            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_envio");
            entity.Property(e => e.IdRemitente).HasColumnName("id_remitente");
            entity.Property(e => e.IdSala).HasColumnName("id_sala");
            entity.Property(e => e.RemitenteTipo)
                .HasColumnType("enum('Donante','Escuela')")
                .HasColumnName("remitente_tipo");
            entity.Property(e => e.RutaImagen)
                .HasMaxLength(255)
                .HasColumnName("ruta_imagen");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.IdSala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mensaje_Sala");
        });

        modelBuilder.Entity<PublicacionesAgradecimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("publicaciones_agradecimiento");

            entity.HasIndex(e => e.IdPublicacionNecesidad, "FK_Necesidad_Agradecimiento");

            entity.HasIndex(e => e.IdUsuarioDonante, "FK_Usuario_Agradecimiento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdPublicacionNecesidad).HasColumnName("id_publicacion_necesidad");
            entity.Property(e => e.IdUsuarioDonante).HasColumnName("id_usuario_donante");
            entity.Property(e => e.RutaFotografia)
                .HasMaxLength(255)
                .HasColumnName("ruta_fotografia");

            entity.HasOne(d => d.IdPublicacionNecesidadNavigation).WithMany(p => p.PublicacionesAgradecimiento)
                .HasForeignKey(d => d.IdPublicacionNecesidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Necesidad_Agradecimiento");

            entity.HasOne(d => d.IdUsuarioDonanteNavigation).WithMany(p => p.PublicacionesAgradecimiento)
                .HasForeignKey(d => d.IdUsuarioDonante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Agradecimiento");
        });

        modelBuilder.Entity<PublicacionesNecesidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("publicaciones_necesidad");

            entity.HasIndex(e => e.IdInstitucion, "FK_institucion_publicaciones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(150)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.PublicacionesNecesidad)
                .HasForeignKey(d => d.IdInstitucion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_institucion_publicaciones");
        });

        modelBuilder.Entity<SalasChat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("salas_chat");

            entity.HasIndex(e => e.IdUsuarioDonante, "FK_Chat_Donante");

            entity.HasIndex(e => e.IdInstitucion, "FK_Chat_Institucion");

            entity.HasIndex(e => e.IdPostNecesidad, "FK_Chat_Post");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdInstitucion).HasColumnName("id_institucion");
            entity.Property(e => e.IdPostNecesidad).HasColumnName("id_post_necesidad");
            entity.Property(e => e.IdUsuarioDonante).HasColumnName("id_usuario_donante");

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.SalasChat)
                .HasForeignKey(d => d.IdInstitucion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chat_Institucion");

            entity.HasOne(d => d.IdPostNecesidadNavigation).WithMany(p => p.SalasChat)
                .HasForeignKey(d => d.IdPostNecesidad)
                .HasConstraintName("FK_Chat_Post");

            entity.HasOne(d => d.IdUsuarioDonanteNavigation).WithMany(p => p.SalasChat)
                .HasForeignKey(d => d.IdUsuarioDonante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chat_Donante");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Correo, "correo").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .HasColumnName("apellidos");
            entity.Property(e => e.ContrasenaHash)
                .HasMaxLength(255)
                .HasColumnName("contrasena_hash");
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .HasColumnName("correo");
            entity.Property(e => e.FotoPerfil)
                .HasMaxLength(255)
                .HasColumnName("foto_perfil");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Persona'")
                .HasColumnName("rol");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
