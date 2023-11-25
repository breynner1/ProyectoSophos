using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSophos.Models;

[Table("facultad")]
public partial class Facultad
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(255)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("Facultad")]
    public virtual ICollection<Alumno> Alumnos { get; set; } = new List<Alumno>();
}
