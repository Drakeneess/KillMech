using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace KillMech.Models;

public partial class KillmechContext : DbContext
{
    public KillmechContext()
    {
    }

    public KillmechContext(DbContextOptions<KillmechContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Arma> Armas { get; set; }

    public virtual DbSet<AtaqueJefe> AtaqueJeves { get; set; }

    public virtual DbSet<Capitulo> Capitulos { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Desafio> Desafios { get; set; }

    public virtual DbSet<DesafioJugador> DesafioJugadors { get; set; }

    public virtual DbSet<DesempenoCapitulo> DesempenoCapitulos { get; set; }

    public virtual DbSet<DesempenoJefe> DesempenoJeves { get; set; }

    public virtual DbSet<DesempenoNivel> DesempenoNivels { get; set; }

    public virtual DbSet<Dificultad> Dificultads { get; set; }

    public virtual DbSet<Enemigo> Enemigos { get; set; }

    public virtual DbSet<Jefe> Jeves { get; set; }

    public virtual DbSet<Jugador> Jugadors { get; set; }

    public virtual DbSet<Muerte> Muertes { get; set; }

    public virtual DbSet<Partidum> Partida { get; set; }

    public virtual DbSet<Sesion> Sesions { get; set; }

    public virtual DbSet<UsoArma> UsoArmas { get; set; }

    public virtual DbSet<Version> Versions { get; set; }

    public virtual DbSet<VersionJugador> VersionJugadors { get; set; }

    public virtual DbSet<Victorium> Victoria { get; set; }

    public virtual DbSet<desempeñoporjudador> Desempeñoporjudadors { get; set; }

    public virtual DbSet<EnemigoCategoria> EnemigoCategorias { get; set; }

    public virtual DbSet<DificultadCapitulo> DificultadCapitulos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=killmech;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DificultadCapitulo>(entity =>
        {
            entity.HasNoKey(); // Indica que la entidad no tiene clave primaria porque es una vista
            entity.ToView("VistaDificultadCapitulo"); // Asegúrate de que el nombre de la vista coincida con la base de datos

            // Mapea las propiedades a los nombres de columnas correspondientes si es necesario
            entity.Property(e => e.NombreCapitulo).HasColumnName("NombreCapitulo");
            entity.Property(e => e.HorasPromedioCompletacion).HasColumnName("HorasPromedioCompletacion").HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DanoRecibidoPromedio).HasColumnName("DanoRecibidoPromedio").HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DisparosRealizadosPromedio).HasColumnName("DisparosRealizadosPromedio").HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PorcentajeDisparosAcertados).HasColumnName("PorcentajeDisparosAcertados").HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransformacionesRealizadasPromedio).HasColumnName("TransformacionesRealizadasPromedio").HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PorcentajeCompletados).HasColumnName("PorcentajeCompletados").HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IndiceDificultad).HasColumnName("IndiceDificultad").HasColumnType("decimal(18, 2)");
        });





        modelBuilder.Entity<desempeñoporjudador>(entity =>
        {
            entity.HasNoKey(); // Indica que la entidad no tiene clave primaria
            entity.ToView("desempenoporjugador"); // Asegúrate de que el nombre de la vista coincida con la base de datos

            // Mapea las propiedades a los nombres de columnas correspondientes si es necesario
            entity.Property(j => j.JugadorID).HasColumnName("JugadorID");
            entity.Property(j => j.NombreJugador).HasColumnName("NombreJugador");
            entity.Property(j => j.TiempoTotalJugado).HasColumnName("TiempoTotalJugado");
            entity.Property(j => j.PartidasJugadas).HasColumnName("PartidasJugadas");
            entity.Property(j => j.PartidasCompletadas).HasColumnName("PartidasCompletadas");
            entity.Property(j => j.PartidasAbandonadas).HasColumnName("PartidasAbandonadas");
        });
        modelBuilder.Entity<EnemigoCategoria>(entity =>
        {
            entity.HasNoKey(); // Indica que la entidad no tiene clave primaria
            entity.ToView("vista_enemigos_categorias"); // Asegúrate de que el nombre de la vista coincida con el de la base de datos

            // Mapea las propiedades a los nombres de columnas correspondientes si es necesario
            entity.Property(j => j.NombreEnemigo).HasColumnName("NombreEnemigo");
            entity.Property(j => j.Categoria).HasColumnName("Categoria");
            entity.Property(j => j.Mortalidad).HasColumnName("Mortalidad");
            
        });
    

    modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Arma>(entity =>
        {
            entity.HasKey(e => e.ArmaId).HasName("PRIMARY");

            entity.ToTable("arma");

            entity.Property(e => e.ArmaId).HasColumnName("ArmaID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<AtaqueJefe>(entity =>
        {
            entity.HasKey(e => e.AtaqueJefeId).HasName("PRIMARY");

            entity.ToTable("ataque_jefe");

            entity.HasIndex(e => e.JefeId, "fk_ataque_jefe_idx");

            entity.Property(e => e.AtaqueJefeId).HasColumnName("ataque_jefeID");
            entity.Property(e => e.JefeId).HasColumnName("jefeID");
            entity.Property(e => e.Mortalidad)
                .HasDefaultValueSql("'0'")
                .HasColumnName("mortalidad");
            entity.Property(e => e.NombreAtaque)
                .HasMaxLength(45)
                .HasColumnName("nombre_ataque");

            entity.HasOne(d => d.Jefe).WithMany(p => p.AtaqueJeves)
                .HasForeignKey(d => d.JefeId)
                .HasConstraintName("fk_ataque_jefe");
        });

        modelBuilder.Entity<Capitulo>(entity =>
        {
            entity.HasKey(e => e.NivelID).HasName("PRIMARY");

            entity.ToTable("capitulo");

            entity.HasIndex(e => e.DificultadId, "fk_capitulo_dificultad_idx");

            entity.Property(e => e.NivelID).HasColumnName("NivelID");
            entity.Property(e => e.DificultadId).HasColumnName("dificultadID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Dificultad).WithMany(p => p.Capitulos)
                .HasForeignKey(d => d.DificultadId)
                .HasConstraintName("fk_capitulo_dificultad");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(45)
                .HasColumnName("nombre_categoria");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.ComentarioId).HasName("PRIMARY");

            entity.ToTable("comentario");

            entity.HasIndex(e => e.JugadorId, "fk_comentario_jugador_idx");

            entity.Property(e => e.ComentarioId).HasColumnName("comentarioID");
            entity.Property(e => e.Comentario1)
                .HasMaxLength(45)
                .HasColumnName("comentario");
            entity.Property(e => e.JugadorId).HasColumnName("jugadorID");

            entity.HasOne(d => d.Jugador).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.JugadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_comentario_jugador");
        });

        modelBuilder.Entity<Desafio>(entity =>
        {
            entity.HasKey(e => e.DesafioId).HasName("PRIMARY");

            entity.ToTable("desafio");

            entity.Property(e => e.DesafioId).HasColumnName("DesafioID");
            entity.Property(e => e.NombreDesafio)
                .HasMaxLength(45)
                .HasColumnName("nombre_desafio");
        });

        modelBuilder.Entity<DesafioJugador>(entity =>
        {
            entity.HasKey(e => e.DesafioJugadorId).HasName("PRIMARY");

            entity.ToTable("desafio_jugador");

            entity.HasIndex(e => e.JugadorId, "fk_Desafio_Jugador");

            entity.HasIndex(e => e.DesafioId, "fk_Jugador_Desafio_idx");

            entity.Property(e => e.DesafioJugadorId).HasColumnName("DesafioJugadorID");
            entity.Property(e => e.DesafioId).HasColumnName("DesafioID");
            entity.Property(e => e.JugadorId).HasColumnName("JugadorID");

            entity.HasOne(d => d.Desafio).WithMany(p => p.DesafioJugadors)
                .HasForeignKey(d => d.DesafioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Jugador_Desafio");

            entity.HasOne(d => d.Jugador).WithMany(p => p.DesafioJugadors)
                .HasForeignKey(d => d.JugadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Desafio_Jugador");
        });

        modelBuilder.Entity<DesempenoCapitulo>(entity =>
        {
            entity.HasKey(e => e.DesempenoCapituloId).HasName("PRIMARY");

            entity.ToTable("desempeno_capitulo");

            entity.HasIndex(e => e.PartidaId, "fk_nivel_partida_idx");

            entity.HasIndex(e => e.NivelId, "fk_partida_nivel_idx");

            entity.Property(e => e.DesempenoCapituloId).HasColumnName("Desempeno_CapituloID");
            entity.Property(e => e.Completo).HasColumnName("completo");
            entity.Property(e => e.DanoJetTotal).HasColumnName("dano_jet_total");
            entity.Property(e => e.DanoMechaTotal).HasColumnName("dano_mecha_total");
            entity.Property(e => e.DanoTotalHecho).HasColumnName("dano_total_hecho");
            entity.Property(e => e.DanoTotalRecibido).HasColumnName("dano_total_recibido");
            entity.Property(e => e.DisparosAcertados).HasColumnName("disparos_acertados");
            entity.Property(e => e.DisparosRealizados).HasColumnName("disparos_realizados");
            entity.Property(e => e.EvasionesRealizadas).HasColumnName("evasiones_realizadas");
            entity.Property(e => e.NivelId).HasColumnName("NivelID");
            entity.Property(e => e.PartidaId).HasColumnName("PartidaID");
            entity.Property(e => e.TiempoCompletacion)
                .HasColumnType("time")
                .HasColumnName("tiempo_completacion");
           
            entity.Property(e => e.TransformacionesRealizadas).HasColumnName("transformaciones_realizadas");

            entity.HasOne(d => d.Nivel).WithMany(p => p.DesempenoCapitulos)
                .HasForeignKey(d => d.NivelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_partida_nivel");

            entity.HasOne(d => d.Partida).WithMany(p => p.DesempenoCapitulos)
                .HasForeignKey(d => d.PartidaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_nivel_partida");
        });

        modelBuilder.Entity<DesempenoJefe>(entity =>
        {
            entity.HasKey(e => e.DesempenoId).HasName("PRIMARY");

            entity.ToTable("desempeno_jefe");

            entity.HasIndex(e => e.ArmaId, "fk_Desempeno_jefe_Arma");

            entity.HasIndex(e => e.JefeId, "fk_Desempeno_jefe_Jefe");

            entity.HasIndex(e => e.DesempenoCapituloId, "fk_Desempeno_jefe_desempeno_capitulo_idx");

            entity.Property(e => e.DesempenoId).HasColumnName("DesempenoID");
            entity.Property(e => e.ArmaId).HasColumnName("ArmaID");
            entity.Property(e => e.DesempenoCapituloId).HasColumnName("Desempeno_CapituloID");
            entity.Property(e => e.JefeId).HasColumnName("JefeID");
            entity.Property(e => e.TiempoDerrota)
                .HasColumnType("datetime")
                .HasColumnName("tiempo_derrota");

            entity.HasOne(d => d.Arma).WithMany(p => p.DesempenoJeves)
                .HasForeignKey(d => d.ArmaId)
                .HasConstraintName("fk_Desempeno_jefe_Arma");

            entity.HasOne(d => d.DesempenoCapitulo).WithMany(p => p.DesempenoJeves)
                .HasForeignKey(d => d.DesempenoCapituloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Desempeno_jefe_desempeno_capitulo");

            entity.HasOne(d => d.Jefe).WithMany(p => p.DesempenoJeves)
                .HasForeignKey(d => d.JefeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Desempeno_jefe_Jefe");
        });

        modelBuilder.Entity<DesempenoNivel>(entity =>
        {
            entity.HasKey(e => e.DesempenoId).HasName("PRIMARY");

            entity.ToTable("desempeno_nivel");

            entity.HasIndex(e => e.JugadorId, "fk_Desempeno_nivel_Jugador");

            entity.HasIndex(e => e.NivelId, "fk_Desempeno_nivel_Nivel");

            entity.Property(e => e.DesempenoId).HasColumnName("DesempenoID");
            entity.Property(e => e.JugadorId).HasColumnName("JugadorID");
            entity.Property(e => e.NivelId).HasColumnName("NivelID");
            entity.Property(e => e.SuperpoderId).HasColumnName("SuperpoderID");
            entity.Property(e => e.TiempoCompletamiento).HasColumnType("time");

            entity.HasOne(d => d.Jugador).WithMany(p => p.DesempenoNivels)
                .HasForeignKey(d => d.JugadorId)
                .HasConstraintName("fk_Desempeno_nivel_Jugador");

            entity.HasOne(d => d.Nivel).WithMany(p => p.DesempenoNivels)
                .HasForeignKey(d => d.NivelId)
                .HasConstraintName("fk_Desempeno_nivel_Nivel");
        });

        modelBuilder.Entity<Dificultad>(entity =>
        {
            entity.HasKey(e => e.DificultadId).HasName("PRIMARY");

            entity.ToTable("dificultad");

            entity.Property(e => e.DificultadId).HasColumnName("DificultadID");
            entity.Property(e => e.DifultadNombre)
                .HasMaxLength(45)
                .HasColumnName("difultad_nombre");
        });

        modelBuilder.Entity<Enemigo>(entity =>
        {
            entity.HasKey(e => e.EnemigoId).HasName("PRIMARY");

            entity.ToTable("enemigo");

            entity.HasIndex(e => e.CategoriaId, "fk_enemigo_categoria_idx");

            entity.Property(e => e.EnemigoId).HasColumnName("EnemigoID");
            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Mortalidad).HasColumnName("mortalidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Enemigos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("fk_enemigo_categoria");
        });

        modelBuilder.Entity<Jefe>(entity =>
        {
            entity.HasKey(e => e.JefeId).HasName("PRIMARY");

            entity.ToTable("jefe");

            entity.HasIndex(e => e.EnemigoId, "fk_jefe_enemigo_idx");

            entity.Property(e => e.JefeId).HasColumnName("JefeID");
            entity.Property(e => e.EnemigoId).HasColumnName("EnemigoID");

            entity.HasOne(d => d.Enemigo).WithMany(p => p.Jeves)
                .HasForeignKey(d => d.EnemigoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_jefe_enemigo");
        });

        modelBuilder.Entity<Jugador>(entity =>
        {
            entity.HasKey(e => e.JugadorId).HasName("PRIMARY");

            entity.ToTable("jugador");

            entity.HasIndex(e => e.CodigoJugador, "codigo_jugador_UNIQUE").IsUnique();

            entity.Property(e => e.JugadorId).HasColumnName("JugadorID");
            entity.Property(e => e.CodigoJugador)
                .HasMaxLength(45)
                .HasColumnName("codigo_jugador");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.TiempoJugado)
                .HasColumnType("time")
                .HasColumnName("tiempo_jugado");
        });

        modelBuilder.Entity<Muerte>(entity =>
        {
            entity.HasKey(e => e.MuerteId).HasName("PRIMARY");

            entity.ToTable("muerte");

            entity.HasIndex(e => e.EnemigoId, "fk_Muerte_Enemigo");

            entity.HasIndex(e => e.DesempenoNivelId, "fk_Muerte_NivelPArtida_idx");

            entity.Property(e => e.MuerteId).HasColumnName("MuerteID");
            entity.Property(e => e.DesempenoNivelId).HasColumnName("Desempeno_NivelID");
            entity.Property(e => e.EnemigoId).HasColumnName("EnemigoID");
            entity.Property(e => e.TiempoMuerte)
                .HasColumnType("datetime")
                .HasColumnName("tiempo_muerte");
            entity.Property(e => e.X).HasColumnName("x");
            entity.Property(e => e.Y).HasColumnName("y");

            entity.HasOne(d => d.DesempenoNivel).WithMany(p => p.Muertes)
                .HasForeignKey(d => d.DesempenoNivelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Muerte_NivelPArtida");

            entity.HasOne(d => d.Enemigo).WithMany(p => p.Muertes)
                .HasForeignKey(d => d.EnemigoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Muerte_Enemigo");
        });

        modelBuilder.Entity<Partidum>(entity =>
        {
            entity.HasKey(e => e.PartidaId).HasName("PRIMARY");

            entity.ToTable("partida");

            entity.HasIndex(e => e.JugadorId, "fk_Partida_Jugador");

            entity.Property(e => e.PartidaId).HasColumnName("PartidaID");
            entity.Property(e => e.CampanaCompleta).HasColumnName("campana_completa");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.JugadorId).HasColumnName("JugadorID");

            entity.HasOne(d => d.Jugador).WithMany(p => p.Partida)
                .HasForeignKey(d => d.JugadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Partida_Jugador");
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.SesionId).HasName("PRIMARY");

            entity.ToTable("sesion");

            entity.HasIndex(e => e.PartidaId, "fk_Sesion_Jugador_idx");

            entity.Property(e => e.SesionId).HasColumnName("SesionID");
            entity.Property(e => e.Duracion)
                .HasColumnType("time")
                .HasColumnName("duracion");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.NivelesCompletados).HasColumnName("niveles_completados");
            entity.Property(e => e.PartidaId).HasColumnName("PartidaID");

            entity.HasOne(d => d.Partida).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.PartidaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Sesion_Jugador");
        });

        modelBuilder.Entity<UsoArma>(entity =>
        {
            entity.HasKey(e => e.UsoArmaId).HasName("PRIMARY");

            entity.ToTable("uso_arma");

            entity.HasIndex(e => e.ArmaId, "fk_arma_desempeno_idx");

            entity.HasIndex(e => e.DesempenoCapituloId, "fk_desempeno_arma_idx");

            entity.Property(e => e.UsoArmaId).HasColumnName("Uso_ArmaID");
            entity.Property(e => e.ArmaId).HasColumnName("armaID");
            entity.Property(e => e.DesempenoCapituloId).HasColumnName("desempeno_capituloID");
            entity.Property(e => e.Uso).HasColumnName("uso");

            entity.HasOne(d => d.Arma).WithMany(p => p.UsoArmas)
                .HasForeignKey(d => d.ArmaId)
                .HasConstraintName("fk_arma_desempeno");

            entity.HasOne(d => d.DesempenoCapitulo).WithMany(p => p.UsoArmas)
                .HasForeignKey(d => d.DesempenoCapituloId)
                .HasConstraintName("fk_desempeno_arma");
        });

        modelBuilder.Entity<Version>(entity =>
        {
            entity.HasKey(e => e.VersionId).HasName("PRIMARY");

            entity.ToTable("version");

            entity.Property(e => e.VersionId).HasColumnName("versionID");
            entity.Property(e => e.FechaLanzamiento)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha_lanzamiento");
            entity.Property(e => e.NumeroVersion)
                .HasMaxLength(45)
                .HasColumnName("numero_version");
        });

        modelBuilder.Entity<VersionJugador>(entity =>
        {
            entity.HasKey(e => e.VersionJugadorId).HasName("PRIMARY");

            entity.ToTable("version_jugador");

            entity.HasIndex(e => e.VersionId, "fk_jugador_version_idx");

            entity.HasIndex(e => e.JugadorId, "fk_version_jugador_idx");

            entity.Property(e => e.VersionJugadorId).HasColumnName("Version_JugadorID");
            entity.Property(e => e.JugadorId).HasColumnName("JugadorID");
            entity.Property(e => e.VersionId).HasColumnName("VersionID");

            entity.HasOne(d => d.Jugador).WithMany(p => p.VersionJugadors)
                .HasForeignKey(d => d.JugadorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_version_jugador");

            entity.HasOne(d => d.Version).WithMany(p => p.VersionJugadors)
                .HasForeignKey(d => d.VersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_jugador_version");
        });

        modelBuilder.Entity<Victorium>(entity =>
        {
            entity.HasKey(e => e.VictoriaId).HasName("PRIMARY");

            entity.ToTable("victoria");

            entity.HasIndex(e => e.JefeId, "fk_Victoria_Jefe");

            entity.HasIndex(e => e.JugadorId, "fk_Victoria_Jugador");

            entity.HasIndex(e => e.PartidaId, "fk_Victoria_Partida");

            entity.Property(e => e.VictoriaId).HasColumnName("VictoriaID");
            entity.Property(e => e.JefeId).HasColumnName("JefeID");
            entity.Property(e => e.JugadorId).HasColumnName("JugadorID");
            entity.Property(e => e.PartidaId).HasColumnName("PartidaID");
            entity.Property(e => e.TiempoCompletamiento).HasColumnType("time");

            entity.HasOne(d => d.Jefe).WithMany(p => p.Victoria)
                .HasForeignKey(d => d.JefeId)
                .HasConstraintName("fk_Victoria_Jefe");

            entity.HasOne(d => d.Jugador).WithMany(p => p.Victoria)
                .HasForeignKey(d => d.JugadorId)
                .HasConstraintName("fk_Victoria_Jugador");

            entity.HasOne(d => d.Partida).WithMany(p => p.Victoria)
                .HasForeignKey(d => d.PartidaId)
                .HasConstraintName("fk_Victoria_Partida");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
