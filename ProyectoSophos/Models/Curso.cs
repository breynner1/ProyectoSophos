using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSophos.Models;

[Table("cursos")]
public partial class Curso
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(255)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("prerequisito_id")]
    public int? PrerequisitoId { get; set; }

    [Column("creditos")]
    public int Creditos { get; set; }

    [Column("estado")]
    public bool Estado { get; set; }

    [Column("cupos_disponibles")]
    public int CuposDisponibles { get; set; }

    [Column("profesor_id")]
    public int? ProfesorId { get; set; }

    [InverseProperty("Prerequisito")]
    public virtual ICollection<Curso> InversePrerequisito { get; set; } = new List<Curso>();

    [InverseProperty("Curso")]
    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();

    [ForeignKey("PrerequisitoId")]
    [InverseProperty("InversePrerequisito")]
    public virtual Curso? Prerequisito { get; set; }

    [ForeignKey("ProfesorId")]
    [InverseProperty("Cursos")]
    public virtual Profesore? Profesor { get; set; }
}
