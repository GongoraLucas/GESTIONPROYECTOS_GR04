using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("PEINS_INSTRUCCION")]
public class Instruccion
{
    [Key]
    [Column("PEINS_CODIGO")]
    [StringLength(2)]
    public string Codigo { get; set; } = string.Empty;

    [Column("PEINS_DESCRI")]
    [Required]
    [StringLength(50)]
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<Empleado> Empleados { get; set; }
        = new List<Empleado>();
}
