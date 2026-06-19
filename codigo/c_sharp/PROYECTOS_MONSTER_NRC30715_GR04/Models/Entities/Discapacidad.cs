using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Models.Entities;

[Table("PEDIS_DISCAPACIDAD")]
public class Discapacidad
{
    [Key]
    [Column("PEDIS_CODIGO")]
    [StringLength(2)]
    public string Codigo { get; set; } = string.Empty;

    [Column("PEDIS_DESCRI")]
    [Required]
    [StringLength(50)]
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<Empleado> Empleados { get; set; }
        = new List<Empleado>();
}
