using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("PECAR_CARGO")]
public class Cargo
{
    [Required]
    [Column("PEDEP_CODIGO")]
    [StringLength(3)]
    public string DepartamentoCodigo { get; set; } = string.Empty;

    [Required]
    [Column("PECAR_CODIGO")]
    [StringLength(3)]
    public string Codigo { get; set; } = string.Empty;

    [Required]
    [Column("PECAR_DESCRI")]
    [StringLength(50)]
    public string Descripcion { get; set; } = string.Empty;

    // Navegación
    public Departamento? Departamento { get; set; }

    public ICollection<Empleado> Empleados { get; set; }
        = new List<Empleado>();
}