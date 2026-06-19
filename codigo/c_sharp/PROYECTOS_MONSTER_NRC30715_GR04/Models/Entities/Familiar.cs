using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("PEFAE_FAMILIAR")]
public class Familiar
{
    [Key]
    [Column("PEFAE_ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long Id { get; set; }

    [Column("PEEMP_CODIGO")]
    [StringLength(6)]
    public string? EmpleadoCodigo { get; set; }

    [Required]
    [Column("PEFAE_NOMBRES")]
    [StringLength(50)]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    [Column("PEFAE_APELL")]
    [StringLength(50)]
    public string Apellidos { get; set; } = string.Empty;

    [Required]
    [Column("PEFAE_FECHAN")]
    public DateTime FechaNacimiento { get; set; }

    [Required]
    [Column("PEFAE_EDAD")]
    public int Edad { get; set; }

    [Required]
    [Column("PEFAE_PARENT")]
    [StringLength(50)]
    public string Parentesco { get; set; } = string.Empty;

    // Navigation property
    public Empleado? Empleado { get; set; }
}
