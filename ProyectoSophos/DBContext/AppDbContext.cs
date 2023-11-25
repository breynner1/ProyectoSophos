using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoSophos.Models;

namespace ProyectoSophos.DBContext;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Facultad> Facultads { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-CDLGFBD\\SQLEXPRESS;Initial Catalog=ProyectoS;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__alumnos__3213E83F761ED1B0");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Facultad).WithMany(p => p.Alumnos)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__alumnos__faculta__2CF2ADDF");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cursos__3213E83F403BA525");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Prerequisito).WithMany(p => p.InversePrerequisito).HasConstraintName("FK__cursos__prerequi__32AB8735");

            entity.HasOne(d => d.Profesor).WithMany(p => p.Cursos)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__cursos__profesor__31B762FC");
        });

        modelBuilder.Entity<Facultad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__facultad__3213E83F55A5ACAD");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__matricul__3213E83F7C2ADA4E");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Alumno).WithMany(p => p.Matriculas).HasConstraintName("FK__matricula__alumn__3587F3E0");

            entity.HasOne(d => d.Curso).WithMany(p => p.Matriculas).HasConstraintName("FK__matricula__curso__367C1819");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__profesor__3213E83FA1EB01B5");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
