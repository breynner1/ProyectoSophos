using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSophos.Models;

[Table("alumnos")]
public partial class Alumno
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(255)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("facultad_id")]
    public int? FacultadId { get; set; }

    [Column("creditos_inscritos")]
    public int CreditosInscritos { get; set; }

    [Column("semestre")]
    public int Semestre { get; set; }

    [ForeignKey("FacultadId")]
    [InverseProperty("Alumnos")]
    public virtual Facultad? Facultad { get; set; }

    [InverseProperty("Alumno")]
    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
