using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSophos.Models;

[Table("profesores")]
public partial class Profesore
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(255)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("maximo_titulo_academico")]
    [StringLength(255)]
    [Unicode(false)]
    public string MaximoTituloAcademico { get; set; } = null!;

    [Column("experiencia")]
    public int Experiencia { get; set; }

    [InverseProperty("Profesor")]
    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
