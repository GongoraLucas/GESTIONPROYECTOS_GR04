using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("PEEMP_EMPLE")]
public class Empleado
{
    [Key]
    [Column("PEEMP_CODIGO")]
    [StringLength(6)]
    public string Codigo { get; set; } = string.Empty;

    [Required]
    [Column("PESEX_CODIGO")]
    [StringLength(1)]
    public string SexoCodigo { get; set; } = string.Empty;

    [Column("PEESC_CODIGO")]
    [StringLength(1)]
    public string? EstadoCivilCodigo { get; set; }

    [Required]
    [Column("PEDEP_CODIGO")]
    [StringLength(3)]
    public string DepartamentoCodigo { get; set; } = string.Empty;

    [Required]
    [Column("PECAR_CODIGO")]
    [StringLength(3)]
    public string CargoCodigo { get; set; } = string.Empty;

    [Column("PEE_PEEMP_CODIGO")]
    [StringLength(6)]
    public string? JefeCodigo { get; set; }

    [Required]
    [Column("PEEMP_APELLI")]
    [StringLength(50)]
    public string Apellidos { get; set; } = string.Empty;

    [Required]
    [Column("PEEMP_NOMBRE")]
    [StringLength(50)]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    [Column("PEEMP_DIREC")]
    [StringLength(200)]
    public string Direccion { get; set; } = string.Empty;

    [Required]
    [Column("PEEMP_FECNAC")]
    public DateTime FechaNacimiento { get; set; }

    [Required]
    [Column("PEEMP_FECSAL")]
    public DateTime FechaSalida { get; set; }

    [Required]
    [Column("PEEMP_TELEF")]
    [StringLength(15)]
    public string Telefono { get; set; } = string.Empty;

    [Required]
    [Column("PEEMP_EMAIL")]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Column("PEEMP_CEDULA")]
    [StringLength(10)]
    public string Cedula { get; set; } = string.Empty;

    [Required]
    [Column("PEEMP_SALAR")]
    public decimal Salario { get; set; }

    [Column("PEEMP_FOTO")]
    [StringLength(250)]
    public string? Foto { get; set; }

    // Relaciones

    public Sexo? Sexo { get; set; }

    public EstadoCivil? EstadoCivil { get; set; }

    public Cargo? Cargo { get; set; }

    public Empleado? Jefe { get; set; }

    public ICollection<Empleado> Subordinados { get; set; }
        = new List<Empleado>();
}