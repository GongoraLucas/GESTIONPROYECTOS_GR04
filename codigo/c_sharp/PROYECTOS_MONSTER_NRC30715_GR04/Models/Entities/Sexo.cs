using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("PESEX_SEXO")]
public class Sexo
{
    [Key]
    [Column("PESEX_CODIGO")]
    [StringLength(1)]
    public string Codigo { get; set; } = string.Empty;

    [Column("PESEX_DESCRI")]
    [Required]
    [StringLength(50)]
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<Empleado> Empleados { get; set; }
    = new List<Empleado>();
}