using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSophos.Models;

[Table("matriculas")]
public partial class Matricula
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("alumno_id")]
    public int AlumnoId { get; set; }

    [Column("curso_id")]
    public int CursoId { get; set; }

    [ForeignKey("AlumnoId")]
    [InverseProperty("Matriculas")]
    public virtual Alumno Alumno { get; set; } = null!;

    [ForeignKey("CursoId")]
    [InverseProperty("Matriculas")]
    public virtual Curso Curso { get; set; } = null!;
}
